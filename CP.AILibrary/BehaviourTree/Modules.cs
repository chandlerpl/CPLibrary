using CP.AILibrary.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CP.AILibrary.BehaviourTree
{
    [Serializable]
    public abstract class Modules
    {
        public bool enabled = true;

        public Brain brain;
        public Memory memory;

        public Guid memoryID;
        public string memoryName;
        public Type memoryType;
        public int Capacity = 1;
        public float maxDuration = 30;

        public void setupStorage(Brain brain, Memory mem)
        {
            memory = mem;
            this.brain = brain;
        }

        public virtual bool Init(Brain brain, Memory memory)
        {
            setupStorage(brain, memory);

            while (!memory.AddData(memoryName, new Dictionary<Brain, ModuleStorage>()))
                memoryName += ".";

            Data data = memory.GetData(memoryName);

            memoryID = data.ID;
            
            return true;
        }
        
        public virtual bool Exit()
        {
            memory.RemoveData(memoryName);

            return true;
        }


        public abstract bool Update();
        
        public void UpdateMemory(float time)
        {
            Dictionary<Brain, ModuleStorage> dict = (Dictionary<Brain, ModuleStorage>)memory.GetData(memoryName).value;

            foreach (Brain unit in dict.Keys.ToArray())
            {
                dict[unit].duration += time;

                if (dict[unit].duration >= maxDuration)
                {
                    dict.Remove(unit);
                }

            }
        }

        public virtual bool Memorise(Brain unit, Data data, float priority = 1)
        {
            Dictionary<Brain, ModuleStorage> dict = (Dictionary<Brain, ModuleStorage>)memory.GetData(memoryName).value;

            if (!dict.ContainsKey(unit))
            { 
                if (dict.Count == Capacity)
                {
                    foreach (Brain u in dict.Keys.ToArray())
                    {
                        if (priority >= dict[u].priority)
                        {
                            dict.Remove(u);
                            dict.Add(unit, new ModuleStorage(data, 0, priority));
                            return true;
                        }
                    }
                }
                else
                {
                    dict.Add(unit, new ModuleStorage(data, 0, priority));
                }
            }
            else
            {
                dict[unit].data = data;
                dict[unit].duration = 0;
                dict[unit].priority = priority;
            }

            return true;
        }

        public virtual bool ContainsUnit(Brain unit)
        {
            Dictionary<Brain, ModuleStorage> dict = (Dictionary<Brain, ModuleStorage>)memory.GetData(memoryName).value;

            return dict.ContainsKey(unit);
        }
    }

    [Serializable]
    public class ModuleStorage
    {
        public Data data;
        public float duration;
        public float priority;
        public bool enabled = true;

        public ModuleStorage(Data data, float dur, float p)
        {
            this.data = data;
            duration = dur;
            priority = p;
        }
    }
}