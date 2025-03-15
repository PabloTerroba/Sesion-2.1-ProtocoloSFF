using CodecLibrary.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.StateMachine
{
    public abstract class State
    {
        protected abstract Packet Receive(); // Método para recibir el paquete.

        public virtual void HandleEvents()
        {
            try
            {
                Packet packet = Receive();  // Recibir el paquete

                // Buscar el manejador adecuado para este tipo de paquete
                IPacketHandler handler = _packetHandlerMap[packet.Type];
                handler.Handle(packet);  // Llamar al manejador para procesar el paquete.
            }
            catch (KeyNotFoundException)
            {
                // Si no se encuentra un manejador para este tipo de paquete
                Console.WriteLine("Unknown packet type.");
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Mapa de tipos de paquetes a sus manejadores correspondientes
        protected Dictionary<PacketBodyType, IPacketHandler> _packetHandlerMap = new Dictionary<PacketBodyType, IPacketHandler>();

        // Método para registrar manejadores
        public void RegisterHandler(PacketBodyType type, IPacketHandler handler)
        {
            _packetHandlerMap.Add(type, handler);
        }
    }

}
