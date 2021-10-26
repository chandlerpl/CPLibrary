using System.Collections.Generic;
using CP.AILibrary.Storage;

namespace CP.AILibrary.BehaviourTree
{
    public class Task : Node
    {
        protected Brain behaviourTree;
        protected Memory memory;

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            this.behaviourTree = behaviourTree;
            this.memory = memory;

            return true;
        }

        public override NodeStates Evaluate()
        {
            return NodeStates.FAILURE;
        }
    }

    public class DebugTask : Task
    {
        private string text = "";

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            this.behaviourTree = behaviourTree;
            this.memory = memory;

            return true;
        }

        public override NodeStates Evaluate()
        {
            return NodeStates.SUCCESS;
        }
    }
}
