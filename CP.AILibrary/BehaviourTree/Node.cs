using CP.AILibrary.Storage;
using System.Collections.Generic;

namespace CP.AILibrary.BehaviourTree
{
    [System.Serializable]
    public abstract class Node
    {
        public NodeStates NodeState
        {
            get;
            protected set;
        }

        public abstract NodeStates Evaluate();

        public abstract bool Init(Brain behaviourTree, Memory memory);

        public virtual bool AddChild(Node node)
        {
            return true;
        }
    }
    
    public class Decorator : Node
    {
        public Node node;

        public override NodeStates Evaluate()
        {
            return NodeStates.FAILURE;
        }

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            return false;
        }

        public override bool AddChild(Node node)
        {
            if (this.node != null)
                return false;

            this.node = node;
            return true;
        }
    }

    public class Composite : Node
    {
        public List<Node> nodes = new List<Node>();

        public override NodeStates Evaluate()
        {
            return NodeStates.FAILURE;
        }

        public override bool Init(Brain behaviourTree, Memory memory)
        {
            return false;
        }

        public override bool AddChild(Node node)
        {
            nodes.Add(node);
            return true;
        }
    }

    public enum NodeStates
    {
        SUCCESS,
        FAILURE,
        RUNNING
    }
}
