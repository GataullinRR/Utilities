using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;

namespace Utilities.Types.Tests
{
    [TestFixture()]
    public class Enumerator_Tests
    {
        [Test()]
        public void AdvanceOrThrow_Test1()
        {
            var values = new string[] { "1", "2", "3" };
            var enumerator = values.StartEnumeration();

            var actual = new List<string>();
            for (int i = 0; i < values.Length; i++)
            {
                actual.Add(enumerator.AdvanceOrThrow());
            }

            Assert.AreEqual(values, actual);
        }

        [Test()]
        public void AdvanceOrThrow_Test2()
        {
            var values = new string[] { "1", "2", "3" };
            var enumerator = values.StartEnumeration();

            var actual = new List<string>();
            foreach (var i in values.Length.Range())
            {
                actual.Add(enumerator.AdvanceOrThrow());
            }

            Assert.AreEqual(values, actual);
        }
    }
}