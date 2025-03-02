using System;
using System.IO;

namespace CodecLibrary
{
    abstract class ICodec<T>
    {
        public abstract byte[] Encode(T message);
        public abstract T Decode(Stream source);

        public T Decode(byte[] packet)
        {
            using (Stream payload = new MemoryStream(packet, 0, packet.Length, false))
                return Decode(payload);
        }
    }
}
