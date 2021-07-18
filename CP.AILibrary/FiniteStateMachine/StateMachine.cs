using System;
using System.Collections.Generic;
using System.Text;

namespace CP.AILibrary.FiniteStateMachine
{
    [Serializable]
    public class StateMachine
    {
        public State startingState;

        public State CurrentState
        {
            get => currentState;
            set
            {
                if(value != null)
                {
                    if(currentState != null)
                        currentState.ExitState(this);
                    currentState = value;
                    currentState.EnterState(this);
                }
            }
        }
        private State currentState;
        private bool isActive = true;

        public void Start()
        {
            currentState = startingState;
        }

        public void Update()
        {
            if (!isActive)
                return;

            if (currentState == null)
            {
                isActive = false;
                return;
            }

            currentState.UpdateState(this);
        }
    }
}
