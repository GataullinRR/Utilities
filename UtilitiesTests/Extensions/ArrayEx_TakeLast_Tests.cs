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
    public class ArrayEx_TakeLast_Tests
    {
        [Test()]
        public void TakeLast_Test()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.TakeLast(2);
            var expected = new[] { 4, 5 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
        [Test()]
        public void TakeLastBoundary_Test()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.TakeLast(5);
            var expected = new[] { 1, 2, 3, 4, 5 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);

            actual = new[] { 1, 2, 3, 4, 5 }.TakeLast(0);
            expected = new int[] {  };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
    }
}