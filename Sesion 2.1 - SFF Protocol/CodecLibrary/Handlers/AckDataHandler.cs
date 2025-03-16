using System;
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
