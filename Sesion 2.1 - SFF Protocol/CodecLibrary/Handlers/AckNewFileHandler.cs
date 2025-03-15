using CodecLibrary.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Handlers
{
    public class AckNewFileHandler : IPacketHandler
    {
        private readonly Sender _sender;

        public AckNewFileHandler(Sender sender)
        {
            _sender = sender;
        }

        public void Handle(Packet packet)
        {
            // Verifica si el paquete es un ACK de NewFile
            if (packet.Type == PacketBodyType.AckNewFile)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ACK recibido para NewFile.");

                if (_sender.GetState() is WaitingForAckState waitingForAckState)
                {
                    waitingForAckState.Acknowledge();
                }

                // Después de que el ACK ha sido recibido, cambiamos de estado
                _sender.ChangeState(new SendingFileState(_sender));  // Cambiar al siguiente estado
            }
        }
    }


}
