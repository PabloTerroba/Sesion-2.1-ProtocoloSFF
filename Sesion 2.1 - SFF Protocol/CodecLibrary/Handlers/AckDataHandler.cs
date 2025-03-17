<<<<<<< HEAD
﻿using CodecLibrary;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;

public class AckDataHandler : IPacketHandler
{
    private Sender _sender;
    private SendingFileState _sendingState;

    public AckDataHandler(Sender sender, SendingFileState sendingState)
    {
        _sender = sender;
        _sendingState = sendingState;
    }

    public void Handle(Packet packet)
    {
        if (packet is AckData ackDataPacket)
        {
            _sendingState.AcknowledgePacket(ackDataPacket.SequenceNumber);
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
    public class AckDataHandler : IPacketHandler
    {
        private Sender _sender;

        public AckDataHandler(Sender sender)
        {
            _sender = sender;
        }

        public void Handle(Packet packet)
        {
            if (packet is AckData ackData)
            {
                Console.WriteLine($"✅ Confirmación de recepción para el bloque #{ackData.SequenceNumber}");

                if (_sender.HasMoreData())
                {
                    _sender.SendNextDataChunk();
                }
                else
                {
                    Console.WriteLine("✅ Todos los datos enviados. Solicitando desconexión.");
                    _sender.ChangeState(new WaitingForDisconAckState(_sender));
                }
            }
        }
    }

}
>>>>>>> 4fe09b8ee82d800eacbfb813cb6fc9571734db42
