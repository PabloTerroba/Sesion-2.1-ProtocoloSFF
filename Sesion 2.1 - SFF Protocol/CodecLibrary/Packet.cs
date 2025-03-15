using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary
{
    public enum PacketBodyType
    {
        NewFile = 1,
        AckNewFile = 10,
        Data = 2,
        AckData = 20,
        Discon = 3,
        AckDiscon = 30
    }

    public class Packet
    {
        public PacketBodyType _type; // { get; private set; }
        public int _bodyLength; // { get; private set; }
        public byte[] _body; // { get; private set; }

        public Packet(PacketBodyType type, int bodyLength, byte[] body)
        {
            _type = type;
            _bodyLength = bodyLength;
            _body = body;
        }

        public PacketBodyType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int BodyLength
        {
            get { return _bodyLength; }
            set { _bodyLength = value; }
        }
        public byte[] Body
        {
            get { return _body; }
            set
            {
                _body = value;
                _bodyLength = value.Length;
            }
        }
    }
}
