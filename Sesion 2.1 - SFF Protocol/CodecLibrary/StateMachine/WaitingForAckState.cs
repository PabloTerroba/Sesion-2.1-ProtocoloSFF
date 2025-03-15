using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using System;
using System.Threading;

namespace CodecLibrary.StateMachine
{
    public class WaitingForAckState : SenderState
    {
        private const int Timeout = 2000;  // Timeout en milisegundos

        public WaitingForAckState(Sender sender):base(sender) {
            _sender.RegisterHandler(PacketBodyType.NewFile, new AckNewFileHandler(_sender));
        }

        public override void HandleEvents()
        {
            var newFilePacket = new NewFile("example.txt", 0, new byte[0]); // Puedes modificar el contenido
            _sender.Send(newFilePacket);
            bool ackReceived = false;
            DateTime before= DateTime.Now;
            while (!ackReceived )
            {
                DateTime now = DateTime.Now;  // Obtener el tiempo actual
                if ((now - before).TotalMilliseconds >= Timeout) // Comprobar si ha pasado el tiempo de espera
                {
                    // Si el ACK no ha llegado, duplicamos el tiempo de espera
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Timeout alcanzado. Reintentando. Tiempo de espera: {_timeout}ms.");

                    // Vuelve a enviar el paquete NewFile
                    _sender.Send(newFilePacket);

                    // Actualizamos la marca de tiempo antes del siguiente intento
                    before = DateTime.Now;
                }

                // Hacer una pequeña pausa antes de la siguiente comprobación (para evitar un bucle sin fin)
                Thread.Sleep(500);
            }

            // Si llegamos aquí es porque se recibió el ACK
            if (ackReceived)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ACK recibido. Cambiando de estado.");
                _sender.ChangeState(new SendingFileState(_sender));  // Cambiar al siguiente estado
            }
        }
        }

        
    }
}
        
