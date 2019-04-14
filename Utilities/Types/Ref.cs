using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Types
{
    public class SetRef<T>
    {
        public static implicit operator SetRef<T>(Action<T> setAction)
        {
            return new SetRef<T>(setAction);
        }
        public static implicit operator SetRef<T>(Func<T, T> setFunc)
        {
            return new SetRef<T>(v => setFunc(v));
        }

        readonly Action<T> _setter;

        public T Value
        {
            set { _setter(value); }
        }

        public SetRef(Action<T> setter)
        {
            _setter = setter ?? throw new ArgumentNullException();
        }
    }

    public class Ref<T> 
    {
        readonly Func<T> _getter;
        readonly Action<T> _setter;

        public T Value
        {
            get { return _getter(); }
            set { _setter(value); }
        }

        public Ref(Func<T> getter)
        {
            _getter = getter;
        }

        public Ref(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }
    }
}
