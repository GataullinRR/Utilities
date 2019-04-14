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

        public static Task[] RunInParallel(params Action[] actions)
        {
            var tasks = new List<Task>();
            foreach (var action in actions)
            {
                tasks.Add(Task.Run(action));
            }

            return tasks.ToArray();
        }
    }
}
