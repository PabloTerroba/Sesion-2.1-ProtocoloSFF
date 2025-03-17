using System;
using CodecLibrary.Messages;

namespace CodecLibrary.Messages
{
    public class EndOfFile : Packet
    {
        public EndOfFile() : base(PacketBodyType.EndOfFile, 0, new byte[0])
        {
            // No tiene datos adicionales, solo indica el fin de la transmisión
        }
    }
}

