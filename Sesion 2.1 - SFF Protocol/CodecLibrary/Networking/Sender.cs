using System;
using System.IO;
using CodecLibrary.Messages;
using CodecLibrary.States;
using CodecLibrary;
using System.Linq;

namespace CodecLibrary.Networking
{
    public class Sender
    {
        private SenderState _currentState;
        public string FileName { get; private set; }
        private FileStream _fileStream;

        public Sender(string fileName)
        {
            FileName = fileName;
            _fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            ChangeState(new WaitingForAckState(this));
        }

        public void ChangeState(SenderState newState)
        {
            _currentState = newState;
            Console.WriteLine($"🔄 Estado actual del emisor: {_currentState.GetType().Name}");
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

        public bool HasMoreData()
        {
            return _fileStream.Position < _fileStream.Length;
        }

        public byte[] GetNextDataChunk()
        {
            byte[] buffer = new byte[512];
            int bytesRead = _fileStream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0) return null;

            return buffer.Take(bytesRead).ToArray();  // ✅ Alternativa válida para .NET Core 2.1
        }

        public void SendNextDataChunk()
        {
            byte[] dataChunk = GetNextDataChunk();
            if (dataChunk != null)
            {
                SendPacket(new Data(0, dataChunk, dataChunk.Length, dataChunk)); // 0 = secuencia inicial
            }
        }

        public bool TimeoutReached => false; // Simulación de timeout

        public void Start()
        {
            while (!(_currentState is SenderTerminatedState))
            {
                _currentState.HandleEvents();
            }
        }
    }
}