using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Messages;

namespace CodecLibrary.Codecs
{
    class AckNewFileCodec : BinaryCodec<AckNewFile>
    {
        public override void WriteBinaryData(BinaryWriter writer, AckNewFile message)
        {
            writer.Write(0); // No hay datos adicionales, escribimos un bodyLength de 0
        }

        public override AckNewFile ReadBinaryData(BinaryReader reader)
        {
            int bodyLength = reader.ReadInt32();
            byte[] body = reader.ReadBytes(bodyLength);
            return new AckNewFile(bodyLength, body);
        }
    }
}
