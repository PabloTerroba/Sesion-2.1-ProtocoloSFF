using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public class WaitingForAckState : SenderState
    {
        public WaitingForAckState(Sender sender) : base(sender)
        {
            _packetHandlerMap[PacketBodyType.AckNewFile] = new AckNewFileHandler(sender);
        }

        public override void HandleEvents()
        {
            Console.WriteLine("⏳ Esperando AckNewFile...");

            base.HandleEvents(); // Procesa el paquete recibido

            // Simulación de timeout y reenvío si es necesario
            if (_sender.TimeoutReached)
            {
                Console.WriteLine("⚠️ Timeout alcanzado. Reenviando solicitud...");
                Send(new NewFile(_sender.FileName, 0, new byte[0]));
            }
        }
    }

}
