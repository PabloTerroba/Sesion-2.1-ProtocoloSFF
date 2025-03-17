using CodecLibrary.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.StateMachine
{
    public abstract class SenderState : State
    {
        public Sender _sender;

        // Constructor que recibe el contexto (Sender) para poder acceder a él
        public SenderState(Sender sender)
        {
            _sender = sender;
            _packetHandlerMap = new Dictionary<int, IPacketHandler>(); // Este map es necesario para manejar los paquetes
        }

    }
}
