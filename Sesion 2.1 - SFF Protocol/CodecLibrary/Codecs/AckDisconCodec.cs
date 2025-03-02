using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Messages;

namespace CodecLibrary.Codecs
{
    class AckDisconCodec : BinaryCodec<AckDiscon>
    {
        public override void WriteBinaryData(BinaryWriter writer, AckDiscon message)
        {
            writer.Write(0); // No hay datos adicionales
        }

        public override AckDiscon ReadBinaryData(BinaryReader reader)
        {
            int bodyLength = reader.ReadInt32();
            byte[] body = reader.ReadBytes(bodyLength);
            return new AckDiscon(bodyLength, body);
        }
    }
}
