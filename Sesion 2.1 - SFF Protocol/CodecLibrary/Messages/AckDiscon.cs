using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Messages
{
    class AckDiscon : Packet
    {
        public AckDiscon(int bodyLength, byte[] body) : base(PacketBodyType.AckDiscon, bodyLength, body) { }
    }
}
