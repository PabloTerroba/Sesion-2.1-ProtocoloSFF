using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using CodecLibrary;

namespace CodecLibrary
{
    public class Sender
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint;
        private string _filePath;
        private State _state;

        public Sender(UdpClient udpClient, string filePath, string serverAddress, int port)
        {
            _udpClient = udpClient;
            _endPoint = new IPEndPoint(IPAddress.Parse(serverAddress), port);
            _filePath = filePath;
            _state = new WaitingForAckNewFileState(this); // Estado inicial
        }


        public void HandleEvents()
        {
            _state.HandleEvents();
        }
        public void ChangeState(State state)
        {
            _state = state;
            this.HandleEvents();
        }

        public UdpClient GetUdpClient()
        {
            return _udpClient;
        }

        public string GetFilePath()
        {
            return _filePath;
        }

        public IPEndPoint GetEndPoint()
        {
            return _endPoint;
        }

        // Función para enviar un paquete UDP
        public void SendPacket(byte[] data)
        {
            _udpClient.Send(data, data.Length, _endPoint);
            Console.WriteLine("[Sender] Paquete enviado.");
        }

        // Función para recibir un paquete UDP
        public byte[] ReceivePacket()
        {
            var receivedData = _udpClient.Receive(ref _endPoint);
            Console.WriteLine("[Sender] Paquete recibido.");
            return receivedData;
        }
    }
}
