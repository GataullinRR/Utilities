using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Extensions
{
    public class FindResult<T>
    {
        readonly T _Value;

        public bool Found { get; }
        public int Index { get; }
        public T Value
        {
            get
            {
                return Found 
                    ? _Value 
                    : throw new InvalidOperationException("The value has not been found.");
            }
        }
        public T ValueOrDefault
        {
            get
            {
                return Found
                    ? _Value
                    : default;
            }
        }

        public FindResult()
        {
            Index = -1;
            _Value = default;
        }
        public FindResult(int index, T value)
        {
            checkIndex(index);
            Index = index;
            _Value = value;
            Found = true;
        }
        /// <summary>
        /// If finding algorithm does not provide value, it can be found automatically, when user
        /// accessing to the <see cref="Value"/> property by enumerating <paramref name="valueSource"/> 
        /// till known <see cref="Index"/> value.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="valueSource"></param>
        public FindResult(int index, IEnumerable<T> valueSource)
        {
            checkIndex(index);
            Index = index;
            _Value = valueSource.First((v, i) => i == Index).Value;
            Found = true;
        }

        void checkIndex(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Index must be in range from 0 to Inf");
            }
        }
    }
}