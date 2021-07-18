using System;
using System.Collections.Generic;
using System.Text;

namespace CP.AILibrary.FiniteStateMachine
{
    public abstract class Decision
    {
        public abstract bool Decide(StateMachine stateMachine);
    }
}
