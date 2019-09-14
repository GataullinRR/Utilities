using System;
using System.Collections.Generic;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Types
{
    public class EList<T> : List<T>
    {
        public static EList<T> operator +(EList<T> list, T item)
        {
            list.Add(item);

            return list;
        }
        public static EList<T> operator +(EList<T> list, IEnumerable<T> items)
        {
            list.AddRange(items);

            return list;
        }

        public EList(IEnumerable<T> collection)
            : base(collection)
        {

        }
        public EList(int capacity)
            : base(capacity)
        {

        }
    }
}
