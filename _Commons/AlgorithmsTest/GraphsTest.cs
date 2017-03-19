using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Algorithms;

namespace AlgorithmsTest
{
    [TestClass]
    public class GraphsTest
    {
        [TestMethod]
        public void UnionFind()
        {
            var elem1 = Tuple.Create(1);
            var elem2 = Tuple.Create(2);
            var elem3 = Tuple.Create(3);
            var elem4 = Tuple.Create(4);

            var unionFind = new UnionFind<Tuple<int>>();
            Assert.AreEqual(1, unionFind.Find(elem1).Item1);
            unionFind.Union(elem1, elem2);
            unionFind.Union(elem2, elem4);
            Assert.AreEqual(3, unionFind.Find(elem3).Item1);
            Assert.AreEqual(4, unionFind.Find(elem4).Item1);
            Assert.AreEqual(4, unionFind.Find(elem2).Item1);
            Assert.AreEqual(4, unionFind.Find(elem1).Item1);

            Assert.AreEqual(2, unionFind.Components().Count);
        }
    }
}
