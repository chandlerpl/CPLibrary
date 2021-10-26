using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CP.AILibrary.Storage;

[Serializable]
public class DataItem
{
    public string Key;
    public string ValueType;
    public string Value;
    
    public DataItem()
    {

    }

    public DataItem(string key, string value, string valueType)
    {
        Key = key;
        ValueType = valueType;
        Value = value;
    }
}
/**
[Serializable]
public class XmlData
{
    [SerializeField]
    public string _serializedBlackboard = "";
    [SerializeField]
    public List<UnityEngine.Object> _objectReferences = new List<UnityEngine.Object>();
    [SerializeField]
    public bool reserialise = false;
}
[XmlRoot("Dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    #region IXmlSerializable Members
    public void ReadXml(MemoryContainer memory, XmlData xmlData)
    {
        if (xmlData._serializedBlackboard == "" || xmlData == null)
            return;
        
        List<DataItem> dataItems = new List<DataItem>();

        XmlSerializer xs = new XmlSerializer(typeof(List<DataItem>));
        
        using (StringReader sr = new StringReader(xmlData._serializedBlackboard))
        {
            dataItems = (List<DataItem>)xs.Deserialize(sr);
        }

        foreach(DataItem data in dataItems)
        {
            Type type = ReflectionTools.GetType(data.ValueType);
            
            if (typeof(UnityEngine.Object).IsAssignableFrom(type) || type.IsInterface)
            {
                UnityEngine.Object obj = xmlData._objectReferences[int.Parse(data.Value)];

                if(obj == null)
                {
                    memory.AddData(data.Key, type);
                } else
                {
                    memory.AddData(data.Key, xmlData._objectReferences[int.Parse(data.Value)]);
                }
            }
            else if (type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Vector4) || type == typeof(Quaternion))
            {
                string[] values = data.Value.Replace("(", "").Replace(")", "").Split(',');
                List<float> floatValues = new List<float>();

                Array.ForEach(values, s => { floatValues.Add(float.Parse(s)); });
                
                if(floatValues.Count == 2)
                {
                    memory.AddData(data.Key, new Vector2(floatValues[0], floatValues[1]));
                } else if (floatValues.Count == 3)
                {
                    memory.AddData(data.Key, new Vector3(floatValues[0], floatValues[1], floatValues[2]));
                } else if (floatValues.Count == 4)
                {
                    if(type == typeof(Vector4))
                        memory.AddData(data.Key, new Vector4(floatValues[0], floatValues[1], floatValues[2], floatValues[3]));
                    else
                        memory.AddData(data.Key, new Quaternion(floatValues[0], floatValues[1], floatValues[2], floatValues[3]));
                } else
                {
                    Debug.Log("Unknown number of values: " + floatValues.Count);
                }
            } 
            else if (type == typeof(Vector2Int) || type == typeof(Vector3Int))
            {
                string[] values = data.Value.Replace("(", "").Replace(")", "").Split(',');
                List<int> intValues = new List<int>();

                Array.ForEach(values, s => { intValues.Add(int.Parse(s)); });

                if (intValues.Count == 2)
                {
                    memory.AddData(data.Key, new Vector2Int(intValues[0], intValues[1]));
                }
                else if (intValues.Count == 3)
                {
                    memory.AddData(data.Key, new Vector3Int(intValues[0], intValues[1], intValues[2]));
                }
                else
                {
                    Debug.Log("Unknown number of values: " + intValues.Count);
                }
            }
            else if (type == typeof(Type))
            {
                if(memory.AddData(data.Key, typeof(Type)))
                    memory.SetValue(data.Key, ReflectionTools.GetType(data.Value));
            }
            else if (type == typeof(AnimationCurve))
            {
                if(data.Value == "No keys.")
                {
                    memory.AddData(data.Key, new AnimationCurve());
                } else
                {
                    string[] values = data.Value.Split(':');
                    List<Keyframe> keys = new List<Keyframe>();

                    foreach(string value in values)
                    {
                        string[] vals = value.Split(',');
                        List<float> floatValues = new List<float>();
                        Array.ForEach(vals, s => { floatValues.Add(float.Parse(s)); });

                        keys.Add(new Keyframe(floatValues[0], floatValues[1], floatValues[2], floatValues[3], floatValues[4], floatValues[5]));
                    }

                    memory.AddData(data.Key, new AnimationCurve(keys.ToArray()));
                }
            }
            else if (type == typeof(Color))
            {
                string[] values = data.Value.Split(',');
                List<float> floatValues = new List<float>();
                Array.ForEach(values, s => { floatValues.Add(float.Parse(s)); });
                
                memory.AddData(data.Key, new Color(floatValues[0], floatValues[1], floatValues[2], floatValues[3]));
            }
            else if (type == typeof(LayerMask))
            {
                memory.AddData(data.Key, new LayerMask { value = int.Parse(data.Value) });
            }
            else if (type == typeof(RaycastHit) || type == typeof(RaycastHit2D))
            {
                memory.AddData(data.Key, type);
            }
            else if (type.IsAbstract)
            {
                Debug.Log("Not yet implemented");
            }
            else
            {
                memory.AddData(data.Key, Convert.ChangeType(data.Value, type));
            }
        }

    }
    
    public XmlData WriteXml(Dictionary<string, Data> dict)
    {
        List<DataItem> dataItems = new List<DataItem>(Count);

        XmlData xmlStorage = new XmlData();

        foreach (KeyValuePair<string, Data> kv in dict)
        {
            string name = kv.Key;
            object value = kv.Value.value;
            var type = kv.Value.dataType;
            string typeName = type.FullName;

            DataItem data = new DataItem();

            if (typeof(UnityEngine.Object).IsAssignableFrom(type) || type.IsInterface)
            {
                xmlStorage._objectReferences.Add((UnityEngine.Object) value);

                data = new DataItem(name, (xmlStorage._objectReferences.Count - 1).ToString(), typeName);
            }
            else if (type == typeof(Type))
            {
                data = new DataItem(name, value.ToString(), typeName);
            }
            else if (type == typeof(AnimationCurve))
            {
                if (((AnimationCurve)value).keys.Length > 0)
                {
                    string str = "";

                    foreach (Keyframe kf in ((AnimationCurve)value).keys)
                    {
                        str += kf.time + "," + kf.value + "," + kf.inTangent + "," + kf.outTangent + "," + kf.inWeight + "," + kf.outWeight + ":";
                    }
                    
                    data = new DataItem(name, str.Substring(0, str.Length-1), typeName);
                } else
                {
                    data = new DataItem(name, "No keys.", typeName);
                }
            }
            else if (type == typeof(Color))
            {
                data = new DataItem(name, value.ToString().Replace("RGBA(", "").Replace(")", "").Replace(" ", ""), typeName);
            }
            else if (type == typeof(LayerMask))
            {
                data = new DataItem(name, ((LayerMask)value).value.ToString(), typeName);
            }
            else if (type == typeof(RaycastHit) || type == typeof(RaycastHit2D))
            {
                data = new DataItem(name, "Data can not be stored.", typeName);
            }
            else if (type.IsAbstract)
            {
                Debug.Log("Not yet implemented");
            }
            else
            {
                data = new DataItem(name, type == typeof(string) ? (string)value : value.ToString(), typeName);
            }

            dataItems.Add(data);
        }
        var xs = new XmlSerializer(typeof(List<DataItem>));
        var ws = new XmlWriterSettings { Indent = true,  OmitXmlDeclaration = true };

        var sb = new StringBuilder();
        using (XmlWriter writer = XmlWriter.Create(sb, ws))
        {
            xs.Serialize(writer, dataItems);
        }
        
        xmlStorage._serializedBlackboard = sb.ToString();
        
        //Add a new content checker, no point regenerating when no new content is added!

        return xmlStorage;
    }

    #endregion
}

public static class ReflectionTools
{
    public static Type GetType(string TypeName)
    {
        var type = Type.GetType(TypeName);
        
        if (type != null)
            return type;
        
        var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));
        
        var assembly = Assembly.LoadWithPartialName(assemblyName);
        if (assembly == null)
            return null;
        
        return assembly.GetType(TypeName);

    }


    private static Assembly[] _loadedAssemblies;
    private static Assembly[] loadedAssemblies
    {
        get
        {
            if (_loadedAssemblies == null)
            {
                _loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            return _loadedAssemblies;
        }
    }

    private static Type[] _allTypes;
    public static Type[] GetAllTypes(bool includeObsolete)
    {
        if (_allTypes != null)
        {
            return _allTypes;
        }

        var result = new List<Type>();
        for (var i = 0; i < loadedAssemblies.Length; i++)
        {
            var asm = loadedAssemblies[i];
            try { result.AddRange(asm.GetExportedTypes().Where(t => includeObsolete == true || !t.IsDefined(typeof(ObsoleteAttribute), true))); }
            catch { continue; }
        }
        return _allTypes = result.OrderBy(t => t.Name).OrderBy(t => t.Namespace).ToArray();
    }

    public static string FriendlyName(this Type t, bool compileSafe = false)
    {

        if (t == null)
        {
            return null;
        }

        if (!compileSafe && t.IsByRef)
        {
            t = t.GetElementType();
        }

        //if (!compileSafe && t == typeof(UnityEngine.Object))
        //{
        //    return "UnityObject";
        //}

        var s = compileSafe ? t.FullName : t.Name;
        if (!compileSafe)
        {
            if (s == "Single") { s = "Float"; }
            if (s == "Single[]") { s = "Float[]"; }
            if (s == "Int32") { s = "Integer"; }
            if (s == "Int32[]") { s = "Integer[]"; }
        }

        if (t.IsGenericParameter)
        {
            s = "T";
        }

        if (t.IsGenericType)
        {
            s = compileSafe ? (t.Namespace + "." + t.Name) : t.Name;
            var args = t.GetGenericArguments();
            if (args.Length != 0)
            {

                s = s.Replace("`" + args.Length.ToString(), "");

                s += compileSafe ? "<" : " (";
                for (var i = 0; i < args.Length; i++)
                {
                    s += (i == 0 ? "" : ", ") + args[i].FriendlyName(compileSafe);
                }
                s += compileSafe ? ">" : ")";
            }
        }

        return s;
    }
}
**/