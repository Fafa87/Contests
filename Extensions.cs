using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace Deadline
{


    public static class RandomExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            // TODO is it not very slow??
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, int seed)
        {
            Random rnd = new Random(seed);
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
    }

    public static class DPExtensions
    {
        public static void SetAllValues<T>(this T[,] table, T value, int maxN, int maxM)
        {
            for (int i = 0; i <= maxN; i++)
                for (int i2 = 0; i2 <= maxM; i2++)
                    table[i, i2] = value;
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
            return keys.All(key => dict.ContainsKey(key));
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
                var newList = new List<TValue>();
                newList.Add(item);
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

    public static class ClientExtensions
    {
        public static void Swap<T>(ref T left, ref T right)
        {
            T temp;
            temp = left;
            left = right;
            right = temp;
        }

        public static List<T> ParseList<T>(this string line)
        {
            List<T> res = new List<T>();
            var tokens = line.Split(' ');
            foreach (var token in tokens)
            {
                res.Add(token.ParseAllTokens<T>());
            }
            return res;
        }

        public static T ParseAllTokens<T>(this string line)
        {
            T res;
            line.ParseToken(out res);
            return res;
        }

        public static Tuple<T1, T2> ParseAllTokens<T1, T2>(this string line)
        {
            T1 res1;
            T2 res2;
            line = line.ParseToken(out res1);
            line = line.ParseToken(out res2);
            return Tuple.Create(res1, res2);
        }

        public static Tuple<T1, T2, T3> ParseAllTokens<T1, T2, T3>(this string line)
        {
            T1 res1;
            T2 res2;
            T3 res3;
            line = line.ParseToken(out res1);
            line = line.ParseToken(out res2);
            line = line.ParseToken(out res3);
            return Tuple.Create(res1, res2, res3);
        }

        public static string ParseToken<T>(this string line, out T parsed)
        {
            var all = line.Split(' ');
            var head = all.First();
            var tail = String.Join(" ", all.Skip(1).ToArray());
            if (typeof(T) == typeof(string))
            {
                parsed = (T)(object)head;
            }
            else if (typeof(T) == typeof(decimal))
            {
                parsed = (T)(object)Decimal.Parse(head, CultureInfo.InvariantCulture);
            }
            else if (typeof(T) == typeof(double))
            {
                parsed = (T)(object)Double.Parse(head, CultureInfo.InvariantCulture);
            }
            else if (typeof(T) == typeof(int))
            {
                parsed = (T)(object)Int32.Parse(head, CultureInfo.InvariantCulture);
            }
            else if (typeof(T) == typeof(long))
            {
                parsed = (T)(object)long.Parse(head, CultureInfo.InvariantCulture);
            }
            else if (typeof(T) == typeof(ulong))
            {
                parsed = (T)(object)ulong.Parse(head, CultureInfo.InvariantCulture);
            }
            else
                throw new NotImplementedException(typeof(T).ToString());
            return tail;
        }

        public static string OutputLine(params object[] data)
        {
            return String.Join(" ", data.Select(d => d.ToString()));
        }

        public static IEnumerable<Tuple<int, T>> WithIndex<T>(this IEnumerable<T> collection)
        {
            return collection.Select((obj, i) => Tuple.Create(i, obj));
        }

        public static T MinElement<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector)
            where T : class
        {
            if (collection.Any() == false)
                return null;
            var minimum = collection.Min(p => selector(p));
            return collection.First(p => selector(p).Equals(minimum));
        }

        public static T MinElement<T>(this IEnumerable<T> collection)
            where T : class
        {
            if (collection.Any() == false)
                return null;
            var minimum = collection.Min();
            return collection.First(p => p == minimum);
        }

        public static T MaxElement<T, TResult>(this IEnumerable<T> collection, Func<T, TResult> selector)
            where T : class
        {
            if (collection.Any() == false)
                return null;
            var minimum = collection.Max(p => selector(p));
            return collection.First(p => selector(p).Equals(minimum));
        }

        public static T MaxElement<T>(this IEnumerable<T> collection)
            where T : class
        {
            if (collection.Any() == false)
                return null;
            var minimum = collection.Max();
            return collection.First(p => p == minimum);
        }

    }
}


