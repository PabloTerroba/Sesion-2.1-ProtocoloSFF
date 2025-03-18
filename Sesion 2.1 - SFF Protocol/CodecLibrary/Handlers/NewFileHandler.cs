using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using CodecLibrary;
using System;

public class NewFileHandler : IPacketHandler
{
    private Receiver _receiver;
    public NewFileHandler(Receiver receiver)
    {
        _receiver = receiver;
    }

    public void Handle(Packet packet)
    {
        // Deserializar el paquete NewFile
        var newFilePacket = packet as NewFile;

        if (newFilePacket == null)
        {
            Console.WriteLine("Error: Paquete recibido no es un NewFile.");
            return;
        }
        else {
            // Mostrar el nombre del archivo recibido
            Console.WriteLine($"Nuevo archivo recibido: {newFilePacket.FileName}");

            // Enviar el ACK por la recepción del archivo
            var ackNewFilePacket = new AckNewFile(0, new byte[0]); // El ACK puede no necesitar datos adicionales
            _receiver.SendPacket(ackNewFilePacket);
            Console.WriteLine($"ACK enviado para el archivo: {newFilePacket.FileName}");

            // Cambiar el estado a ReceivingFileState para empezar a recibir el archivo
            string fileName = newFilePacket.FileName;
            _receiver.SetFileName(fileName);
            _receiver.ChangeState(new ReceivingFileState(_receiver);  // Cambio al estado donde se manejará la recepción del archivo
        }
    }
}
