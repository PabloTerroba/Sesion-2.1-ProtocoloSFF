using CodecLibrary;
using CodecLibrary.Messages;
using CodecLibrary.StateMachine;
using System;

public class AckDataHandler : IPacketHandler
{
    private Sender _sender;
    private SendingFileState _sendingState;

    public AckDataHandler(Sender sender, SendingFileState sendingState)
    {
        _sender = sender;
        _sendingState = sendingState;
    }

    public void Handle(Packet packet)
    {
        if (packet is AckData ackDataPacket)
        {
            _sendingState.AcknowledgePacket(ackDataPacket.SequenceNumber);
        }
    }
}
