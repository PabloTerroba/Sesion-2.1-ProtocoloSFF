using CodecLibrary;
using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System.IO.Enumeration;
using System.Linq;

public class WaitingForAckNewFileState : SenderState
{
    private string _filePath;
    public WaitingForAckNewFileState(Sender sender) : base(sender)
    {
        // Crear el paquete NewFile con el nombre del archivo
        _filePath = _sender.GetFilePath();
        string fileName=_filePath.Split('\\').Last(); // Obtenemos el nombre del archivo
        NewFile newFilePacket = new NewFile(fileName,0,new byte[0]);
        //Codificación
        byte[] newFileCod = new byte[0];
        // Enviar el paquete NewFile
        _sender.SendPacket(newFileCod);
        // Aquí se podría definir un manejador para ACKNewFile
        _packetHandlerMap.Add((int)PacketBodyType.AckNewFile, new AckNewFileHandler(sender));
        _packetHandlerMap.Add((int)PacketBodyType.AckData, new AckDataHandler());
        _packetHandlerMap.Add((int)PacketBodyType.AckDiscon, new AckDisconHandler(sender));
    }

    public override void HandleEvents()
    {
        base.HandleEvents();

        _sender.ChangeState(new SendingFileState(_sender,_filePath)); // Vamos al siguiente estado de envío
    }
    protected override Packet Receive()
    {
        byte[] packetBytes= null;
        packetBytes=_sender.ReceivePacket();
        Packet packet = packetBytes.Decode();// Esto es lo que no sé
        return packet;
    }
}
