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
    public class ArrayUtilities_Find_Tests
    {
        List<byte> _source = new List<byte>() { 0xAA, 0x00, 0x0A, 0xFF, 0xFF, 0x00, 0x01, 0x01, 0x01, 0xFF, 0x90 };

        [TestMethod()]
        public void Find_TwoVal_Test()
        {
            List<byte> what1 = new List<byte>() { 0xAA, 0x00 };
            List<byte> what2 = new List<byte>() { 0xFF, 0x00 };
            List<byte> what3 = new List<byte>() { 0xFF, 0x90 };
            List<byte> what4 = new List<byte>() { 0xFF, 0x01 };

            testFind(what1, 0);
            testFind(what2, 4);
            testFind(what3, 9);
            testFind(what4, -1);
        }

        [TestMethod()]
        public void Find_OneVal_Test()
        {
            List<byte> what1 = new List<byte>() { 0xAA };            
            List<byte> what2 = new List<byte>() { 0xFF };
            List<byte> what3 = new List<byte>() { 0x90 };
            List<byte> what4 = new List<byte>() { 0x10 };

            testFind(what1, 0);
            testFind(what2, 3);
            testFind(what3, 10);
            testFind(what4, -1);
        }

        [TestMethod()]
        public void Find_LotOfVals_Test()
        {
            List<byte> what1 = new List<byte>() { 0x01, 0x01, 0xFF };
            List<byte> what2 = _source;
            List<byte> what3 = new List<byte>(_source);
            what3.Add(0x00);

            testFind(what1, 7);
            testFind(what2, 0);
            testFind(what3, -1);
        }

        void testFind(List<byte> what, int expected)
        {
            int position = _source.Find(what).Index;
            Assert.AreEqual(expected, position);
        }
    }
}