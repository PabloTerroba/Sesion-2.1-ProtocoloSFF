using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public class WaitingForDisconAckState : SenderState
    {
        public WaitingForDisconAckState(Sender sender) : base(sender)
        {
            _packetHandlerMap[PacketBodyType.AckDiscon] = new AckDisconHandler(sender);
        }

        public override void HandleEvents()
        {
            Console.WriteLine("⏳ Esperando AckDiscon...");

            base.HandleEvents(); // Procesa el paquete recibido

            if (_sender.TimeoutReached)
            {
                Console.WriteLine("⚠️ Timeout alcanzado. Reenviando solicitud de desconexión...");
                Send(new Discon(0, new byte[0]));
            }
        }
    }

}
