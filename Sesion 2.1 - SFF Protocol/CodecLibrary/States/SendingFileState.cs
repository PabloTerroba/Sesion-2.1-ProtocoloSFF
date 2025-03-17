using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public class SendingFileState : SenderState
    {
        private int _nextSequenceNumber = 0;

        public SendingFileState(Sender sender) : base(sender)
        {
            _packetHandlerMap[PacketBodyType.AckData] = new AckDataHandler(sender);
        }

        public override void HandleEvents()
        {
            Console.WriteLine($"📤 Enviando bloque #{_nextSequenceNumber}...");

            byte[] dataChunk = _sender.GetNextDataChunk();
            if (dataChunk != null)
            {
                Send(new Data(_nextSequenceNumber, dataChunk, dataChunk.Length, dataChunk));
                _nextSequenceNumber++;
            }
            else
            {
                Console.WriteLine("✅ Todos los bloques enviados. Pasando a estado de espera de desconexión.");
                ChangeState(new WaitingForDisconAckState(_sender));
            }

            base.HandleEvents(); // Procesa el paquete recibido
        }
    }
}
