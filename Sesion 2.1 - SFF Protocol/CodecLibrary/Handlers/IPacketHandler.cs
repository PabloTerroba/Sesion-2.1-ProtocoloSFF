using System;
using System.Collections.Generic;
using System.Text;


namespace CodecLibrary.Handlers
{
    public interface IPacketHandler
    {
        void Handle(Packet packet);
    }
}
