using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

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
        return source.Randomize(seed);
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
        foreach(var token in tokens)
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
        return Tuple.Create(res1,res2);
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

}




