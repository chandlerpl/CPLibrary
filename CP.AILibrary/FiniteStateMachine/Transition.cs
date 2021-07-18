using System;
using System.Collections.Generic;
using System.Text;

namespace CP.AILibrary.FiniteStateMachine
{
    [Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}
