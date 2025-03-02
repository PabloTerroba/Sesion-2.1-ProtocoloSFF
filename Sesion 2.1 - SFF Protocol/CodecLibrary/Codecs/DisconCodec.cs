using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Messages;

namespace CodecLibrary.Codecs
{
    class DisconCodec : BinaryCodec<Discon>
    {
        public override void WriteBinaryData(BinaryWriter writer, Discon message)
        {
            writer.Write(0); // No hay datos adicionales
        }

        public override Discon ReadBinaryData(BinaryReader reader)
        {
            int bodyLength = reader.ReadInt32();
            byte[] body = reader.ReadBytes(bodyLength);
            return new Discon(bodyLength, body);
        }
    }
}
