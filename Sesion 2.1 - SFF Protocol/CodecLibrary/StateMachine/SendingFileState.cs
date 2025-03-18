using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using CodecLibrary;
using System.Threading;
using System;
using System.IO;

public class SendingFileState : SenderState
{
    private const int MaxRetries = 5;
    private int _currentRetries = 0;
    private TimeSpan _timeout = TimeSpan.FromSeconds(2);
    private Timer _retryTimer;
    private int _sequenceNumber = 0;
    private string _filePath;
    private byte[] _fileData;
    private int _chunkSize = 512; // Tamaño del bloque de datos
    private bool _awaitingDisconAck = false;

    public SendingFileState(Sender sender, string filePath) : base(sender)
    {
        _filePath = filePath;
        _fileData = File.ReadAllBytes(_filePath);

        // Manejador para los AckData
        _packetHandlerMap.Add(PacketBodyType.AckData, new AckDataHandler(sender, this));
        _packetHandlerMap.Add(PacketBodyType.Discon, new AckDisconHandler(sender, this)); // Manejador para Discon ACK
    }

    public override void HandleEvents()
    {
        if (_currentRetries >= MaxRetries) return; // Solo continuar si no se agotaron los reintentos

        // Si el número de secuencia es válido y no se está esperando un ACK, entonces proceder a enviar el siguiente paquete
        if (_sequenceNumber * _chunkSize < _fileData.Length)
        {
            SendNextPacket();
        }
        else if (!_awaitingDisconAck) // Enviar Discon una vez se haya enviado todo el archivo
        {
            SendDisconPacket();
        }

        base.HandleEvents();
    }

    private void SendNextPacket()
    {
        int offset = _sequenceNumber * _chunkSize;
        int length = Math.Min(_chunkSize, _fileData.Length - offset);
        byte[] dataChunk = new byte[length];
        Array.Copy(_fileData, offset, dataChunk, 0, length);

        Data dataPacket = new Data(_sequenceNumber, length, dataChunk);
        byte[] encodedData = dataChunk; // Aquí iría la codificación real
        _sender.SendPacket(encodedData);

        Console.WriteLine($"Enviado paquete DATA #{_sequenceNumber}");

        _retryTimer = new Timer(_ => ResendPacket(), null, _timeout, Timeout.InfiniteTimeSpan); // Temporizador solo se inicia si no hay uno activo
    }

    private void ResendPacket()
    {
        if (_currentRetries < MaxRetries)
        {
            _currentRetries++;
            Console.WriteLine($"Reenviando paquete DATA #{_sequenceNumber} - Intento {_currentRetries}/{MaxRetries}");

            _timeout = TimeSpan.FromMilliseconds(_timeout.TotalMilliseconds * 2);
            SendNextPacket();
        }
        else
        {
            Console.WriteLine($"Se agotaron los reintentos para DATA #{_sequenceNumber}. Finalizando transmisión.");
            _sender.ChangeState(new SenderTerminatedState(_sender));
        }
    }

    public void AcknowledgePacket(int sequenceNumber)
    {
        if (sequenceNumber == _sequenceNumber)
        {
            Console.WriteLine($"ACK recibido para DATA #{sequenceNumber}");

            _retryTimer?.Dispose();  // Detener el temporizador del reintento
            _currentRetries = 0;
            _sequenceNumber++;

            // Continuar con el siguiente paquete después de recibir el ACK
            HandleEvents(); // Se llama nuevamente a HandleEvents para asegurar que se envíe el siguiente paquete
        }
    }

    private void SendDisconPacket()
    {
        if (_awaitingDisconAck)
            return;

        _awaitingDisconAck = true;
        _currentRetries = 0;
        AttemptSendDiscon();
    }
    protected override Packet Receive()
    {
        byte[] packetBytes = null;
        packetBytes = _sender.ReceivePacket();
        Packet packet = packetBytes.Decode();// Esto es lo que no sé
        return packet;
    }
    private void AttemptSendDiscon()
    {
        if (_currentRetries < MaxRetries)
        {
            _currentRetries++;
            Console.WriteLine($"Enviando paquete Discon (Intento {_currentRetries}/{MaxRetries})");

            Discon disconPacket = new Discon(0, new byte[0]);
            byte[] encodedDiscon = new byte[0]; // Aquí iría la codificación real
            _sender.SendPacket(encodedDiscon);

            _retryTimer = new Timer(_ => AttemptSendDiscon(), null, _timeout, Timeout.InfiniteTimeSpan);
            _timeout = TimeSpan.FromMilliseconds(_timeout.TotalMilliseconds * 2);
        }
        else
        {
            Console.WriteLine("No se recibió ACK para Discon. Finalizando de todos modos.");
            _sender.ChangeState(new SenderTerminatedState(_sender));
        }
    }

    public void AcknowledgeDiscon()
    {
        Console.WriteLine("ACK para Discon recibido. Finalizando conexión.");
        _retryTimer?.Dispose();  // Detener el temporizador del reintento
        _sender.ChangeState(new SenderTerminatedState(_sender));
    }
}
