using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodecLibrary.Handlers;
using CodecLibrary.Messages;
using CodecLibrary.Networking;


namespace CodecLibrary.States
{
    public abstract class ReceiverState : State
    {
        protected Receiver _receiver;

        public ReceiverState(Receiver receiver)
        {
            _receiver = receiver;
        }

        protected void WriteDataToFile(string filePath, Data data)
        {
            Console.WriteLine($"💾 Escribiendo datos en {filePath}");
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                fs.Write(data.Content, 0, data.Content.Length);
            }
        }

        protected void ChangeState(ReceiverState newState)
        {
            Console.WriteLine($"🔄 Cambio de estado: {this.GetType().Name} → {newState.GetType().Name}");
            _receiver.ChangeState(newState);
        }

        protected override Packet Receive()
        {
            return _receiver.ReceivePacket();
        }
    }

}
