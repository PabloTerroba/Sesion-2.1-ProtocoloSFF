using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Messages
{
    class AckNewFile : Packet
    {
        public AckNewFile(int bodyLength, byte[] body) : base(PacketBodyType.AckNewFile, bodyLength, body)
        {

        }
    }
}
