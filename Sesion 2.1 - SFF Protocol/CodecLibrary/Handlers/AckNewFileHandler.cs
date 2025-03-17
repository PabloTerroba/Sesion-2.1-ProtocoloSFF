<<<<<<< HEAD
﻿using CodecLibrary.Handlers;
using CodecLibrary;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

public class AckNewFileHandler : IPacketHandler
{
    private Sender _sender;
    private const int MaxRetries = 5; // Número máximo de intentos
    private int _currentRetries = 0;
    private TimeSpan _timeout = TimeSpan.FromSeconds(2); // Timeout inicial de 2 segundos
    private Timer _retryTimer; // El temporizador para manejar los reintentos

    public AckNewFileHandler(Sender sender)
    {
        _sender = sender;
    }

    public void Handle(Packet packet)
    {
        // Verifica que el paquete recibido sea del tipo AckNewFile
        if (packet is AckNewFile ackNewFilePacket)
        {
            Console.WriteLine("ACK de NewFile recibido correctamente.");
            _sender.ChangeState(new SendingFileState(_sender, _sender.GetFilePath())); // Cambiar al siguiente estado
        }
        else
        {
            // Si no fue exitoso, manejar el error
            Console.WriteLine("Error:Paquete recibido no es ACKNewFile.");
            RetrySendingNewFile(); // Reintentar enviar el NewFile
        }
    }

    private void RetrySendingNewFile()
    {
        if (_currentRetries < MaxRetries)
        {
            _currentRetries++;
            Console.WriteLine($"Reintentando enviar el paquete NewFile. Intento {_currentRetries} de {MaxRetries}.");

            // Aumentar el timeout para el siguiente intento
            _timeout = _timeout.Add(_timeout);  // Duplicamos el tiempo de espera

            // Reenviar el paquete NewFile después del timeout
            SendNewFilePacketWithRetry();

            // Crear el temporizador para manejar el reintento asincrónicamente
            _retryTimer = new Timer(RetryTimerCallback, null, _timeout, Timeout.InfiniteTimeSpan);
        }
        else
        {
            // Si hemos superado el número máximo de intentos, fallamos la operación
            Console.WriteLine("Se han agotado los intentos. La operación ha fallado.");
            _sender.ChangeState(new SenderTerminatedState(_sender)); // Finalizamos la operación
        }
    }

    // Este es el callback del temporizador que se ejecutará cuando termine el tiempo de espera
    private void RetryTimerCallback(object state)
    {
        // Reintentar el envío de NewFile
        RetrySendingNewFile();
    }

    private void SendNewFilePacketWithRetry()
    {
        // Crear el paquete NewFile con el nombre del archivo
        string fileName = _sender.GetFilePath().Split('\\').Last(); // Obtenemos el nombre del archivo
        NewFile newFilePacket = new NewFile(fileName, 0, new byte[0]);

        // Codificación del paquete (asumimos que la codificación es la correcta)
        byte[] newFileCod = new byte[0];

        // Enviar el paquete NewFile
        _sender.SendPacket(newFileCod); // Reenviar el paquete
    }
}
