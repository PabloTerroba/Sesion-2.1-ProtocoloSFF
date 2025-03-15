using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using System;

namespace CodecLibrary.StateMachine
{
    public class WaitingForRequestState : ReceiverState
    {
        public WaitingForRequestState(Receiver receiver) : base(receiver)
        {
            // Aquí podemos registrar el manejador del paquete NewFile
            _receiver.RegisterHandler(PacketBodyType.NewFile, new NewFileHandler(_receiver));
        }

        public override void HandleEvents()
        {
            // El estado espera recibir un paquete NewFile
            Console.WriteLine("Esperando solicitud de archivo (NewFile)...");
            var receivedPacket = _receiver.GetReceivedPacket();  // Suponiendo que GetReceivedPacket obtiene el paquete recibido
            if (receivedPacket != null)
            {
                // Si se recibió un paquete, lo manejamos
                _receiver.HandleReceivedPacket(receivedPacket);
            }
        }
    }
}
