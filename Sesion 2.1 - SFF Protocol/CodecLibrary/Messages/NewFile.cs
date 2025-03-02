using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.Messages
{
    class NewFile : Packet
    {
        public string FileName { get; }

        public NewFile(string fileName, int bodyLength, byte[] body) : base(PacketBodyType.NewFile, bodyLength, body)
        {
            FileName = fileName;
        }
    }
}
