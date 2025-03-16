using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{

    public abstract class State
    {
        protected Dictionary<PacketBodyType, IPacketHandler> _packetHandlerMap = new Dictionary<PacketBodyType, IPacketHandler>();

        public virtual void HandleEvents()
        {
            try
            {
                Packet packet = Receive();
                if (_packetHandlerMap.TryGetValue(packet.Type, out IPacketHandler handler))
                {
                    handler.Handle(packet);
                }
                else
                {
                    OnUnknownPacket(packet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el manejo de eventos: {ex.Message}");
            }
        }

        protected abstract Packet Receive();
        protected virtual void OnUnknownPacket(Packet packet)
        {
            Console.WriteLine($"⚠️ Paquete desconocido recibido: {packet.Type}");
        }
    }
}
