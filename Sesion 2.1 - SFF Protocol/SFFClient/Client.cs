using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using CodecLibrary;

public class Client
{
    private const string ServerIP = "127.0.0.1"; // IP del servidor
    private const int ServerPort = 9000;
    private UdpClient _udpClient;
    private Sender _sender;

    public Client(string filePath)
    {
        _udpClient = new UdpClient();
        _udpClient.Connect(ServerIP, ServerPort);

        _sender = new Sender(_udpClient, filePath, ServerIP, ServerPort);
    }

    public void Start()
    {
        string fileName = _sender.GetFilePath().Split('\\').Last();
        Console.WriteLine($"Enviando archivo '{fileName}' al servidor {ServerIP}:{ServerPort}...");
        _sender.HandleEvents();
    }

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Uso: Client <ruta_del_archivo>");
            return;
        }

        string filePath = args[0];

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: El archivo '{filePath}' no existe.");
            return;
        }

        Client client = new Client(filePath);
        client.Start();
    }
}
