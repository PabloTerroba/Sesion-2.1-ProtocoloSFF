using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Networking;


namespace CodecLibrary.Handlers
{
    public interface IPacketHandler
    {
        void Handle(Packet packet);
    }
}
