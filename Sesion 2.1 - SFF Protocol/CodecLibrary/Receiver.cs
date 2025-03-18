using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CodecLibrary
{
    public class Receiver
    {
        private UdpClient _udpServer; // Usamos _udpServer
        private IPEndPoint _remoteEndPoint; // Esta es la variable que estamos usando para el endpoint remoto
        private FileStream _fileStream;
        private string _fileName;
        private bool _receivingFile = false;
        private State _state;

        public Receiver(UdpClient udpServer)
        {
            _udpServer = udpServer;
            _remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);  // Escuchar en cualquier IP
            _state = new WaitingForRequestState(this);  // Establecer estado inicial
        }

        // Método que maneja eventos del estado
        public void HandleEvents()
        {
            _state.HandleEvents();
        }

        // Cambiar el estado en el receptor
        public void ChangeState(State state)
        {
            _state = state;
        }

        // Métodos getter
        public UdpClient GetUdpClient()
        {
            return _udpServer;  // Usamos _udpServer aquí
        }


        public IPEndPoint GetEndPoint()
        {
            return _remoteEndPoint;  // Usamos _remoteEndPoint
        }

        // Función para enviar un paquete UDP
        public void SendPacket(byte[] data)
        {
            _udpServer.Send(data, data.Length, _remoteEndPoint);  // Usamos _udpServer y _remoteEndPoint
            Console.WriteLine("[Receiver] Paquete enviado.");
        }
        public void SetFileName(string fileName)
        {
            _fileName = fileName;
        }
        public string GetFileName()
        {
            return _fileName;
        }
        // Función para recibir un paquete UDP
        public byte[] ReceivePacket()
        {
            var receivedData = _udpServer.Receive(ref _remoteEndPoint);  // Usamos _udpServer y _remoteEndPoint
            Console.WriteLine("[Receiver] Paquete recibido.");
            return receivedData;
        }
    }
}
