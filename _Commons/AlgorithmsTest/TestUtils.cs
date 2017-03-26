using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmsTest
{
    public static class TestUtils
    {
        public static void AreEquivalent<T>(ICollection<T> expected,
            ICollection<T> actual, Comparer<T> customComparer)
        {
            CollectionAssert.AreEqual(
                expected.OrderBy(x => x, customComparer).ToList(),
                actual.OrderBy(x => x, customComparer).ToList(),
                customComparer);
        }
    }
}
