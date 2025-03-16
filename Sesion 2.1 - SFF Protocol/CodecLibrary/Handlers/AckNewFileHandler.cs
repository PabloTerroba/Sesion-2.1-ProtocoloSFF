using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;


namespace CodecLibrary.Handlers
{
    public class AckNewFileHandler : IPacketHandler
    {
        private Sender _sender;

        public AckNewFileHandler(Sender sender)
        {
            _sender = sender;
        }

        public void Handle(Packet packet)
        {
            if (packet is AckNewFile)
            {
                Console.WriteLine("✅ Transferencia aceptada. Iniciando envío de datos.");
                _sender.ChangeState(new SendingFileState(_sender));
            }
        }
    }

}
