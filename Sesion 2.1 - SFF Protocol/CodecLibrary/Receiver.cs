using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Receiver
{
    private UdpClient _udpServer;
    private IPEndPoint _remoteEndPoint;
    private FileStream _fileStream;
    private string _fileName;
    private bool _receivingFile = false;

    public Receiver(UdpClient udpServer)
    {
        _udpServer = udpServer;
        _remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    public void HandleEvents()
    {
        byte[] receivedData = _udpServer.Receive(ref _remoteEndPoint);
        ProcessPacket(receivedData);
    }

    private void ProcessPacket(byte[] packet)
    {
        byte messageType = packet[0];

        switch (messageType)
        {
            case 1: // NewFile
                _fileName = Encoding.UTF8.GetString(packet, 1, packet.Length - 1);
                _fileStream = new FileStream(_fileName, FileMode.Create, FileAccess.Write);
                _receivingFile = true;
                Console.WriteLine($"[Receiver] Recibiendo archivo: {_fileName}");
                break;

            case 2: // Data
                if (_receivingFile && _fileStream != null)
                {
                    _fileStream.Write(packet, 1, packet.Length - 1);
                    Console.WriteLine($"[Receiver] Recibidos {packet.Length - 1} bytes.");
                }
                break;

            case 3: // Discon
                _fileStream?.Close();
                _receivingFile = false;
                Console.WriteLine("[Receiver] Transferencia finalizada. Archivo guardado.");
                break;

            default:
                Console.WriteLine("[Receiver] Mensaje desconocido.");
                break;
        }
    }
}
