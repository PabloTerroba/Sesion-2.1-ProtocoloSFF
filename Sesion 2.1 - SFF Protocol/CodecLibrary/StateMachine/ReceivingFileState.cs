using System;
using System.IO;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;

namespace CodecLibrary.StateMachine
{
    public class ReceivingFileState : ReceiverState
    {
        private string _filePath;
        private FileStream _fileStream;
        private int _expectedSequenceNumber = 0;

        public ReceivingFileState(Receiver receiver, string filePath) : base(receiver)
        {
            _filePath = filePath;
            _fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
            _receiver.RegisterHandler(PacketBodyType.Data, new DataHandler(this)); // Registra el handler
            _receiver.RegisterHandler(PacketBodyType.EndOfFile, new EndOfFileHandler(this)); // Registra el handler

        }

        public override void HandleEvents()
        {
            Console.WriteLine("Esperando paquetes de datos...");

            while (true)
            {
                Packet receivedPacket = _receiver.GetReceivedPacket();
                if (receivedPacket != null)
                {
                    _receiver.HandleReceivedPacket(receivedPacket);
                }
            }
        }

        public void WriteData(byte[] data, int length, int sequenceNumber)
        {
            if (sequenceNumber < _expectedSequenceNumber)
            {
                // Se recibió un paquete duplicado porque el ACK anterior se perdió. Reenviar ACK sin escribirlo de nuevo.
                Console.WriteLine($"Paquete {sequenceNumber} ya recibido. Reenviando ACK.");
                byte[] body = new byte[0];
                AckData ack = new AckData(sequenceNumber, body.Length, body);
                _receiver.Send(ack);
                return; // No escribir en el archivo otra vez
            }
            if (sequenceNumber == _expectedSequenceNumber)
            {
                _fileStream.Write(data, 0, length);
                _fileStream.Flush();
                Console.WriteLine($"Paquete {sequenceNumber} recibido y escrito correctamente.");

                // Enviar ACK para el paquete recibido correctamente
                byte[] body= new byte[0];
                AckData ack = new AckData(sequenceNumber,body.Length,body);
                _receiver.Send(ack);

                // Pasar al siguiente número de secuencia esperado
                _expectedSequenceNumber++;
            }
        }

        public void CompleteReception(AckEndOfFile ackEoF)
        {
            _receiver.Send(ackEoF);
            _fileStream.Close();
            Console.WriteLine("Recepción del archivo completada.");
            _receiver.ChangeState(new WaitingForDisconState(_receiver)); // Cambiar al siguiente estado si es necesario
        }
    }
}
