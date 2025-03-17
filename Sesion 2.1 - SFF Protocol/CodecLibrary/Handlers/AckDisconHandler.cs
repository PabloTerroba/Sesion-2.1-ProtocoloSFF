<<<<<<< HEAD
﻿using CodecLibrary;
using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;

public class AckDisconHandler : IPacketHandler
{
    private Sender _sender;
    private SendingFileState _state;

    public AckDisconHandler(Sender sender, SendingFileState state)
    {
        _sender = sender;
        _state = state;
    }

    public void Handle(Packet packet)
    {
        if (packet is AckDiscon)
        {
            Console.WriteLine("ACK de Discon recibido. Finalizando conexión.");
            _state.AcknowledgeDiscon();
        }
        else
        {
            Console.WriteLine("Paquete inesperado recibido en espera de AckDiscon.");
        }
    }
}
=======
﻿using System;
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
>>>>>>> 4fe09b8ee82d800eacbfb813cb6fc9571734db42
