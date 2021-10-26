using System.Collections.Generic;
using CP.AILibrary.Storage;

namespace CP.AILibrary.BehaviourTree.Nodes
{
    [System.Serializable]
    public class Sequence : Composite
    {
        public override NodeStates Evaluate()
        {
            bool childRunning = false;

            foreach (Node node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.FAILURE:
                        return NodeStates.FAILURE;
                    case NodeStates.SUCCESS:
                        continue;
                    case NodeStates.RUNNING:
                        childRunning = true;
                        continue;
                    default:
                        return NodeStates.SUCCESS;
                }
            }
            return childRunning ? NodeStates.RUNNING : NodeStates.SUCCESS;
        }

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            bool returnState = true;

            foreach (Node node in nodes)
            {
                if (!node.Init(behaviourTree, memory))
                    returnState = false;
            }

            return returnState;
        }
    }
}