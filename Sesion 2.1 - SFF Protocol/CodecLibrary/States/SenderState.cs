using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public abstract class SenderState : State
    {
        protected Sender _sender;

        public SenderState(Sender sender)
        {
            _sender = sender;
        }

        protected void Send(Packet packet)
        {
            Console.WriteLine($"📤 Enviando paquete: {packet.Type}");
            _sender.SendPacket(packet);
        }

        protected void ChangeState(SenderState newState)
        {
            Console.WriteLine($"🔄 Cambio de estado: {this.GetType().Name} → {newState.GetType().Name}");
            _sender.ChangeState(newState);
        }

        protected override Packet Receive()
        {
            return _sender.ReceivePacket();
        }
    }

}
