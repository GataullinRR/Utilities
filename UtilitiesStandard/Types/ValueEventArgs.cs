using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;

namespace Utilities.Types
{
    public class ValueEventArgs<T> : EventArgs
    {
        public static implicit operator ValueEventArgs<T>(T value)
        {
            return new ValueEventArgs<T>(value);
        }
        public static implicit operator T(ValueEventArgs<T> value)
        {
            return value.Value;
        }

        public T Value { get; }

        public ValueEventArgs(T value)
        {
            Value = value;
        }
    }
}
