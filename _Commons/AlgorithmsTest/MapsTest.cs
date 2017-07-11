
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Algorithms;

namespace AlgorithmsTest
{
    [TestClass]
    public class MapsTest
    {
        [TestMethod]
        public void CreateMapFromString()
        {
            List<string> hashMap = new List<string> { "#..#",
                                                      "..##"};
            var map1 = Map<char>.ParseMap(hashMap, p => p);
            CollectionAssert.AreEqual("#..#".ToArray(), map1[0]);
            CollectionAssert.AreEqual("..##".ToArray(), map1[1]);

            List<string> boolMap = new List<string> { "011",
                                                      "110"};
            var map2 = Map<char>.ParseMap(boolMap, p => p == '1');
            Assert.AreEqual(false, map2[0][0]);
            Assert.AreEqual(true, map2[0][1]);
            Assert.AreEqual(true, map2[0][2]);
            Assert.AreEqual(true, map2[1][0]);
            Assert.AreEqual(true, map2[1][1]);
            Assert.AreEqual(false, map2[1][2]);

            var point = new GridPoint(2,1);
            Assert.AreEqual(false, map2[point]);
            map2[point] = true;
            Assert.AreEqual(true, map2[point]);
        }
    }
}
