using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.QuickGraph.Map;

namespace AlgorithmsTest.QuickGraph
{
    [TestClass]
    public class Map2DGraphTest
    {
        class CountryWithRoads : Map2DGraph<char>
        {
            public CountryWithRoads(int rows, int cols)
                : base(rows, cols)
            {
            }

            public override double Cost(GridPoint start, GridPoint step, char fieldType)
            {
                return fieldType == '.' ? 1 : 2;
            }

            public override bool IsEmptyField(char field)
            {
                return field != '#';
            }

            public void CreateEdges()
            {
                base.CreateEdges(Algorithms.GraphUtils.STEPS4);
            }
        }

        [TestMethod]
        public void RoadToTheCastleTest()
        {
            CountryWithRoads map = new CountryWithRoads(5, 5);
            map.Fill(new [] {   
                ".....",
                "..::.",
                "#.:##",
                ".....",
                "....." 
            }.ToList(), (c) => c);

            map.CreateVertices();
            map.CreateEdges();

            var res = map.GetClosestPath(new GridPoint(4, 1), new GridPoint(4, 3));
            Assert.AreEqual(9, res.Item2);
            Assert.AreEqual(new GridPoint(3, 1), res.Item1);

            res = map.GetClosestPath(new GridPoint(3, 1), new GridPoint(4, 3));
            Assert.AreEqual(8, res.Item2);
            Assert.AreEqual(new GridPoint(2, 1), res.Item1);

            res = map.GetClosestPath(new GridPoint(2, 1), new GridPoint(4, 3));
            Assert.AreEqual(6, res.Item2);
            Assert.AreEqual(new GridPoint(2, 2), res.Item1);

            res = map.GetClosestPath(new GridPoint(2, 2), new GridPoint(4, 3));
            Assert.AreEqual(4, res.Item2);
            Assert.AreEqual(new GridPoint(2, 3), res.Item1);

            res = map.GetClosestPath(new GridPoint(2, 3), new GridPoint(4, 3));
            Assert.AreEqual(2, res.Item2);
            Assert.AreEqual(new GridPoint(3, 3), res.Item1);
        }
    }
}
