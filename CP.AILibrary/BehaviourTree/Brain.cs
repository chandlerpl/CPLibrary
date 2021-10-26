using CP.AILibrary.Storage;
using System;
using System.Collections.Generic;

namespace CP.AILibrary.BehaviourTree
{
    [Serializable]
    public class Brain
    {
        public BTStorage treeStorage;
        
        private bool memoryNotRequired = false;

        public Memory memory;
        public Memory Memory { get => memory; private set => memory = value; }
        
        //private Unit unit;
        //public Unit Unit { get => unit; private set => unit = value; }


        public bool MemoryNotRequired
        {
            get => memoryNotRequired;
        }

        public List<Modules> modules = new List<Modules>();


        public void Start()
        {
            //modules.Add(new SightModule() { Capacity = 1, checkFrequency = 1, detectionTime = 5f, distance = 10, fieldOfView = 90, eyeHeight = new Vector3(0, 1.8f, 0), memoryName = "Sight", memoryType = typeof(Vector3) });
            //modules.Add(new HearingModule() { memoryName = "Hearing", memoryType = typeof(Vector3), hearingAbility = 1 });

            if(!MemoryNotRequired)
            {
                if(memory == null)
                {
                    memory = new Memory();
                }
                
                memory.Setup();
            }
            
            if (treeStorage != null)
                treeStorage.Init(this, memory);
            
            foreach (Modules module in modules)
            {
                if(module.enabled && !module.Init(this, memory))
                    module.enabled = false;
            }
        }

        public void Update()
        {
            foreach (Modules module in modules)
                if (module.enabled)
                    module.Update();

            if(treeStorage != null)
                treeStorage.Evaluate();
        }
    }
}
