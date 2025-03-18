using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;
using System.IO;

namespace CodecLibrary.Handlers
{
    public class DataHandler : IPacketHandler
    {
        private Receiver _receiver;
        private FileStream _fileStream;

        public DataHandler(Receiver receiver)
        {
            _receiver=receiver;
            _fileStream = new FileStream(receiver.GetFileName(), FileMode.Append, FileAccess.Write);
        }

        public void Handle(Packet packet)
        {
            var dataPacket = packet as Data;
            if (dataPacket != null)
            {
                // Escribir los datos en el archivo
                _fileStream.Write(dataPacket.Data, 0, dataPacket.Data.Length);
                _fileStream.Flush();

                Console.WriteLine($"Datos recibidos y guardados (ID: {dataPacket.SequenceNumber})");

                // Enviar Ack de confirmación
                var ackPacket = new AckData(dataPacket.SequenceNumber, 0, new byte[0]);
                _receiver.SendPacket(ackPacket.Encode());

                Console.WriteLine($"ACK enviado para el paquete {dataPacket.SequenceNumber}");
            }
            else if (packet is Discon disconPacket)
            {
                Console.WriteLine("Recibido paquete de desconexión.");

                var ackDiscon = new Discon(0, new byte[0]);
                _receiver.SendPacket(ackDiscon.Encode());

                Console.WriteLine("ACK de Disconnect enviado");

                // Cerrar el archivo y cambiar de estado
                _fileStream.Close();
                _receiver.ChangeState(new ReceiverTerminatedState(_receiver));
            }
            else
            {
                Console.WriteLine("Error: Paquete recibido desconocido.");
            }
        }
    }
}