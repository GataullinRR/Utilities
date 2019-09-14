using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace Utilities.Types
{
    public class PeriodDelay
    {
        readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public int Period { get; }
        public int TimeLeft => (Period - _stopwatch.Elapsed.TotalMilliseconds).NegativeToZero().Round();

        public PeriodDelay(int period)
        {
            Period = period;
        }

        public async Task WaitTimeLeft()
        {
            await WaitTimeLeft(CancellationToken.None);
        }
        public async Task WaitTimeLeft(CancellationToken cancellation)
        {
            await Task.Delay(TimeLeft, cancellation);
            _stopwatch.Restart();
        }
    }
}
