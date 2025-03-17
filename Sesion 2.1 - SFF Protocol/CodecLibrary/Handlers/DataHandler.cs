using CodecLibrary.Messages;
using CodecLibrary.StateMachine;

namespace CodecLibrary.Handlers
{
    public class DataHandler : IPacketHandler
    {
        private ReceivingFileState _receivingFileState;

        public DataHandler(ReceivingFileState receivingFileState)
        {
            _receivingFileState = receivingFileState;
        }

        public void Handle(Packet packet)
        {
            if (packet is Data dataPacket)
            {
                _receivingFileState.WriteData(dataPacket.Body, dataPacket.BodyLength, dataPacket.SequenceNumber);
            }
        }
    }
}