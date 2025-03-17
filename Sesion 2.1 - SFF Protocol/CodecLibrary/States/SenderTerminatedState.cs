using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Messages;
using CodecLibrary.Handlers;
using CodecLibrary.Networking;

namespace CodecLibrary.States
{
    public class SenderTerminatedState : SenderState
    {
        public SenderTerminatedState(Sender sender) : base(sender) { }

        public override void HandleEvents()
        {
            Console.WriteLine("🔴 El emisor ha finalizado la sesión.");
        }

        protected override Packet Receive()
        {
            return null;
        }
    }
}
