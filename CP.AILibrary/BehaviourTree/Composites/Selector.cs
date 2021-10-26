using CP.AILibrary.Storage;
using System.Collections.Generic;

namespace CP.AILibrary.BehaviourTree.Nodes
{

    [System.Serializable]
    public class Selector : Composite
    {
        public override NodeStates Evaluate()
        {
            foreach (Node node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.FAILURE:
                        continue;
                    case NodeStates.SUCCESS:
                        return NodeStates.SUCCESS;
                    case NodeStates.RUNNING:
                        return NodeStates.RUNNING;
                    default:
                        continue;
                }
            }

            return NodeStates.FAILURE;
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