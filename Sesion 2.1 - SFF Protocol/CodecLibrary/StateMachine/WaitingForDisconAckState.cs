using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using System;
using System.Threading;

namespace CodecLibrary.StateMachine
{
    public class WaitingForDisconAckState : SenderState
    {
        private bool _disconAckReceived = false;
        private Discon _disconPacket;
        private const int _timeout = 2000;  // Timeout en milisegundos

        public WaitingForDisconAckState(Sender sender) : base(sender)
        {
            _sender.RegisterHandler(PacketBodyType.AckDiscon, new AckDisconHandler(_sender));  // Registrar el handler
        }

        public override void HandleEvents()
        {
            DateTime before = DateTime.Now;
            _disconPacket = new Discon(0, new byte[0]);
            while (!_disconAckReceived)
            {
                _sender.Send(_disconPacket);  // Enviar el paquete Disconnect
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Enviando paquete Disconnect.");

                // Verificar si ha pasado el tiempo de espera
                DateTime now = DateTime.Now;
                if ((now - before).TotalMilliseconds >= _timeout)
                {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timeout alcanzado. Reintentando.");
                    before = DateTime.Now; // Resetear el tiempo de espera
                }

                // Verificar si hay un paquete recibido y manejarlo
                var receivedPacket = _sender.GetReceivedPacket();
                if (receivedPacket != null)
                {
                    _sender.HandleReceivedPacket(receivedPacket);  // Procesar el paquete recibido
                }

                Thread.Sleep(500);  // Esperar un poco antes de verificar nuevamente
            }

            // Cuando se recibe el ACK de desconexión, cambiar a un estado final o de cierre
            _sender.ChangeState(new SenderTerminatedState(_sender));
        }

        public void Acknowledge()
        {
            _disconAckReceived = true;  // El ACK de desconexión ha sido recibido
        }
    }
}
