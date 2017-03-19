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

        public static int GetLowerBound<T>(this List<T> l, T value)
        {
            var num = l.BinarySearch(value);
            return num >= 0 ? num : ~num;
        }

        public static int Max(params int[] nums)
        {
            int res = nums[0];
            for(int i=1;i<nums.Length;i++)
                res = Math.Max(res, nums[i]);
            return res;
        }

        public static double Max(params double[] nums)
        {
            double res = nums[0];
            for (int i = 1; i < nums.Length; i++)
                res = Math.Max(res, nums[i]);
            return res;
        }

        public static int Min(params int[] nums)
        {
            int res = nums[0];
            for (int i = 1; i < nums.Length; i++)
                res = Math.Min(res, nums[i]);
            return res;
        }

        public static double Min(params double[] nums)
        {
            double res = nums[0];
            for (int i = 1; i < nums.Length; i++)
                res = Math.Min(res, nums[i]);
            return res;
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
