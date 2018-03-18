using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Algorithms;

namespace AlgorithmsTest
{
    [TestClass]
    public class CommonsTest
    {
        [TestMethod]
        public void WithIndexTest()
        {
            List<double> data = new List<double>();
            Assert.AreEqual(0, data.WithIndex().Count());

            data.Add(2.3);
            data.Add(2.9);
            var indexedData = data.WithIndex();
            CollectionAssert.AreEqual(new[] { 0, 1 }, indexedData.Select(p => p.Item1).ToList());
            CollectionAssert.AreEqual(new[] { 2.3, 2.9 }, indexedData.Select(p => p.Item2).ToList());
        }

        [TestMethod]
        public void FindFirstIndexGreaterThanOrEqualToTest()
        {
            Assert.AreEqual(0, new[] { 1 }.FindFirstIndexGreaterThanOrEqualTo(0, p => p));
            Assert.AreEqual(0, new[] { 1 }.FindFirstIndexGreaterThanOrEqualTo(1, p => p));
            Assert.AreEqual(1, new[] { 1 }.FindFirstIndexGreaterThanOrEqualTo(2, p => p));

            Assert.AreEqual(0, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(0, p => p));
            Assert.AreEqual(0, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(1, p => p));
            Assert.AreEqual(1, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(2, p => p));
            Assert.AreEqual(1, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(3, p => p));
            Assert.AreEqual(3, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(4, p => p));
            Assert.AreEqual(3, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(5, p => p));
            Assert.AreEqual(4, new[] { 1, 3, 3, 5 }.FindFirstIndexGreaterThanOrEqualTo(6, p => p));
        }

        [TestMethod]
        public void LowerBoundTest()
        {
            Assert.AreEqual(0, new List<int> { 1 }.GetLowerBound(0));
            Assert.AreEqual(0, new List<int> { 1 }.GetLowerBound(1));
            Assert.AreEqual(1, new List<int> { 1 }.GetLowerBound(2));

            Assert.AreEqual(0, new List<int> { 1, 3, 3, 5 }.GetLowerBound(0));
            Assert.AreEqual(0, new List<int> { 1, 3, 3, 5 }.GetLowerBound(1));
            Assert.AreEqual(1, new List<int> { 1, 3, 3, 5 }.GetLowerBound(2));
            Assert.AreEqual(1, new List<int> { 1, 3, 3, 5 }.GetLowerBound(3));
            Assert.AreEqual(3, new List<int> { 1, 3, 3, 5 }.GetLowerBound(4));
            Assert.AreEqual(3, new List<int> { 1, 3, 3, 5 }.GetLowerBound(5));
            Assert.AreEqual(4, new List<int> { 1, 3, 3, 5 }.GetLowerBound(6));
        }

        [TestMethod]
        public void LISTest()
        {
            CollectionAssert.AreEqual(new int[] { }, new int[] { }.FindLIS());
            CollectionAssert.AreEqual(new int[] { 1 }, new int[] { 1 }.FindLIS());

            CollectionAssert.AreEqual(new int[] { 1, 3 }, new int[] { 1, 3 }.FindLIS());
            CollectionAssert.AreEqual(new int[] { 1, 2, 5 }, new int[] { 1, 3, 2, 5 }.FindLIS());
            CollectionAssert.AreEqual(new int[] { 1, 3, 5 }, new int[] { 1, 3, 3, 5 }.FindLIS());
        }
    }
}
