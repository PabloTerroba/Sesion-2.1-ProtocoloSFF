using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public class WaitingForRequestState : ReceiverState
    {
        public WaitingForRequestState(Receiver receiver) : base(receiver)
        {
            _packetHandlerMap[PacketBodyType.NewFile] = new NewFileHandler(receiver);
        }

        public override void HandleEvents()
        {
            Console.WriteLine("⏳ Esperando solicitud de transferencia...");

            base.HandleEvents(); // Procesa el paquete recibido

            Console.WriteLine("✅ Solicitud aceptada. Enviando AckNewFile.");
            _receiver.SendPacket(new AckDiscon(0, new byte[0]));
            ChangeState(new ReceivingFileState(_receiver));
        }
    }

}
