using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CP.AILibrary
{
    public static class DictionaryUtils
    {
        public static void ReplaceKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey oldKey, TKey newKey)
        {
            TValue value;
            if (!dict.TryGetValue(oldKey, out value))
                return;

            dict.Remove(oldKey);
            dict[newKey] = value;
        }

        public static string NamespaceToPath(this Type type)
        {
            if (type == null) { return string.Empty; }
            return string.IsNullOrEmpty(type.Namespace) ? "No Namespace" : type.Namespace.Split('.').First();
        }

        public static void ReplaceKey(this IDictionary dict, object oldKey, object newKey)
        {
            object value = dict[oldKey];

            dict.Remove(oldKey);
            dict[newKey] = value;
        }
    }
}
