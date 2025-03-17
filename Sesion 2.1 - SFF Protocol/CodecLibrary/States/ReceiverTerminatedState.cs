using System;
using System.Collections.Generic;
using System.Text;
using CodecLibrary.Networking;

namespace CodecLibrary.States
{
    public class ReceiverTerminatedState : ReceiverState
    {
        public ReceiverTerminatedState(Receiver receiver) : base(receiver) { }

        public override void HandleEvents()
        {
            Console.WriteLine("🔴 El receptor ha finalizado la sesión.");
        }

        protected override Packet Receive()
        {
            return null;
        }
    }
}
