using CodecLibrary.Handlers;
using CodecLibrary.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary
{
    public class Receiver
    {
        private State _state;
        private Dictionary<PacketBodyType, IPacketHandler> _packetHandlerMap;

        public Receiver()
        {
            _state = new WaitingForRequestState(this);
            _packetHandlerMap = new Dictionary<PacketBodyType, IPacketHandler>();
        }
        public void ChangeState(State newState)
        {
            _state = newState;
        }
        public void Send(Packet packet)
        {
            Console.WriteLine($"Enviando paquete {packet.Type}");
            // Implementar el envío de paquete (puedes usar UDP/TCP)
        }
        public Packet GetReceivedPacket()
        {
            //Implementar recepción de paquete con Sockets UDP en puerto correspondiente
            byte[] body = null;
            int bodyLength = 0;
            Packet packet = new Packet(PacketBodyType.NewFile, bodyLength, body);
            return packet;
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