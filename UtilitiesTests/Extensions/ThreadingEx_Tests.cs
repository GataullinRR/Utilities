using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Extensions.Tests
{
    [TestFixture()]
    public class ThreadingEx_Tests
    {
        [Test()]
        public async Task CatchOperationCanceledExeption_Test()
        {
            var cts = new CancellationTokenSource();
            var t = Task.Delay(99999, cts.Token);
            cts.Cancel();

            await t.CatchOperationCanceledExeption();
            var ok = await CommonUtils.TryAsync(async () => await t);
            Assert.False(ok);
        }
    }
}