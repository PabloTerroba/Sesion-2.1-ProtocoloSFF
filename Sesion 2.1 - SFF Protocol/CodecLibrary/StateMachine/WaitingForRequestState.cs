using CodecLibrary;
using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;
using System.Net.Sockets;

public class WaitingForRequestState : ReceiverState
{
    public WaitingForRequestState(Receiver receiver) : base(receiver)
    {
    }

    public override void HandleEvents()
    {
        base.HandleEvents();
    }
    protected override Packet Receive()
    {
        byte[] packetBytes = null;
        packetBytes = _receiver.ReceivePacket();
        Packet packet = packetBytes.Decode();// Esto es lo que no sé
        return packet;
    }
}
