using CodecLibrary.Messages;

namespace CodecLibrary.Messages
{
    public class AckEndOfFile : Packet
    {
        public AckEndOfFile() : base(PacketBodyType.AckEndOfFile, 0, new byte[0])
        {
            // Mensaje vacío, solo confirma la recepción del EOF
        }
    }
}
