using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;


namespace CodecLibrary.Handlers
{
    public class AckDisconHandler : IPacketHandler
    {
        private Sender _sender;

        public AckDisconHandler(Sender sender)
        {
            _sender = sender;
        }

        public void Handle(Packet packet)
        {
            if (packet is AckDiscon)
            {
                Console.WriteLine("✅ Desconexión confirmada. Finalizando sesión.");
                _sender.ChangeState(new SenderTerminatedState(_sender));
            }
        }
    }

}
