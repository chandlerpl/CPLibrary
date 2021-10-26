using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CP.AILibrary.Storage
{
    /*
     * 
     * This is rather messy :/
     * 
     */

    public class GlobalMemory : SingletonBase<GlobalMemory>, IMemory
    {
        //public XmlData serialisationData;
        
        private MemoryContainer _memoryContainer = new MemoryContainer();
        public MemoryContainer memoryContainer
        {
            get
            {
                if (_memoryContainer == null)
                {
                    _memoryContainer = new MemoryContainer();
                    _memoryContainer.Setup("Global Memory");

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

        public void Awake()
        {
            _memoryContainer.Setup("Global Memory");
        }
        
        public static bool AddData(string dataName, object value)
        {
            return Instance.memoryContainer.AddData(dataName, value);
        }

        public static bool AddData(string dataName, Type value)
        {
            return Instance.memoryContainer.AddData(dataName, value);
        }

        public static bool RemoveData(string dataName)
        {
            return Instance.memoryContainer.RemoveData(dataName);
        }

        public static bool SetValue(string dataName, object value)
        {
            return Instance.memoryContainer.SetValue(dataName, value);
        }

        public static T GetValue<T>(string dataName)
        {
            return Instance.memoryContainer.GetValue<T>(dataName);
        }

        public static Data GetData(string dataName)
        {
            return Instance.memoryContainer.GetData(dataName);
        }

        public static Data GetData(Guid ID)
        {
            return Instance.memoryContainer.GetData(ID);
        }

        public static bool CheckDataExist(params string[] dataNames)
        {
            return Instance.memoryContainer.CheckDataExist(dataNames);
        }

        public static Data<T> GetData<T>(string dataName)
        {
            return Instance.memoryContainer.GetData<T>(dataName);
        }

        public static string[] GetDataNames()
        {
            return Instance.memoryContainer.GetDataNames();
        }

        public static string[] GetDataNames(Type ofType)
        {
            return Instance.memoryContainer.GetDataNames(ofType);
        }

        bool IMemory.Setup()
        {
            memoryContainer.Setup();

            return true;
        }

        bool IMemory.Setup(string Name)
        {
            memoryContainer.Setup(Name);

            return true;
        }

        bool IMemory.AddData(string dataName, object value)
        {
            return memoryContainer.AddData(dataName, value);
        }

        MemoryContainer IMemory.GetMemoryContainer()
        {
            return memoryContainer;
        }

/*        XmlData IMemory.GetSerialisationData()
        {
            return serialisationData;
        }*/

        bool IMemory.AddData(string dataName, Type value)
        {
            return memoryContainer.AddData(dataName, value);
        }

        bool IMemory.RemoveData(string dataName)
        {
            return memoryContainer.RemoveData(dataName);
        }

        bool IMemory.SetValue(string dataName, object value)
        {
            return memoryContainer.SetValue(dataName, value);
        }

        bool IMemory.SetDataName(string dataName, string newDataName)
        {
            return memoryContainer.SetDataName(dataName, newDataName);
        }

        T IMemory.GetValue<T>(string dataName)
        {
            return memoryContainer.GetValue<T>(dataName);
        }

        Data IMemory.GetData(string dataName)
        {
            return memoryContainer.GetData(dataName);
        }

        Data IMemory.GetData(Guid ID)
        {
            return memoryContainer.GetData(ID);
        }

        bool IMemory.CheckDataExist(params string[] dataNames)
        {
            return memoryContainer.CheckDataExist(dataNames);
        }

        Data<T> IMemory.GetData<T>(string dataName)
        {
            return memoryContainer.GetData<T>(dataName);
        }

        string[] IMemory.GetDataNames()
        {
            return memoryContainer.GetDataNames();
        }

        string[] IMemory.GetDataNames(Type ofType)
        {
            return memoryContainer.GetDataNames(ofType);
        }

        Data[] IMemory.Values()
        {
            return memoryContainer.Values();
        }


    }
}