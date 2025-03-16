using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public class WaitingForDisconState : ReceiverState
    {
        public WaitingForDisconState(Receiver receiver) : base(receiver)
        {
            _packetHandlerMap[PacketBodyType.Discon] = new DisconHandler(receiver);
        }

        public override void HandleEvents()
        {
            Console.WriteLine("⏳ Esperando Discon...");

            base.HandleEvents(); // Procesa el paquete recibido

            Console.WriteLine("✅ Desconexión confirmada. Enviando AckDiscon.");
            _receiver.SendPacket(new AckDiscon(0, new byte[0]));
            _receiver.ChangeState(new ReceiverTerminatedState(_receiver));
        }
    }

}
