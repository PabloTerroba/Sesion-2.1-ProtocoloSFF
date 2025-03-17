using System;
using System.IO;
using CodecLibrary.States;
using CodecLibrary;

namespace CodecLibrary.Networking
{
    public class Receiver
    {
        private ReceiverState _currentState;
        private int _expectedLastSequenceNumber;

        public string FileName { get; set; }

        public Receiver()
        {
            ChangeState(new WaitingForRequestState(this));
        }

        public void ChangeState(ReceiverState newState)
        {
            _currentState = newState;
            Console.WriteLine($"🔄 Estado actual del receptor: {_currentState.GetType().Name}");
        }

        public void SendPacket(Packet packet)
        {
            Console.WriteLine($"📤 Enviando: {packet.Type}");
            // Aquí iría la lógica para enviar el paquete por la red (UDP)
        }

        public Packet ReceivePacket()
        {
            // Simulación de recepción de paquetes
            return null;
        }

        public bool ReceivedBlock(int sequenceNumber)
        {
            return true; // Simulación de confirmación
        }

        public bool IsLastBlock(int sequenceNumber)
        {
            // Lógica para determinar si el bloque actual es el último
            return sequenceNumber == _expectedLastSequenceNumber;
        }

        public bool CanResumeReceiving => true; // Simulación de control de flujo

        public void Start()
        {
            while (!(_currentState is ReceiverTerminatedState))
            {
                _currentState.HandleEvents();
            }
        }
    }
}