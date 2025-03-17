<<<<<<< HEAD
﻿using CodecLibrary.Messages;
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
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary.Networking;


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
            if (packet is Discon)
            {
                Console.WriteLine("🚪 Solicitud de desconexión recibida.");
                Console.WriteLine("✅ Enviando AckDiscon...");
                _receiver.SendPacket(new AckDiscon(0, new byte[0]));
                _receiver.ChangeState(new ReceiverTerminatedState(_receiver));
            }
        }
    }

}
>>>>>>> 4fe09b8ee82d800eacbfb813cb6fc9571734db42
