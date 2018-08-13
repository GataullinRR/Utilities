using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Types
{
    public class Enumerable<T> : IEnumerable<T>
    {
        IEnumerable<T> _values;

        public Enumerable()
        {
            _values = Enumerable.Empty<T>();
        }

        public void Add(T value)
        {
            Add(new T[] { value });
        }
        public void Add(IEnumerable<T> value)
        {
            _values = _values.Concat(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }
    }
}
