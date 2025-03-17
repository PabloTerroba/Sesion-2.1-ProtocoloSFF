using CodecLibrary.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Handlers
{
    public class AckEndOfFileHandler : IPacketHandler
    {
        private Sender _sender;
        public AckEndOfFileHandler(Sender sender)
        {
            _sender = sender;
        }

        public void Handle(Packet packet)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ACK recibido para EndOfFile.");

        }
    }
}
