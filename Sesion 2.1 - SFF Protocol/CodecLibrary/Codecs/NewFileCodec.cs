using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Messages;

namespace CodecLibrary.Codecs
{
    class NewFileCodec : BinaryCodec<NewFile>
    {
        public override void WriteBinaryData(BinaryWriter writer, NewFile message)
        {
            writer.Write(message.FileName);
        }

        public override NewFile ReadBinaryData(BinaryReader reader)
        {
            int bodyLength = reader.ReadInt32();
            byte[] body = reader.ReadBytes(bodyLength);
            string fileName = Encoding.UTF8.GetString(body);
            return new NewFile(fileName, bodyLength, body);
        }
    }
}
