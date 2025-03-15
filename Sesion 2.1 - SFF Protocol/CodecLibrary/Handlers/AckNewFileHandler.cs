using CodecLibrary.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Handlers
{
    public class AckNewFileHandler : IPacketHandler
    {
        private Sender _sender;

        public AckNewFileHandler(Sender sender)
        {
            _sender = sender;
        }

        public void Handle(Packet packet)
        {
            // Aquí procesas el ACK, verificas si todo está bien
            Console.WriteLine("ACK de NewFile recibido.");

            // Si el ACK es válido, cambias de estado
            _sender.ChangeState(new SendingFileState(_sender));  // Cambiar de estado a 'SendingFile'
        }
    }

}
