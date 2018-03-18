using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Algorithms;

namespace AlgorithmsTest
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void TestMax()
        {
            Assert.AreEqual(4, Utils.Max(4));
            Assert.AreEqual(4.2, Utils.Max(4.2));
            Assert.AreEqual(3, Utils.Max(1, 3));
            Assert.AreEqual(3.4, Utils.Max(1, 3.4));
        }

        [TestMethod]
        public void TestMin()
        {
            Assert.AreEqual(4, Utils.Min(4));
            Assert.AreEqual(4.2, Utils.Min(4.2));
            Assert.AreEqual(1, Utils.Min(1, 3));
            Assert.AreEqual(1, Utils.Min(1, 3.4));
        }

        [TestMethod]
        public void MinMaxElement()
        {
            List<Tuple<int, double>> data = new List<Tuple<int, double>>();
            Assert.AreEqual(null, data.MinElement());
            Assert.AreEqual(null, data.MaxElement());

            data.Add(Tuple.Create(1, 2.3));
            Assert.AreEqual(data[0], data.MinElement());
            Assert.AreEqual(data[0], data.MaxElement());

            data.Add(Tuple.Create(6, 1.3));
            Assert.AreEqual(data[0], data.MinElement());
            Assert.AreEqual(data[1], data.MaxElement());

            Assert.AreEqual(data[1], data.MinElement(p=>p.Item2));
            Assert.AreEqual(data[0], data.MaxElement(p => p.Item2));
        }
    }
}
