using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public class ReceivingFileState : ReceiverState
    {
        private string _filePath = "received_file.txt";
        private int sequenceNumber;

        public ReceivingFileState(Receiver receiver) : base(receiver)
        {
            _packetHandlerMap[PacketBodyType.Data] = new DataHandler(receiver);
        }

        public override void HandleEvents()
        {
            Console.WriteLine("📩 Esperando bloques de datos...");

            base.HandleEvents(); // Procesa el paquete recibido

            if (_receiver.IsLastBlock(sequenceNumber))
            {
                Console.WriteLine("✅ Archivo recibido completamente. Esperando solicitud de desconexión.");
                ChangeState(new WaitingForDisconState(_receiver));
            }
        }
    }

}
