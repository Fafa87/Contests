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
        public void TestMax()
        {
            Assert.AreEqual(4, Commons.Max(4));
            Assert.AreEqual(4.2, Commons.Max(4.2));
            Assert.AreEqual(3, Commons.Max(1, 3));
            Assert.AreEqual(3.4, Commons.Max(1, 3.4));
        }

        [TestMethod]
        public void TestMin()
        {
            Assert.AreEqual(4, Commons.Min(4));
            Assert.AreEqual(4.2, Commons.Min(4.2));
            Assert.AreEqual(1, Commons.Min(1, 3));
            Assert.AreEqual(1, Commons.Min(1, 3.4));
        }

    }
}
