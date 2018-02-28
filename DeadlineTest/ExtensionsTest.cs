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
