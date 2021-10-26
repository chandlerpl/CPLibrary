using CP.AILibrary.Storage;

namespace CP.AILibrary.BehaviourTree.Nodes
{
    [System.Serializable]
    public class Succeeder : Decorator
    {
        public override NodeStates Evaluate()
        {
            node.Evaluate();
            return NodeStates.SUCCESS;
        }

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            return node.Init(behaviourTree, memory);
        }
    }
}