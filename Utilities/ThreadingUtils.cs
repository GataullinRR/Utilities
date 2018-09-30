using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities
{
   public static  class ThreadingUtils
    {
        public static CancellationToken CreateCToken(int timeout)
        {
            return new CancellationTokenSource(timeout).Token;
        }

        /// <summary>
        /// Returns task, which does not continue in initial <see cref="SynchronizationContext"/>.
        /// </summary>
        /// <returns></returns>
        public static ConfiguredTaskAwaitable ContinueAtThreadPull()
        {
            return Task.Run(delegate { }).ConfigureAwait(false);
        }
    }
}
