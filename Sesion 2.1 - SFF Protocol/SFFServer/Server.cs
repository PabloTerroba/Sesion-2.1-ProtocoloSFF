using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

class Server
{
    private const int Port = 9000; // Puerto UDP para el servidor
    private UdpClient _udpServer;
    private Receiver _receiver;

    public Server()
    {
        _udpServer = new UdpClient(Port);
        _receiver = new Receiver(_udpServer);
    }

    public void Start()
    {
        Console.WriteLine($"Servidor escuchando en el puerto {Port}...");
        while (true)
        {
            _receiver.HandleEvents();
        }
    }

    public static void Main(string[] args)
    {
        Server server = new Server();
        server.Start();
    }
}
