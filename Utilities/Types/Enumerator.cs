﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;
using System.Collections;

namespace Utilities.Types
{
    public class Enumerator<T> : IEnumerator<T>
    {
        readonly IEnumerator<T> _enumerator;

        public int Index { get; private set; } = -1;
        public T Current => _enumerator.Current;
        object IEnumerator.Current => _enumerator.Current;

        public Enumerator(IEnumerable<T> sequence)
        {
            _enumerator = sequence.GetEnumerator();
        }

        /// <summary>
        /// Advances enumerator position while enumerating returning sequence. It's strongly recommended to use it with 
        /// .Take(count).ToArray(). Enumerating twice will return different values and advance enumerator position!
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> AdvanceRangeOrThrow()
        {
            while (true)
            {
                yield return AdvanceOrThrow();
            }
        }
        public T AdvanceOrThrow()
        {
            var haveValue = MoveNext();
            if (!haveValue)
            {
                throw new InvalidOperationException("There are no elements left");
            }
            else
            {
                return Current;
            }
        }

        public void Dispose()
        {
            _enumerator.Dispose();
            Index = -1;
        }

        public bool MoveNext()
        {
            var haveItem = _enumerator.MoveNext();
            if (haveItem)
            {
                Index++;
            }
            else
            {
                Index = -1;
            }

            return haveItem;
        }

        public void Reset()
        {
            _enumerator.Reset();
            Index = -1;
        }
    }
}