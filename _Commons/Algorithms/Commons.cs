using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class Commons
    {
        public static int FindFirstIndexGreaterThanOrEqualTo<T>(
            this IList<T> sortedCollection, int key, Func<T,int> extractor
        ) //where T : IComparable<T>
        {
            int begin = 0;
            int end = sortedCollection.Count;
            while (end > begin)
            {
                int index = (begin + end) / 2;
                T el = sortedCollection[index];
                if (extractor(el).CompareTo(key) >= 0)
                    end = index;
                else
                    begin = index + 1;
            }
            return end;
        }

        public static IEnumerable<Tuple<int, T>> WithIndex<T>(this IEnumerable<T> collection)
        {
            return collection.Select((obj, i) => Tuple.Create(i, obj));
        }

        public static int GetLowerBound<T>(this List<T> l, T value)
        {
            var num = l.BinarySearch(value);
            return num >= 0 ? num : ~num;
        }

        public static List<T> FindLIS<T>(this IList<T> nums)
        {
            var orderednum = new List<T>();
            if (nums == null)
                return orderednum;

            foreach (var num in nums)
            {
                var index = orderednum.BinarySearch(num);
                if (index < 0) index = -(index + 1);

                if (index == orderednum.Count) orderednum.Add(num);
                else orderednum[index] = num;
            }

            return orderednum;
        }
    }
}
