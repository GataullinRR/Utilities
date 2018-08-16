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
    public class ArrayEx_Distinct_Tests
    {
        class TestClass
        {
            public string Value1;

            public override bool Equals(object obj)
            {
                if (obj is TestClass tc)
                {
                    return tc.Value1 == Value1;
                }
                else
                {
                    return false;
                }
            }
        }

        [Test()]
        public void Distinct_FuncComparer1()
        {
            var actual = new[] { 1, 2, 3, 4, 4, 5, 1 }
                .Select(n => new { Key = n })
                .Distinct((v1, v2) => v1.Key == v2.Key)
                .Select(v => v.Key)
                .ToArray();
            var expected = new[] { 1, 2, 3, 4, 5 };

            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void Distinct_FuncComparer2()
        {
            var actual = new[] 
            {
                new TestClass() { Value1 = "1" },
                new TestClass() { Value1 = "1" },
                new TestClass() { Value1 = "2" }
            }
                .Distinct((v1, v2) => v1.Equals(v2))
                .Select(v => v.Value1)
                .ToArray();
            var expected = new[] { "1", "2" };

            Assert.AreEqual(expected, actual);
        }
    }
}