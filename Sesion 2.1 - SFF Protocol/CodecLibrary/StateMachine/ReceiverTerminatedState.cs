using System;
using System.Net.Sockets;

namespace CodecLibrary.StateMachine
{
    public class ReceiverTerminatedState : ReceiverState
    {
        public ReceiverTerminatedState(Receiver receiver) : base(receiver)
        {
            // Lógica de limpieza cuando el estado es alcanzado
            Console.WriteLine("Estado de terminación alcanzado. La recepción ha finalizado.");
            // Realizar acciones para cerrar la conexión, si es necesario
            CloseReceiver();
        }

        public override void HandleEvents()
        {
            // Este estado no maneja eventos. Simplemente termina el proceso.
            Console.WriteLine("El proceso de recepción ha terminado, no se aceptan más eventos.");
        }

        private void CloseReceiver()
        {
            Console.WriteLine("[Receiver] Cerrando recursos...");

            // Cerrar el UdpClient si está activo
            if (_receiver.GetUdpClient() != null)
            {
                try
                {
                    _receiver.GetUdpClient().Close();
                    Console.WriteLine("[Receiver] Conexión UDP cerrada.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Receiver] Error al cerrar la conexión UDP: {ex.Message}");
                }
            }

            // Finalmente, notificar que el Receiver ha sido cerrado
            Console.WriteLine("[Receiver] Recursos cerrados correctamente.");
        }
    }
}
