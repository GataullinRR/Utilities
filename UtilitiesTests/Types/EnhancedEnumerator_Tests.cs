using NUnit.Framework;
using Utilities.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Types.Tests
{
    [TestFixture()]
    public class EnhancedEnumerator_Tests
    {
        [Test()]
        public void EnhancedEnumerator_Test()
        {
            var enumerator = new EnhancedEnumerator<int>(new[] { 1, 2, 3 });
            bool hasNext = false;
            hasNext = enumerator.MoveNext();
            Assert.AreEqual(true, hasNext);
            Assert.AreEqual(1, enumerator.Current);
            Assert.AreEqual(0, enumerator.Index);
            Assert.AreEqual(false, enumerator.IsLastElement);

            hasNext = enumerator.MoveNext();
            Assert.AreEqual(true, hasNext);
            Assert.AreEqual(2, enumerator.Current);
            Assert.AreEqual(1, enumerator.Index);
            Assert.AreEqual(false, enumerator.IsLastElement);

            hasNext = enumerator.MoveNext();
            Assert.AreEqual(true, hasNext);
            Assert.AreEqual(3, enumerator.Current);
            Assert.AreEqual(2, enumerator.Index);
            Assert.AreEqual(true, enumerator.IsLastElement);

            hasNext = enumerator.MoveNext();
            Assert.AreEqual(false, hasNext);
            Assert.Throws<InvalidOperationException>(() => { var tmp = enumerator.Current; });
            Assert.Throws<InvalidOperationException>(() => { var tmp = enumerator.Index; });
            Assert.Throws<InvalidOperationException>(() => { var tmp = enumerator.IsLastElement; });

            hasNext = enumerator.MoveNext();
            Assert.AreEqual(false, hasNext);
            Assert.Throws<InvalidOperationException>(() => { var tmp = enumerator.Current; });
            Assert.Throws<InvalidOperationException>(() => { var tmp = enumerator.Index; });
            Assert.Throws<InvalidOperationException>(() => { var tmp = enumerator.IsLastElement; });
        }
    }
}