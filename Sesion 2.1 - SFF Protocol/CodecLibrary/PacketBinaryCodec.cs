using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodecLibrary
{
    class PacketBinaryCodec : BinaryCodec<Packet>
    {
        public override void WriteBinaryData(BinaryWriter writer, Packet message)
        {
            writer.Write((int)message._type);
            writer.Write(message._bodyLength);
            writer.Write(message._body);
        }

        public override Packet ReadBinaryData(BinaryReader reader)
        {
            int typeRaw = reader.ReadInt32();
            int bodyLength = reader.ReadInt32();

            byte[] body = new byte[bodyLength];
            reader.Read(body, 0, bodyLength);

            return new Packet((PacketBodyType)typeRaw, bodyLength, body);
        }
    }
}
