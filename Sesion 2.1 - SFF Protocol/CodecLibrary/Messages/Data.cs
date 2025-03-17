using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Messages
{

    public class Data : Packet
    {
        public int SequenceNumber { get; }


        public Data(int sequenceNumber, int bodyLength, byte[] body) : base(PacketBodyType.Data, bodyLength, body)
        {
            SequenceNumber = sequenceNumber;
        }
    }
}
