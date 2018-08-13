using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Types
{
    public class Counter
    {
        readonly Dictionary<string, int> _counters = new Dictionary<string, int>();

        public void Increment(string counter)
        {
            addCounterIfNotExist(counter);
            _counters[counter] += 1;
        }
        public void Decrement(string counter)
        {
            addCounterIfNotExist(counter);
            _counters[counter] -= 1;
        }
        public void Reset(string counter)
        {
            addCounterIfNotExist(counter);
            _counters[counter] = 0;
        }

        private void addCounterIfNotExist(string counter)
        {
            if (counter == null || !_counters.ContainsKey(counter))
            {
                _counters.Add(counter, 0);
            }
        }

        public override string ToString()
        {
            return _counters.AsMultilineString();
        }
    }
}
