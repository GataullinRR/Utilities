using NUnit.Framework;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Utilities.Extensions;

namespace Utilities.Tests
{
    [TestFixture()]
    public class InteropUtils_Tests
    {
        class SomeClass
        {
            public byte Byte = 1;
            public byte[] ByteArr = new byte[] { 2, 3 };
            public byte[][] ByteJaggedArr = new byte[][] 
            {
                new byte[] { 4, 5 },
                new byte[] { 6, 7 },
            };
        }

        [Test()]
        public void SerializeAllPrimitiveFieldsSequentiallyTest()
        {
            var actual = InteropUtils.SerializeAllPrimitiveFieldsSequentially(new SomeClass()).ToArray();
            var expected = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

            Assert.AreEqual(expected, actual);
        }

        class TestClass
        {
            public ushort US = 256;
            public float F = 1;
            public byte B = 128;
            public int I = int.MaxValue;
            public char C = '0';
        }
        class TestClassWithArray
        {
            public object ComplicateObj = new object();
            public object ComplicateNullObj = null;
            public float F = 1;
            public byte B = 128;
            public char[] CArr = new[] { '0', '1' };
        }

        [Test()]
        public void SerializAllPrimitiveFieldsSequentiallyTest()
        {
            var tstObj = new TestClass();
            var actual = InteropUtils.SerializeAllPrimitiveFieldsSequentially(tstObj);

            var writer = new BinaryWriter(new MemoryStream());
            writer.Write(tstObj.US);
            writer.Write(tstObj.F);
            writer.Write(tstObj.B);
            writer.Write(tstObj.I);
            writer.Write((short)tstObj.C);
            var expected = writer.BaseStream.To<MemoryStream>().ToArray();

            var info = ArrayUtils.Compare(actual, expected, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }

        [Test()]
        public void SerializeAllTest1()
        {
            var tstObj = new TestClass();
            var actual = InteropUtils.SerializeAll(tstObj.US, tstObj.F, tstObj.B, tstObj.I, tstObj.C);

            var writer = new BinaryWriter(new MemoryStream());
            writer.Write(tstObj.US);
            writer.Write(tstObj.F);
            writer.Write(tstObj.B);
            writer.Write(tstObj.I);
            writer.Write((short)tstObj.C);
            var expected = writer.BaseStream.To<MemoryStream>().ToArray();

            var info = ArrayUtils.Compare(actual, expected, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }


        [Test()]
        public void SerializeAllTest2()
        {
            var tstObj = new TestClass();
            var actual = InteropUtils.SerializeAll(new[] { 0, 1, 2, 3 });

            var writer = new BinaryWriter(new MemoryStream());
            writer.Write(0);
            writer.Write(1);
            writer.Write(2);
            writer.Write(3);
            var expected = writer.BaseStream.To<MemoryStream>().ToArray();

            var info = ArrayUtils.Compare(actual, expected, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }

        [Test()]
        public void SerializeAllTest3()
        {
            var tstObj = new TestClass();
            var actual = InteropUtils.SerializeAll(new[] { 0, 1, 2, 3 }, new[] { 4D, 5D });

            var writer = new BinaryWriter(new MemoryStream());
            writer.Write(0);
            writer.Write(1);
            writer.Write(2);
            writer.Write(3);
            writer.Write(4D);
            writer.Write(5D);
            var expected = writer.BaseStream.To<MemoryStream>().ToArray();

            var info = ArrayUtils.Compare(actual, expected, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }

        [Test()]
        public void InitializeAllFieldsSequentiallyTest1()
        {
            var tstObj = new TestClass();
            var initial = InteropUtils.SerializeAllPrimitiveFieldsSequentially(tstObj);
            tstObj.B = 0;
            tstObj.C = 'ф';
            tstObj.F = 0;
            tstObj.I = 0;
            tstObj.US = 0;

            InteropUtils.InitializeAllFieldsSequentially
                (tstObj, new MemoryStream(initial.ToArray()), nameof(TestClass.US), nameof(TestClass.C));

            var current = InteropUtils.SerializeAllPrimitiveFieldsSequentially(tstObj);

            var info = ArrayUtils.Compare(current, initial, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }

        [Test()]
        public void InitializeAllFieldsSequentiallyTest2()
        {
            var tstObj = new TestClass();
            var initial = InteropUtils.SerializeAllPrimitiveFieldsSequentially(tstObj);
            tstObj.B = 0;
            tstObj.C = 'а';
            tstObj.F = 0;
            tstObj.I = 0;
            tstObj.US = 0;

            var stream = new MemoryStream(initial.ToArray());
            InteropUtils.InitializeAllFieldsSequentially
                (tstObj, stream, nameof(TestClass.US), nameof(TestClass.F));
            InteropUtils.InitializeAllFieldsSequentially
                (tstObj, stream, nameof(TestClass.B), nameof(TestClass.I));
            InteropUtils.InitializeAllFieldsSequentially
                (tstObj, stream, nameof(TestClass.C), nameof(TestClass.C));

            var current = InteropUtils.SerializeAllPrimitiveFieldsSequentially(tstObj);

            var info = ArrayUtils.Compare(current, initial, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }

        [Test()]
        public void SerializeAllPrimitiveFieldsSequentiallyTest1()
        {
            var tstObj = new TestClassWithArray();
            var actual = InteropUtils.SerializeAllPrimitiveFieldsSequentially(tstObj);

            var writer = new BinaryWriter(new MemoryStream());
            writer.Write(tstObj.F);
            writer.Write(tstObj.B);
            writer.Write((ushort)tstObj.CArr[0]);
            writer.Write((ushort)tstObj.CArr[1]);
            var expected = writer.BaseStream.To<MemoryStream>().ToArray();

            var info = ArrayUtils.Compare(actual, expected, (v1, v2) => v1 == v2);
            Assert.IsTrue(info.IsMatch, info.ToString());
        }
    }
}