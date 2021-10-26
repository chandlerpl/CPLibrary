using System;

namespace CP.AILibrary.Storage
{
    [Serializable]
    public class Memory : IMemory
    {
        private MemoryContainer _memoryContainer = new MemoryContainer();
        public MemoryContainer memoryContainer
        {
            get
            {
                if (_memoryContainer == null)
                {
                    _memoryContainer = new MemoryContainer();
                    _memoryContainer.Setup("Memory");

                }
                return _memoryContainer;
            }
            protected set => _memoryContainer = value;
        }
        
        public event Action<Data> OnDataAdded { add { memoryContainer.onDataAdded += value; } remove { memoryContainer.onDataAdded -= value; } }
        public event Action<Data> OnDataChanged { add { memoryContainer.onDataChanged += value; } remove { memoryContainer.onDataChanged -= value; } }
        public event Action<Data> OnDataRemoved { add { memoryContainer.onDataRemoved += value; } remove { memoryContainer.onDataRemoved -= value; } }

        public object this[string dataName]
        {
            get => memoryContainer[dataName] != null ? memoryContainer[dataName] : null;
            set => SetValue(dataName, value);
        }

        //Setups do not need bool returns however they are in place for future upgrades?? 
        public bool Setup()
        {
            memoryContainer.Setup();

            return true;
        }

/*        public XmlData GetSerialisationData()
        {
            return null;
        }*/

        public MemoryContainer GetMemoryContainer()
        {
            return memoryContainer;
        }

        public bool Setup(string Name)
        {
            memoryContainer.Setup(Name);

            return true;
        }

        public bool AddData(string dataName, object value)
        {
            return memoryContainer.AddData(dataName, value);
        }
        
        public bool AddData(string dataName, Type value)
        {
            return memoryContainer.AddData(dataName, value);
        }

        public bool RemoveData(string dataName)
        {
            return memoryContainer.RemoveData(dataName);
        }

        public bool SetValue(string dataName, object value)
        {
            return memoryContainer.SetValue(dataName, value);
        }

        public bool SetDataName(string dataName, string newDataName)
        {
            return memoryContainer.SetDataName(dataName, newDataName);
        }

        public T GetValue<T>(string dataName)
        {
            return memoryContainer.GetValue<T>(dataName);
        }

        public Data GetData(string dataName)
        {
            return memoryContainer.GetData(dataName);
        }

        public Data GetData(Guid ID)
        {
            return memoryContainer.GetData(ID);
        }

        public bool CheckDataExist(params string[] dataNames)
        {
            return memoryContainer.CheckDataExist(dataNames);
        }

        public Data<T> GetData<T>(string dataName)
        {
            return memoryContainer.GetData<T>(dataName);
        }

        public string[] GetDataNames()
        {
            return memoryContainer.GetDataNames();
        }

        public string[] GetDataNames(Type ofType)
        {
            return memoryContainer.GetDataNames(ofType);
        }

        public Data[] Values()
        {
            return memoryContainer.Values();
        }
    }
}