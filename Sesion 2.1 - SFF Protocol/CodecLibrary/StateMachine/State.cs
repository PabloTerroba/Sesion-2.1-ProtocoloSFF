using CodecLibrary.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodecLibrary.StateMachine
{
    public abstract class State
    {
        public abstract void HandleEvents();

    }

}
