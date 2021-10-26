using CP.AILibrary.Storage;

namespace CP.AILibrary.BehaviourTree.Nodes
{
    [System.Serializable]
    public class Inverter : Decorator
    {
        public override NodeStates Evaluate()
        {
            NodeStates state = node.Evaluate();

            if (state == NodeStates.RUNNING)
                return NodeStates.RUNNING;

            return state == NodeStates.SUCCESS ? NodeStates.FAILURE : NodeStates.SUCCESS;
        }

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            return node.Init(behaviourTree, memory);
        }
    }
}
