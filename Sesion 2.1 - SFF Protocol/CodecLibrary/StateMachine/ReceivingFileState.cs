using CodecLibrary;
using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;
using System.IO;
using System.Net.Sockets;

public class ReceivingFileState : ReceiverState
{
    private FileStream _fileStream;

    public ReceivingFileState(Receiver receiver) : base(receiver)
    {
    }

    public override void HandleEvents()
    {
        base.HandleEvents();
    }

    protected override Packet Receive()
    {
        byte[] packetBytes = _receiver.ReceivePacket();
        Packet packet = packetBytes.Decode();
        return packet;
    }

}
