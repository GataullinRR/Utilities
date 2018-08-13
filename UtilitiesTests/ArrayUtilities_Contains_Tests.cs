using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Tests
{
    [TestClass()]
    public class ArrayUtilities_Contains_Tests
    {
        List<byte> _source = new List<byte>() { 0xAA, 0x00, 0x0A, 0xFF, 0xFF, 0x00, 0x01, 0x01, 0x01, 0xFF, 0x90 };

        [TestMethod()]
        public void ContainsTest1()
        {
            List<byte> what1 = new List<byte>() { 0xFF, 0xFF, 0x00 };
            List<byte> what2 = _source;
            List<byte> what3 = new List<byte>() { 0x22 };

            testContains(what1, true);
            testContains(what2, true);
            testContains(what3, false);
        }

        void testContains(List<byte> what, bool expected)
        {
            bool b = _source.Contains(what);
            Assert.AreEqual(expected, b);
        }
    }
}