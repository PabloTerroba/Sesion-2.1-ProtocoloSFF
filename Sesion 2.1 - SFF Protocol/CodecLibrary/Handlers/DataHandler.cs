using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;
using System.IO;


namespace CodecLibrary.Handlers
{
    public class DataHandler : IPacketHandler
    {
        private Receiver _receiver;
        private string filePath = "received_file.txt";


        public DataHandler(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Handle(Packet packet)
        {
            if (packet is Data dataPacket)
            {
                Console.WriteLine($"📩 Recibido bloque #{dataPacket.SequenceNumber} ({dataPacket.Content.Length} bytes)");

                // Escribir datos en el archivo
                using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                {
                    fs.Write(dataPacket.Content, 0, dataPacket.Content.Length);
                }

                Console.WriteLine($"✅ Enviando AckData para bloque #{dataPacket.SequenceNumber}");
                _receiver.SendPacket(new AckData(dataPacket.SequenceNumber, 0, new byte[0]));

                if (_receiver.IsLastBlock(dataPacket.SequenceNumber))
                {
                    Console.WriteLine("✅ Último bloque recibido. Esperando solicitud de desconexión.");
                    _receiver.ChangeState(new WaitingForDisconState(_receiver));
                }
            }
        }
    }
}
