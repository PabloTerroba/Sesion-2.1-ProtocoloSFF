using CodecLibrary.Handlers;
using CodecLibrary.StateMachine;
using System;
using System.Collections.Generic;

namespace CodecLibrary
{
    public class Sender
    {
        private State _state;
        private Dictionary<PacketBodyType, IPacketHandler> _packetHandlerMap;

        public Sender()
        {
            _state = new WaitingForAckState(this);  // Inicializa con el primer estado
            _packetHandlerMap = new Dictionary<PacketBodyType, IPacketHandler>();
        }

        public void Run()
        {
            // Ejecuta el flujo de eventos del emisor
            _state.HandleEvents();
        }

        public void ChangeState(State newState)
        {
            _state = newState;
        }
        public State GetState()
        {
            return _state;
        }
        public void Send(Packet packet)
        {
            Console.WriteLine($"Enviando paquete {packet.Type}");
            // Implementar el envío de paquete (puedes usar UDP/TCP)
        }

        public void RegisterHandler(PacketBodyType type, IPacketHandler handler)
        {
            _packetHandlerMap[type] = handler;
        }
        public Packet GetReceivedPacket()
        {
            //Implementar recepción de paquete con Sockets UDP en puerto correspondiente
            byte [] body = null;
            int bodyLength = 0;
            Packet packet= new Packet(PacketBodyType.NewFile,bodyLength,body);
            return packet;
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
