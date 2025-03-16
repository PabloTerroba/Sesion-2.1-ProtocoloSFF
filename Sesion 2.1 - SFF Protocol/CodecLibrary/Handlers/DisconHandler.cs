using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;


namespace CodecLibrary.Handlers
{
    public class DisconHandler : IPacketHandler
    {
        private Receiver _receiver;

        public DisconHandler(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Handle(Packet packet)
        {
            if (packet is Discon)
            {
                Console.WriteLine("🚪 Solicitud de desconexión recibida.");
                Console.WriteLine("✅ Enviando AckDiscon...");
                _receiver.SendPacket(new AckDiscon(0, new byte[0]));
                _receiver.ChangeState(new ReceiverTerminatedState(_receiver));
            }
        }
    }

}
