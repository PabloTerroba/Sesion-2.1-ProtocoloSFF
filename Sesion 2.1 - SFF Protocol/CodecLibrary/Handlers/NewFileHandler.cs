using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;


namespace CodecLibrary.Handlers
{
    public class NewFileHandler : IPacketHandler
    {
        private Receiver _receiver;

        public NewFileHandler(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Handle(Packet packet)
        {
            if (packet is NewFile newFile)
            {
                Console.WriteLine($"📂 Solicitud de transferencia recibida para: {newFile.FileName}");
                _receiver.FileName = newFile.FileName;
                Console.WriteLine("✅ Enviando AckNewFile...");
                _receiver.SendPacket(new AckNewFile(0, new byte[0]));
                _receiver.ChangeState(new ReceivingFileState(_receiver));
            }
        }
    }

}
