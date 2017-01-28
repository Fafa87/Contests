using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmsTest
{
    [TestClass]
    public class GeometryTest
    {
        [TestMethod]
        public void TriangleArea()
        {
            var points = new[] { new Point(3, 2), new Point(4, 3), new Point(4, 0) };
            Assert.AreEqual(3 * 1 / 2.0, GeometryUtils.Area(points[0], points[1], points[2]));
        }

        [TestMethod]
        public void PolygonConvexArea()
        {
            Polygon square = new Polygon(new[] { new Point(0, 0), new Point(0, 100), new Point(100, 100), new Point(100, 0) });
            Assert.AreEqual(100 * 100, square.Area());

            Polygon kickedRect = new Polygon(new[] { new Point(0, 0), new Point(0, 200), new Point(150, 300), new Point(150, 100) });
            Assert.AreEqual(150 * 200, kickedRect.Area());

            Polygon triangle = new Polygon(new[] { new Point(0, 0), new Point(0, 100), new Point(100, 100)});
            Assert.AreEqual(100 * 100 / 2, triangle.Area());

            Polygon rectMorePoints = new Polygon(new[] { new Point(0, 0), new Point(100, 0), new Point(200, 0), new Point(200, 100), new Point(100, 100), new Point(0, 100) });
            Assert.AreEqual(200 * 100, rectMorePoints.Area());
        }

        [TestMethod]
        public void PolygonComplexArea()
        {
            
            Polygon hat = new Polygon(new[] {  new Point(1, 1), new Point(0, 2), new Point(-1, 1) });
            Assert.AreEqual(1 * 1 * 1, hat.Area());

            Polygon roof = new Polygon(new[] { new Point(0, 0), new Point(0, 100), new Point(100, 100), new Point(0, 200), new Point(-100, 100) });
            Assert.AreEqual(1.5 * 100 * 100, roof.Area());

        }

        private void Quarter_CheckPoint(int x, int y, int expected)
        {
            var point = new Point(x, y);
            Assert.AreEqual(expected,point.Quarter);
        }

        [TestMethod]
        public void Quarter()
        {
            Quarter_CheckPoint(1, 0, 1);
            Quarter_CheckPoint(1, 1, 1);
            Quarter_CheckPoint(0, 1, 2);
            Quarter_CheckPoint(0, 2, 2);
            Quarter_CheckPoint(-1, 2, 2);
            Quarter_CheckPoint(-3, 0, 3);
            Quarter_CheckPoint(-1, -1, 3);
            Quarter_CheckPoint(0, -1, 4);
            Quarter_CheckPoint(3, -1, 4);
        }


        private void GetVisible_Test(Func<IEnumerable<LineSegment>, Point, IEnumerable<LineSegment>> calculator)
        {
            var segmentA = new LineSegment(new Point(1, 0), new Point(0, 5));
            var segmentB = new LineSegment(new Point(-1, 10), new Point(100, 10));
            var segments = new[] { segmentA, segmentB };
            var point = new Point(0, 0);

            CollectionAssert.AreEqual(new[] { segmentA }, calculator(segments, point).ToList());

            // fml
            var allSegments = new List<LineSegment>
            {
                new LineSegment(1, 0, 0, 1),
                new LineSegment(0, 1, 0, 2),
                new LineSegment(0, 2, 0, 3),
                new LineSegment(0, 3, -1, 3),
                new LineSegment(-1, 3, -2, 2)
            };
            var visibleSegments = new[] { allSegments[0], allSegments[4] };
            point = new Point(0, 0);
            var results = calculator(allSegments, point).ToList();
            CollectionAssert.AreEqual(visibleSegments, results.ToList());
        }

        [TestMethod]
        public void TestToString()
        {
            Point a = new Point(1.2, 2.3);
            Point b = new Point(2.2, 2.6);
            Point c = new Point(0, 0);
            GridPoint ia = new GridPoint(1, 2);
            Rectangle rect = new Rectangle(a, b);
            LineSegment segment = new LineSegment(a, b);
            Polygon poly = new Polygon(new[] { a, b, c });

            Assert.AreEqual("x=1.2 y=2.3", a.ToString());
            Assert.AreEqual("x=1 y=2", ia.ToString());

            Assert.AreEqual("1.2 2.6 2.2 2.3", rect.ToString());
            Assert.AreEqual("Segment: (" + a.ToString() + "->" + b.ToString() + ")", segment.ToString());
            Assert.AreEqual("Polygon: (" + a.ToString() + ";" + b.ToString() + ";" + c.ToString() + ")", poly.ToString());
        }

        [TestMethod]
        public void GetVisibleBrute()
        {
            GetVisible_Test((seg, p) => p.GetVisibleBrute(seg));
        }

        [TestMethod]
        public void TestPoly()
        {
            //var poly = new Polygon(new[] { new Point(1, 1), new Point(2, 1), new Point(2, 2), new Point(1, 2) });
            //Assert.AreEqual(1, poly.Area());
            //Assert.AreEqual(-1,poly.Area(true));

            var poly2 = new Polygon((new[] { new Point(1, 2), new Point(2, 2), new Point(2, 1), new Point(1, 1) }));
            Assert.AreEqual(1, poly2.Area());
            Assert.AreEqual(1, poly2.Area(true));

            var poly3 = new Polygon((new[] { new Point(1, 2), new Point(1.5, 3.0), new Point(2, 2), new Point(2, 1), new Point(1, 1) }).Reverse());
            Assert.AreEqual(1.5, poly3.Area());
            Assert.AreEqual(1.5, poly3.Area(true));

            poly2.points.Insert(1, new Point(1.5, 3.0));
            Assert.AreEqual(1.5, poly2.Area());
            Assert.AreEqual(1.5, poly2.Area(true));
        }
    }
}
