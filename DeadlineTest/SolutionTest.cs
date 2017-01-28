using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Deadline;

namespace DeadlineTest
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void Construction()
        {
            var sol = new Solution(new IOClient(), 1023);
        }

        [TestMethod]
        public void InvariantCultureTest()
        {
            var sol = new Solution(new IOClient(), 1023);
            Assert.AreEqual("1.345", 1.345.ToString());
            Assert.AreEqual("314", 314.0.ToString());
            Assert.AreEqual("31314.3", 31314.3.ToString());

            Assert.AreEqual(1.345, "1.345".ParseList<double>().Single());
            Assert.AreEqual(314.0, "314".ParseList<double>().Single());
            Assert.AreEqual(31314.3, "31314.3".ParseList<double>().Single());
        }

        //[TestMethod]
        //public void DoubleTest()
        //{
        //    string map1 = "11.111."; // 11.1..1 -> ....1.1
        //    string map2 = ".1..1.."; // .1..1.. -> .1..1..
        //    string map3 = "11.111."; // 11.1..1 -> 11.1..1
        //    string[] map = new[] { map1, map2, map3 };
        //    int[] val = new[] { 1, 1 };
        //    var sol = new PegJumping();

        //    var res = sol.getMoves(val, map);
        //}

    }
}
