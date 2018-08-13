using Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Utilities.Extensions.Tests
{
    [TestFixture()]
    public class ArrayEx_SkipFromEnd_Tests
    {
        [Test()]
        public void SkipFromEnd_Test()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.SkipFromEnd(2).ToArray();
            var expected = new[] { 1, 2, 3 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
        [Test()]
        public void SkipFromEndBoundary_Test()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.SkipFromEnd(5).ToArray();
            var expected = new int[] { };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);

            actual = new[] { 1, 2, 3, 4, 5 }.SkipFromEnd(6).ToArray();
            expected = new int[] { };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);

            actual = new[] { 1, 2, 3, 4, 5 }.SkipFromEnd(0).ToArray();
            expected = new[] { 1, 2, 3, 4, 5 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
    }
}