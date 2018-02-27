using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

using Algorithms;

namespace AlgorithmsTest
{
    [TestClass]
    public class CombinatoricsTest
    {
        [TestMethod]
        public void NextPermutation()
        {
            int[] list = { 1, 2, 3 };

            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 1, 3, 2 }));
            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 2, 1, 3 }));
            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 2, 3, 1 }));
            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 3, 1, 2 }));
            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 3, 2, 1 }));
            Assert.IsFalse(Combinatorics.NextPermutation(list));
        }

        [TestMethod]
        public void NextPermutationRepetitionsOnTheList()
        {
            int[] list = { 1, 1, 2 };

            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 1, 2, 1 }));
            Assert.IsTrue(Combinatorics.NextPermutation(list));
            Assert.IsTrue(list.SequenceEqual(new int[] { 2, 1, 1 }));
            Assert.IsFalse(Combinatorics.NextPermutation(list));
        }

        [TestMethod]
        public void GetAllPartitions()
        {
            var partitions = Combinatorics.GetAllPartitions(new[] { 1, 2, 3, 4 }).ToArray();

            int[][][] sets = {
                new int[][] { new int[] { 1, 2, 3, 4 } },
                new int[][] { new int[] { 1, 3, 4}, new int[] { 2} },
                new int[][] { new int[] { 1, 2, 4 }, new int[] { 3 } },
                new int[][] { new int[] { 1, 4 }, new int[] { 2, 3 } },
                new int[][] { new int[] { 1, 4 }, new int[] { 2 }, new int[] { 3 } },
                new int[][] { new int[] { 1, 2, 3 }, new int[] { 4 } },
                new int[][] { new int[] { 1, 3 }, new int[] { 2, 4 } },
                new int[][] { new int[] { 1, 3 }, new int[] { 2 }, new int[] { 4 } },
                new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } },
                new int[][] { new int[] { 1, 2 }, new int[] { 3 }, new int[] { 4 } },
                new int[][] { new int[] { 1 }, new int[] { 2, 3, 4 } },
                new int[][] { new int[] { 1 }, new int[] { 2, 4 }, new int[] { 3 } },
                new int[][] { new int[] { 1 }, new int[] { 2, 3 }, new int[] { 4 } },
                new int[][] { new int[] { 1 }, new int[] { 2 }, new int[] { 3, 4 } },
                new int[][] { new int[] { 1 }, new int[] { 2 }, new int[] { 3 }, new int[] { 4 } }
            };

            Assert.AreEqual(sets.Length, partitions.Length);
            for (int i = 0; i < sets.Length; i++)
            {
                Assert.AreEqual(sets[i].Length, partitions[i].Length);
                for (int j = 0; j < sets[i].Length; j++)
                {
                    Assert.IsTrue(sets[i][j].SequenceEqual(partitions[i][j]));
                }
            }
        }

        [TestMethod]
        public void BitTest()
        {
            int k1 = Convert.ToInt32("01101101", 2);
            Assert.AreEqual(1, k1.Bit(0));
            Assert.AreEqual(0, k1.Bit(1));
            Assert.AreEqual(1, k1.Bit(2));
            Assert.AreEqual(1, k1.Bit(3));
            Assert.AreEqual(0, k1.Bit(4));

            int k2 = Convert.ToInt32("10010010", 2);
            Assert.AreEqual(k1.BitInvert(8), k2);
            Assert.AreEqual(k1.BitInvert(8).BitInvert(8), k1);
            Assert.AreEqual(k2.BitInvert(8).BitInvert(8), k2);

            int k3 = k2.BitFlip(0);
            k3 = k3.BitFlip(4);
            int expect_k3 = Convert.ToInt32("10000011", 2);
            Assert.AreEqual(expect_k3, k3);
        }
    }
}
