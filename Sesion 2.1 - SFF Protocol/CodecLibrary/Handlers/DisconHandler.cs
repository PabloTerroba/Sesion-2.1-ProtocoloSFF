using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;

namespace CodecLibrary.Handlers
{
    public class DisconHandler : IPacketHandler
    {
        private Receiver _receiver;

        public DisconHandler(Receiver receiver)
        {
            _receiver = receiver;
        }

        public void Handle(Packet packet)
        {
            // Verificar si el paquete es una solicitud de desconexión
            if (packet.Type == PacketBodyType.Discon)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Solicitud de desconexión recibida.");
                // Deserializar el paquete NewFile
                var disconPacket = packet as Discon;

                if (disconPacket == null)
                {
                    Console.WriteLine("Error: Paquete recibido no es un Disconnection.");
                    return;
                }

            }
        }
    }
}
