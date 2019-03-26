using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Deadline
{
    public static class RandomExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, Random rnd)
        {
            // uses Fisher–Yates shuffle, see:
            // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm

            var result = source.ToArray();

            for (int i = result.Length; i > 1; i--)
            {
                int j = rnd.Next(i);
                ClientExtensions.Swap(ref result[i - 1], ref result[j]);
            }

            return result;
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            return source.Randomize(new Random());
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, int seed)
        {
            return source.Randomize(new Random(seed));
        }

        public static IEnumerable<T> RandomizeWeighted<T>(this IEnumerable<T> source, Func<float> weigth)
        {
            throw new NotImplementedException("This should allow to randomize but without ignoring the value of the sample.")
        }
    }

    public static class ClientExtensions
    {
        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        public static List<T> ParseList<T>(this string line)
        {
            List<T> res = new List<T>();
            var tokens = line.Trim().Split(' ');
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
            var tail = string.Join(" ", all.Skip(1).ToArray());
            if (typeof(T) == typeof(string))
            {
                parsed = (T)(object)head;
            }
            else if (typeof(T) == typeof(decimal))
            {
                parsed = (T)(object)decimal.Parse(head, CultureInfo.InvariantCulture);
            }
            else if (typeof(T) == typeof(double))
            {
                parsed = (T)(object)double.Parse(head, CultureInfo.InvariantCulture);
            }
            else if (typeof(T) == typeof(int))
            {
                parsed = (T)(object)int.Parse(head, CultureInfo.InvariantCulture);
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
            return string.Join(" ", data.Select(d => d.ToString()));
        }
    }
}


