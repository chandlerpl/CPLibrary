using System;

namespace CP.AILibrary.Storage
{
    public interface IMemory
    {
        bool Setup();

        bool Setup(string Name);

        MemoryContainer GetMemoryContainer();

        //XmlData GetSerialisationData();

        bool AddData(string dataName, object value);
        
        bool AddData(string dataName, Type value);

        bool RemoveData(string dataName);

        bool SetValue(string dataName, object value);

        bool SetDataName(string dataName, string newDataName);

        T GetValue<T>(string dataName);

        Data GetData(string dataName);

        Data GetData(Guid ID);

        bool CheckDataExist(params string[] dataNames);

        Data<T> GetData<T>(string dataName);

        string[] GetDataNames();

        string[] GetDataNames(Type ofType);

        Data[] Values();
    }
}