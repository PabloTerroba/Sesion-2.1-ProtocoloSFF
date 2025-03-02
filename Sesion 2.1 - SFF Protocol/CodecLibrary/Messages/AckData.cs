using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Messages
{
    class AckData : Packet
    {
        public int SequenceNumber { get; }

        public AckData(int sequenceNumber, int bodyLength, byte[] body) : base(PacketBodyType.AckData, bodyLength, body)
        {
            SequenceNumber = sequenceNumber;
        }
    }

}
