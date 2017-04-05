using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Deadline;

namespace DeadlineTest
{
    [TestClass]
    public class ExtensionsTest
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
