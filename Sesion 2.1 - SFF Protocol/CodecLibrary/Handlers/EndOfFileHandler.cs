using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;

namespace CodecLibrary.Handlers
{
    public class EndOfFileHandler : IPacketHandler
    {
        private ReceivingFileState _state;

        public EndOfFileHandler(ReceivingFileState state)
        {
            _state = state;
        }

        public void Handle(Packet packet)
        {
            if (packet is EndOfFile)
            {
                Console.WriteLine("EndOfFile recibido. Enviando ACK...");

                // Enviar ACK para confirmar la recepción del EOF
                AckEndOfFile ackEoF = new AckEndOfFile();
                _state.CompleteReception(ackEoF);
            }
            else
            {
                Console.WriteLine("Paquete recibido no es EndOfFile.");
            }
        }
    }
}
