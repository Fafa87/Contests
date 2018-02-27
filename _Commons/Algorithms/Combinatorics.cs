using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Algorithms
{
    public static class Combinatorics
    {
        // code from http://stackoverflow.com/questions/11208446/generating-permutations-of-a-set-most-efficiently
        public static bool NextPermutation<T>(T[] numList)
            where T : IComparable<T>
        {
            /*
             Knuths
             1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
             2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
             3. Swap a[j] with a[l].
             4. Reverse the sequence from a[j + 1] up to and including the final element a[n].
             */
            var largestIndex = -1;
            for (var i = numList.Length - 2; i >= 0; i--)
            {
                if (numList[i].CompareTo(numList[i + 1]) < 0)
                {
                    largestIndex = i;
                    break;
                }
            }

            if (largestIndex < 0) return false;

            var largestIndex2 = -1;
            for (var i = numList.Length - 1; i >= 0; i--)
            {
                if (numList[largestIndex].CompareTo(numList[i]) < 0)
                {
                    largestIndex2 = i;
                    break;
                }
            }

            var tmp = numList[largestIndex];
            numList[largestIndex] = numList[largestIndex2];
            numList[largestIndex2] = tmp;

            for (int i = largestIndex + 1, j = numList.Length - 1; i < j; i++, j--)
            {
                tmp = numList[i];
                numList[i] = numList[j];
                numList[j] = tmp;
            }

            return true;
        }

        // code from http://stackoverflow.com/questions/20530128/how-to-find-all-partitions-of-a-set
        public static IEnumerable<T[][]> GetAllPartitions<T>(T[] elements)
        {
            return GetAllPartitions(new T[][] { }, elements);
        }

        private static IEnumerable<T[][]> GetAllPartitions<T>(T[][] fixedParts, T[] suffixElements)
        {
            // A trivial partition consists of the fixed parts
            // followed by all suffix elements as one block
            yield return fixedParts.Concat(new[] { suffixElements }).ToArray();

            // Get all two-group-partitions of the suffix elements
            // and sub-divide them recursively
            var suffixPartitions = GetTuplePartitions(suffixElements);
            foreach (Tuple<T[], T[]> suffixPartition in suffixPartitions)
            {
                var subPartitions = GetAllPartitions(
                    fixedParts.Concat(new[] { suffixPartition.Item1 }).ToArray(),
                    suffixPartition.Item2);
                foreach (var subPartition in subPartitions)
                {
                    yield return subPartition;
                }
            }
        }

        private static IEnumerable<Tuple<T[], T[]>> GetTuplePartitions<T>(
            T[] elements)
        {
            // No result if less than 2 elements
            if (elements.Length < 2) yield break;

            // Generate all 2-part partitions
            for (int pattern = 1; pattern < 1 << (elements.Length - 1); pattern++)
            {
                // Create the two result sets and
                // assign the first element to the first set
                List<T>[] resultSets = {
                    new List<T> { elements[0] }, new List<T>() };
                // Distribute the remaining elements
                for (int index = 1; index < elements.Length; index++)
                {
                    resultSets[(pattern >> (index - 1)) & 1].Add(elements[index]);
                } 

                yield return Tuple.Create(
                    resultSets[0].ToArray(), resultSets[1].ToArray());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Bit(this int k, int d)
        {
            return (k >> d) & 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BitFlip(this int k, int d)
        {
            return k ^ (1 << d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BitInvert(this int k, int digits)
        {
            return k ^ ((1 << digits) - 1);
        }
    }
}
