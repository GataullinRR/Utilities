using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public interface IEnableable
    {
        bool Enabled { get; set; }
    }

    public interface IDivisible<T, T1>
    {
        T DivideBy(T1 v);
    }

    public interface IMultipliable<T, T1>
    {
        T MultiplyBy(T1 v);
    }
}
