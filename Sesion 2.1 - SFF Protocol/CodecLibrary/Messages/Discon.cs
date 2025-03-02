using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Messages
{
    class Discon : Packet
    {
        public Discon(int bodyLength, byte[] body) : base(PacketBodyType.Discon, bodyLength, body) { }
    }
}
