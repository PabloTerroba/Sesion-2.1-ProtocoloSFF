using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Messages;

namespace CodecLibrary.Codecs
{
    class AckDataCodec : BinaryCodec<AckData>
    {
        public override void WriteBinaryData(BinaryWriter writer, AckData message)
        {
            writer.Write(message.SequenceNumber);
        }

        public override AckData ReadBinaryData(BinaryReader reader)
        {
            int bodyLength = reader.ReadInt32();
            byte[] body = reader.ReadBytes(bodyLength);
            using (MemoryStream ms = new MemoryStream(body))
            using (BinaryReader br = new BinaryReader(ms))
            {
                int sequenceNumber = br.ReadInt32();
                return new AckData(sequenceNumber, bodyLength, body);
            }
        }
    }
}
