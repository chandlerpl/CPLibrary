using System;
using System.Collections.Generic;
using System.Linq;

namespace CP.AILibrary.Storage
{
    public class MemoryContainer
    {
        private Dictionary<string, Data> _memory = new Dictionary<string, Data>();
        protected Dictionary<string, Data> memory
        {
            get
            {
                if (_memory == null)
                    _memory = new Dictionary<string, Data>();
                return _memory;
            }
            set => _memory = value;
        }

        public event Action<Data> onDataAdded;
        public event Action<Data> onDataChanged;
        public event Action<Data> onDataRemoved;

        public object this[string dataName]
        {
            get => memory[dataName] != null ? memory[dataName].value : null;
            set => SetValue(dataName, value);
        }
/*
        public XmlData WriteXml()
        {
            return memory.WriteXml(memory);
        }

        public void ReadXml(MemoryContainer mem, XmlData xmlData)
        {
            memory.ReadXml(mem, xmlData);
        }*/

        public MemoryContainer() { }

        public MemoryContainer(string Name)
        {
            
        }

        //Setups do not need bool returns however they are in place for future upgrades?? 
        public bool Setup()
        {
            return true;
        }

        public bool Setup(string Name)
        {
            return true;
        }
        
        public bool AddData(string dataName, object value)
        {
            if (value == null)
            {
                return false;
            }

            if(AddData(dataName, value.GetType(), false))
            {
                var newData = GetData(dataName);

                if (newData != null)
                {
                    newData.value = value;
                    onDataAdded?.Invoke(newData);
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
        
        public bool AddData(string dataName, Type value, bool triggerInvoke = true)
        {
            Type type = value;

            if (memory.ContainsKey(dataName))
            {
                Data existing = GetData(dataName);
                
                return false;
            }

            var dataType = typeof(Data<>).MakeGenericType(type);
            var newData = (Data)Activator.CreateInstance(dataType);

            newData.Name = dataName;

            if(type == typeof(Type))
            {
                newData.value = typeof(System.Boolean);
            } else if(type == typeof(string))
            {
                newData.value = "";
            } else
            {
                newData.value = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
            }
            
            memory.Add(dataName, newData);
            if(triggerInvoke)
                onDataAdded?.Invoke(newData);
            return true;
        }
        
        public bool RemoveData(string dataName)
        {
            if (memory.TryGetValue(dataName, out Data data) && memory.Remove(dataName))
            {
                onDataRemoved?.Invoke(data);
                return true;
            }
            return false;
        }

        public bool SetValue(string dataName, object value)
        {
            if (!memory.ContainsKey(dataName))
            {
                return AddData(dataName, value);
            }

            memory[dataName].value = value;
            onDataChanged?.Invoke(memory[dataName]);
            return true;
        }

        public bool SetDataName(string dataName, string newDataName)
        {
            memory.ReplaceKey(dataName, newDataName);
            memory[newDataName].Name = newDataName;

            onDataChanged?.Invoke(memory[newDataName]);

            return true;
        }

        public T GetValue<T>(string dataName)
        {
            if (!memory.ContainsKey(dataName))
            {
                return default;
            }

            T val = (memory[dataName] as Data<T>).value;

            if (val == null)
                val = (T)memory[dataName].value;

            if (val == null)
            {
                return default;
            }

            return val;
        }

        public Data GetData(string dataName)
        {
            if (memory.TryGetValue(dataName, out Data var))
                return var;
            return null;
        }

        public Data GetData(Guid ID)
        {
            return memory.Where(w => w.Value.ID == ID).First().Value;
        }

        public bool CheckDataExist(params string[] dataNames)
        {
            if (dataNames.Length == 0)
            {
                return false;
            }
            else if (dataNames.Length == 1)
            {
                bool val = memory.ContainsKey(dataNames[0]);

                return val;
            }
            else
            {
                List<string> missingVars = new List<string>();

                foreach (string dataName in dataNames)
                    if (!memory.ContainsKey(dataName))
                        missingVars.Add(dataName);

                if (missingVars.Count != 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        public Data<T> GetData<T>(string dataName)
        {
            return (Data<T>)GetData(dataName);
        }

        public string[] GetDataNames()
        {
            return memory.Keys.ToArray();
        }

        public string[] GetDataNames(Type ofType)
        {
            return memory.Values.Where(v => v.dataType == ofType).Select(v => v.Name).ToArray();
        }

        public Data[] Values()
        {
            return memory.Values.ToArray();
        }
    }
}