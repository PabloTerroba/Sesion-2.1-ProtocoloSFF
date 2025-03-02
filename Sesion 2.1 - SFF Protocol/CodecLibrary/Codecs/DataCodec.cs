using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Messages;

namespace CodecLibrary.Codecs
{
    class DataCodec : BinaryCodec<Data>
    {
        public override void WriteBinaryData(BinaryWriter writer, Data message)
        {
            writer.Write(message.SequenceNumber);
            writer.Write(message.Content.Length);
            writer.Write(message.Content);
        }

        public override Data ReadBinaryData(BinaryReader reader)
        {
            int bodyLength = reader.ReadInt32();
            byte[] body = reader.ReadBytes(bodyLength);
            using (MemoryStream ms = new MemoryStream(body))
            using (BinaryReader br = new BinaryReader(ms))
            {
                int sequenceNumber = br.ReadInt32();
                int length = br.ReadInt32();
                byte[] content = br.ReadBytes(length);
                return new Data(sequenceNumber, content, bodyLength, body);
            }
        }
    }
}
