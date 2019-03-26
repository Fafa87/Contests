
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Algorithms;
using System.Globalization;

namespace AlgorithmsTest
{
    [TestClass]
    public class MapsTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        }

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

            var point = new GridPoint(2, 1);
            Assert.AreEqual(false, map2[point]);
            map2[point] = true;
            Assert.AreEqual(true, map2[point]);
        }

        [TestMethod]
        public void CreateMapFromSeparatedString()
        {
            List<string> valueMap = new List<string>
            {
                "1 2 3 10 -1",
                "0.3 2 3 1.2 -3.1"
            };

            var map = Map<int>.ParseMap(valueMap, ' ', double.Parse);
            CollectionAssert.AreEqual(new double[] { 1, 2, 3, 10, -1 }, map[0]);
            CollectionAssert.AreEqual(new double[] { 0.3, 2, 3, 1.2, -3.1 }, map[1]);
        }

        [TestMethod]
        public void IterateMap()
        {
            List<string> valueMap = new List<string>
            {
                "1 2 3 10 -1",
                "0.3 2 3 1.2 -3.1"
            };

            var map = Map<int>.ParseMap(valueMap, ' ', double.Parse);
            var coords = map.Coordinates().ToList();
            int cur = 0;
            for (int y = 0; y < map.Rows; y++)
                for (int x = 0; x < map.Cols; x++)
                {
                    var coord = coords[cur];
                    Assert.AreEqual(x, coord.X);
                    Assert.AreEqual(y, coord.Y);
                    cur++;
                }

            Assert.AreEqual(map.Rows * map.Cols, map.Coordinates().Count());
        }

        [TestMethod]
        public void IterateFilterMap()
        {
            List<string> valueMap = new List<string>
            {
                "1 2 3 10 -1",
                "0.3 2 3 1.2 -3.1"
            };

            var map = Map<int>.ParseMap(valueMap, ' ', double.Parse);
            var coords = map.Coordinates((v) => v < 1 || v == 10).ToList();

            Assert.AreEqual(4, coords.Count());
            Assert.AreEqual(new GridPoint(3, 0), coords[0]);
            Assert.AreEqual(new GridPoint(4, 0), coords[1]);
            Assert.AreEqual(new GridPoint(0, 1), coords[2]);
            Assert.AreEqual(new GridPoint(4, 1), coords[3]);
        }

        [TestMethod]
        public void CenteredBoxTest()
        {
            var point = new Point(1.2, 3.6).ToGridPoint(true);

            Assert.AreEqual(new GridPoint(1, 3), Moves.CenteredBox(point, 0, 0).Single());

            var box = Moves.CenteredBox(point, 1, 2);
            Assert.AreEqual(15, box.Count());
            Assert.AreEqual(new GridPoint(-1, 2), box[0]);
            Assert.AreEqual(new GridPoint(0, 2), box[1]);
            Assert.AreEqual(new GridPoint(1, 2), box[2]);
            Assert.AreEqual(new GridPoint(2, 2), box[3]);
            Assert.AreEqual(new GridPoint(3, 2), box[4]);
            Assert.AreEqual(new GridPoint(-1, 3), box[5]);
            Assert.AreEqual(new GridPoint(1, 3), box[7]);
            Assert.AreEqual(new GridPoint(3, 4), box[14]);
        }

        [TestMethod]
        public void ConvertPointToGridPoint()
        {
            var point = new Point(1.2, 3.6);
            Assert.AreEqual(new GridPoint(1, 4), point.ToGridPoint(false));
            Assert.AreEqual(new GridPoint(1, 3), point.ToGridPoint(true));

            Assert.AreEqual(new Point(1, 4), point.ToGridPoint(false).ToPoint());
        }
    }
}
