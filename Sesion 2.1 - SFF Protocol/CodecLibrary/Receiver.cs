using CodecLibrary.Handlers;
using CodecLibrary.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary
{
    public class Receiver
    {
        private Dictionary<PacketBodyType, IPacketHandler> _packetHandlerMap;

        public Receiver()
        {
            _packetHandlerMap = new Dictionary<PacketBodyType, IPacketHandler>();
        }

        public void RegisterHandler(PacketBodyType type, IPacketHandler handler)
        {
            _packetHandlerMap[type] = handler;
        }

        public void HandleReceivedPacket(Packet packet)
        {
            if (_packetHandlerMap.ContainsKey(packet.Type))
            {
                _packetHandlerMap[packet.Type].Handle(packet);
            }
        }
    }
}