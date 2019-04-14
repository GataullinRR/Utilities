using System;
using System.Collections.Generic;
using System.Collections;

namespace Utilities.Types
{
    /// <summary>
    /// Not thread safe (even IEnumerator)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedEnumerable<T> : Disposable, IReadOnlyList<T>
    {
        readonly IEnumerator<T> _source;
        readonly List<T> _cache = new List<T>();

        public T this[int index]
        {
            get
            {
                throwIfDisposed();

                var dI = index - _cache.Count;
                while (dI >= 0 && moveNextAndUpdate())
                {
                    dI--;
                    _cache.Add(_source.Current);
                }
                if (dI >= 0)
                {
                    throw new InvalidOperationException($"Underlying Enumerator didn't provide element with index {index}");
                }

                return _cache[index];
            }
        }

        public int Count
        {
            get
            {
                throwIfDisposed();

                while (moveNextAndUpdate())
                {
                    _cache.Add(_source.Current);
                }

                return _cache.Count;
            }
        }

        public bool IsEnumerated { get; private set; }

        public CachedEnumerable(IEnumerable<T> source)
        {
            _source = source.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throwIfDisposed();

            foreach (var item in _cache)
            {
                yield return item;
            }
            while (moveNextAndUpdate())
            {
                _cache.Add(_source.Current);
                yield return _source.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throwIfDisposed();

            return GetEnumerator();
        }

        bool moveNextAndUpdate()
        {
            var result = _source.MoveNext();
            IsEnumerated = !result;
            if (IsEnumerated)
            {
                _cache.TrimExcess();
            }

            return result;
        }

        protected override void disposeManagedState()
        {
            _source.Dispose();
        }
    }
}
