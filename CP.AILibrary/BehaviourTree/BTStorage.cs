using CP.AILibrary.Storage;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CP.AILibrary.BehaviourTree
{
    [Serializable]
    public class BTStorage
    {
        public string xml;

        public Node rootNode;

        public BTStorage(string xml)
        {
            this.xml = xml;

            //rootNode = NodeXmlReader.Generate(xml);
        }

        public void Init(Brain bt, Memory memory)
        {
            rootNode.Init(bt, memory);
        }

        public NodeStates Evaluate()
        {
            return rootNode.Evaluate();
        }

    }
}