using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Algorithms
{
    public static class Utils
    {
        public static T Min<T>(params T[] nums)
        {
            return nums.Min();
        }

        public static T Max<T>(params T[] nums)
        {
            return nums.Max();
        }

        public static T MinElement<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector)
        {
            if (collection.Any() == false)
                return default(T);
            var minimum = collection.Min(p => selector(p));
            return collection.First(p => selector(p).Equals(minimum));
        }

        public static T MinElement<T>(this IEnumerable<T> collection)
        {
            if (collection.Any() == false)
                return default(T);
            return collection.Min();
        }

        public static T MaxElement<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector)
        {
            if (collection.Any() == false)
                return default(T);
            var minimum = collection.Max(p => selector(p));
            return collection.First(p => selector(p).Equals(minimum));
        }

        public static T MaxElement<T>(this IEnumerable<T> collection)
        {
            if (collection.Any() == false)
                return default(T);
            return collection.Max();
        }

        public static T DeepClone<T>(this T obj)
            where T : class
        {
            T objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms) as T;
            }
            return objResult;
        }
    }

    public static class DictExtensions
    {
        public static TValue GetOrNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (!dict.ContainsKey(key))
                dict[key] = new TValue();
            return dict[key];
        }

        public static TValue GetOrNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> creator)
        {
            if (!dict.ContainsKey(key))
                dict[key] = creator();
            return dict[key];
        }

        public static TValue GetOrNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> creator)
        {
            if (!dict.ContainsKey(key))
                dict[key] = creator(key);
            return dict[key];
        }

        public static bool ContainsAllKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<TKey> keys)
        {
            return keys.All(dict.ContainsKey);
        }

        public static void Increment<TKey>(this Dictionary<TKey, int> dict, TKey key, int step)
        {
            if (dict.ContainsKey(key) == false)
                dict[key] = step;
            else
                dict[key] = dict[key] + step;
        }

        public static void Increment<TKey>(this Dictionary<TKey, int> dest, Dictionary<TKey, int> source)
        {
            foreach (KeyValuePair<TKey, int> pair in source)
            {
                dest.Increment(pair.Key, pair.Value);
            }
        }

        public static Dictionary<TKey, int> Summate<TKey>(this Dictionary<TKey, int> dict0, Dictionary<TKey, int> dict1)
        {
            var result = new Dictionary<TKey, int>(dict0);
            result.Increment(dict1);
            return result;
        }

        public static void Increment<TKey>(this Dictionary<TKey, double> dict, TKey key, double step)
        {
            if (dict.ContainsKey(key) == false)
                dict[key] = step;
            else
                dict[key] = dict[key] + step;
        }

        public static void AppendItem<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key, TValue item)
        {
            if (dict.ContainsKey(key) == false)
            {
                var newList = new List<TValue> { item };
                dict[key] = newList;
            }
            else
                dict[key].Add(item);
        }

        public static List<TValue> GetOrEmpty<TKey, TValue>(this Dictionary<TKey, List<TValue>> dict, TKey key)
        {
            List<TValue> res;
            if (dict.TryGetValue(key, out res) == false)
                res = new List<TValue>();
            return res;
        }

        public static TValue[] GetOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue[]> dict, TKey key)
        {
            TValue[] res;
            if (dict.TryGetValue(key, out res) == false)
                res = new TValue[0];
            return res;
        }

        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
            where TValue : class
        {
            TValue res;
            if (dict.TryGetValue(key, out res) == false)
                res = null;
            return res;
        }

        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            TValue res;
            if (dict.TryGetValue(key, out res) == false)
                res = value;
            return res;
        }
    }
}
