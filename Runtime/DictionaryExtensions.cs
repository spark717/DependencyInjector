using System.Collections.Generic;

namespace Spark
{
    internal static class DictionaryExtensions
    {
        public static List<TValue> GetOrCreate<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key)
        {
            var list = dictionary.GetValueOrDefault(key);
            if (list == null)
            {
                list = new List<TValue>();
                dictionary[key] = list;
            }
            return list;
        }
        
        public static HashSet<TValue> GetOrCreate<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dictionary, TKey key)
        {
            var list = dictionary.GetValueOrDefault(key);
            if (list == null)
            {
                list = new HashSet<TValue>();
                dictionary[key] = list;
            }
            return list;
        }
    }
}