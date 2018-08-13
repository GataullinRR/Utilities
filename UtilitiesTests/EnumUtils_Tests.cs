using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Tests
{
    [TestClass()]
    public class EnumUtilsTests
    {
        enum TestEnum : int
        {
            A,
            B,
            C
        }

        [TestMethod()]
        public void GetValues_Test()
        {
            List<TestEnum> values = EnumUtils.GetValues<TestEnum>().ToList();
            List<TestEnum> expected = new List<TestEnum> { TestEnum.A, TestEnum.B, TestEnum.C };

            CollectionAssert.AreEqual(expected, values);
        }
    }
}