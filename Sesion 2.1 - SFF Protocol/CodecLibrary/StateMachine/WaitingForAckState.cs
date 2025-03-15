using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using System;
using System.Threading;

namespace CodecLibrary.StateMachine
{
    public class WaitingForAckState : SenderState
    {
        private const int _timeout = 2000;  // Timeout en milisegundos
        private bool _ackReceived = false;
        public WaitingForAckState(Sender sender):base(sender) {
            _sender.RegisterHandler(PacketBodyType.NewFile, new AckNewFileHandler(_sender));
        }

        public override void HandleEvents()
        {
            var newFilePacket = new NewFile("example.txt", 0, new byte[0]); // Puedes modificar el contenido
            _sender.Send(newFilePacket);
            DateTime before= DateTime.Now;
            while (!_ackReceived )
            {
                DateTime now = DateTime.Now;  // Obtener el tiempo actual
                if ((now - before).TotalMilliseconds >= _timeout) // Comprobar si ha pasado el tiempo de espera
                {
                    // Si el ACK no ha llegado, duplicamos el tiempo de espera
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timeout alcanzado. Reintentando. Tiempo de espera: {_timeout}ms.");

                    // Vuelve a enviar el paquete NewFile
                    _sender.Send(newFilePacket);

                    // Actualizamos la marca de tiempo antes del siguiente intento
                    before = DateTime.Now;
                }
                // Verificamos si hay un paquete recibido antes de manejarlo
                var receivedPacket = _sender.GetReceivedPacket();  // Suponiendo que GetReceivedPacket obtiene el paquete recibido
                if (receivedPacket != null)
                {
                    // Si se recibió un paquete, lo manejamos
                    _sender.HandleReceivedPacket(receivedPacket);
                }
                // Hacer una pequeña pausa antes de la siguiente comprobación (para evitar un bucle sin fin)
                Thread.Sleep(500);
            }
        }
        public void Acknowledge()
        {
            _ackReceived = true;  // Cambiar el estado a "ACK recibido"
        }
    }
}
        
