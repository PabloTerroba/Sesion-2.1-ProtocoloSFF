using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.StateMachine
{
    public abstract class ReceiverState : State
    {
        protected Receiver _receiver;

        // Constructor para inicializar el contexto (Receiver)
        public ReceiverState(Receiver receiver)
        {
            _receiver = receiver;
        }
   }
}
