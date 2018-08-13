using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Types
{
    public class IEqualityComparerAction<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> _comparer;

        public IEqualityComparerAction(Func<T, T, bool> comparer)
        {
            _comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
