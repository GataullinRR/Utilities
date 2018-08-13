using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions.Tests
{
    [TestFixture()]
    public class ArrayEx_Tests
    {
        List<byte> _source = new List<byte>()
        { 0xAA, 0x00, 0xAA, 0x00, 0x0A, 0xFF, 0xFF, 0x00, 0x01, 0x01, 0x01, 0xFF, 0x90, 0x01, 0xFF, 0xFF, 0x00 };

        [Test()]
        public void Find_OneVal_Test()
        {
            List<byte> what1 = new List<byte>() { 0xAA };
            List<byte> what2 = new List<byte>() { 0xFF };
            List<byte> what3 = new List<byte>() { 0x90 };
            List<byte> what4 = new List<byte>() { 0x10 };

            testFind(what1, 0, 2);
            testFind(what2, 5, 6, 11, 14, 15);
            testFind(what3, 12);
            testFind(what4);
        }

        [Test()]
        public void Find_TwoVal_Test()
        {
            List<byte> what1 = new List<byte>() { 0xAA, 0x00 };
            List<byte> what2 = new List<byte>() { 0xFF, 0x00 };
            List<byte> what3 = new List<byte>() { 0xFF, 0x90 };
            List<byte> what4 = new List<byte>() { 0xFF, 0x01 };

            testFind(what1, 0, 2);
            testFind(what2, 6, 15);
            testFind(what3, 11);
            testFind(what4);
        }

        [Test()]
        public void Find_LotOfVals_Test()
        {
            List<byte> what1 = new List<byte>() { 0x01, 0x01, 0xFF };
            List<byte> what2 = _source;
            List<byte> what3 = new List<byte>(_source);
            what3.Add(0x00);

            testFind(what1, 9);
            testFind(what2, 0);
            testFind(what3);
        }

        void testFind(List<byte> what, params int[] expected)
        {
            var positions = _source.FindAll(what);
            CollectionAssert.AreEqual(expected, positions);
        }

        [Test()]
        public void MoveSelfRight_Test()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.MoveSelf(2);
            var expected = new[] { 4, 5, 1, 2, 3 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
        [Test()]
        public void MoveSelfLeft_Test()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.MoveSelf(-2);
            var expected = new[] { 3, 4, 5, 1, 2 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
        [Test()]
        public void MoveSelf_BoundaryTest()
        {
            var actual = new[] { 1, 2, 3, 4, 5 }.MoveSelf(0);
            var expected = new[] { 1, 2, 3, 4, 5 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);

            actual = new[] { 1, 2, 3, 4, 5 }.MoveSelf(5);
            expected = new[] { 1, 2, 3, 4, 5 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }

        [Test()]
        public void SkipFromEndWhile_Test()
        {
            var actual = new[] { 4, 9, 3, 4, 5 }.SkipFromEndWhile(n => n > 3);
            var expected = new[] { 4, 9, 3 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
        [Test()]
        public void SkipFromEndWhile_Boundary1Test()
        {
            var actual = new int[] { }.SkipFromEndWhile(n => n > 3);
            var expected = new int[] { };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
        [Test()]
        public void SkipFromEndWhile_Boundary2Test()
        {
            var actual = new int[] { 5 }.SkipFromEndWhile(n => n > 3);
            var expected = new int[] { 5 };

            TestingUtils.AreEqual(Assert.IsTrue, expected, actual);
        }
    }
}