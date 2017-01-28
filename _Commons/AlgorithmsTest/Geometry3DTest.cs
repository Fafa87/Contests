using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmsTest
{
    [TestClass]
    public class Geometry3DTest
    {
        [TestMethod]
        public void Point3D_Length()
        {
            // Primitive Pythagorean quadruples
            Assert.AreEqual(3.0, new Point3D(1.0, 2.0, 2.0).Length());
            Assert.AreEqual(13.0, new Point3D(3.0, 4.0, 12.0).Length());
            Assert.AreEqual(19.0, new Point3D(1.0, 6.0, 18.0).Length());
        }
    }
}
