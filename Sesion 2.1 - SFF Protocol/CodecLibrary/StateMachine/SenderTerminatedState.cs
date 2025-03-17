using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CodecLibrary.StateMachine
{
    public class SenderTerminatedState: SenderState
    {
        public SenderTerminatedState(Sender sender) : base(sender)
        {
            // Lógica de limpieza cuando el estado es alcanzado
            Console.WriteLine("Estado de terminación alcanzado. La transmisión ha finalizado.");
            // Realizar acciones para cerrar la conexión, si es necesario
            CloseSender();
        }

        public override void HandleEvents()
        {
            // Este estado no maneja eventos. Simplemente termina el proceso.
            Console.WriteLine("El proceso de envío ha terminado, no se aceptan más eventos.");
        }
        public void CloseSender()
        {
            Console.WriteLine("[Sender] Cerrando recursos...");

            // Cerrar el UdpClient si está activo
            if (_sender.GetUdpClient() != null)
            {
                try
                {
                    _sender.GetUdpClient().Close();
                    Console.WriteLine("[Sender] Conexión UDP cerrada.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Sender] Error al cerrar la conexión UDP: {ex.Message}");
                }
            }

            // Liberar otros recursos si es necesario
            // (en este caso no parece haber otros recursos como archivos, pero si los hubiera,
            // también deberían ser cerrados o liberados aquí).

            // En este caso, no hay necesidad de nullificar variables privadas, pero si fuera necesario
            // podríamos hacerlo aquí, como:
            // _udpClient = null;
            // _endPoint = null;

            // Finalmente, notificar que el Sender ha sido cerrado
            Console.WriteLine("[Sender] Recursos cerrados correctamente.");
        }

    }
}
