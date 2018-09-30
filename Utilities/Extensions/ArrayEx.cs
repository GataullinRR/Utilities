using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class ArrayEx
    {
        #region ##### Type convertions #####

        public static List<TOut> ChangeType<TIn, TOut>(this IList<TIn> array)
             where TIn : IConvertible
        {
            List<TOut> result = new List<TOut>(array.Count);
            for (int i = 0; i < array.Count; i++)
            {
                TOut newVal = (TOut)array[i].ToType(typeof(TOut), null);
                result.Add(newVal);
            }

            return result;
        }

        #endregion

        #region ##### IEnumerable #####

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> sequence)
        {
            foreach (var items in sequence)
            {
                foreach (var item in items)
                {
                    yield return item;
                }
            }
        }

        public static T FirstItem<T>(this IEnumerable<T> sequence)
        {
            foreach (var item in sequence)
            {
                return item;
            }

            throw new ArgumentException();
        }

        public static T FirstOrDefault<T>(this IEnumerable<T> sequence, T defaultValue)
        {
            foreach (var item in sequence)
            {
                return item;
            }

            return defaultValue;
        }

        public static T FirstOrAnyOrDefault<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            T any = default;
            foreach (var item in sequence)
            {
                any = item;
                if (predicate(item))
                {
                    return item;
                }
            }

            return any;
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> sequence)
        {
            return !sequence.IsEmpty();
        }
        public static bool IsEmpty<T>(this IEnumerable<T> sequence)
        {
            foreach (var item in sequence)
            {
                return false;
            }
            return true;
        }

        #region ##### To opperators #####

        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> sequence)
        {
            return new ObservableCollection<T>(sequence);
        }

        #endregion

        #region ##### GetXXX #####

        public static List<T> GetCopy<T>(this IEnumerable<T> array)
            where T : struct
        {
            return new List<T>(array);
        }

        public static List<T> GetRange<T>(this IEnumerable<T> sequence, int from, int count)
        {
            return new List<T>(sequence.Skip(from).Take(count));
        }
        public static List<T> GetRangeTill<T>(this IEnumerable<T> sequence, int from, int to)
        {
            if (to < from)
            {
                throw new ArgumentOutOfRangeException();
            }

            return new List<T>(sequence.Skip(from).Take(to - from));
        }

        #endregion

        #region ##### AsXXX #####

        /// <summary>
        /// Just aggregates the values into the string with empty separator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string AsString<T>(this IEnumerable<T> values)
        {
            return values.AsString("");
        }
        public static string AsString<T>(this IEnumerable<T> values, string splitter)
        {
            StringBuilder sb = new StringBuilder();
            values.ForEach((v, i) => sb.AppendFormat("{0}{1}", v, splitter));

            return sb.ToString();
        }

        public static string AsMultilineString<T>(this IEnumerable<T> values)
        {
            return AsMultilineString(values, v => v.ToString(), 0);
        }
        public static string AsMultilineString<T>(this IEnumerable<T> values, int indentWidth)
        {
            return AsMultilineString(values, v => v.ToString(), indentWidth);
        }
        public static string AsMultilineString<T>(this IEnumerable<T> values, int indentWidth, bool writeIndex)
        {
            return AsMultilineString(values, v => v.ToString(), indentWidth, writeIndex);
        }
        public static string AsMultilineString<T>(
            this IEnumerable<T> values, Func<T, string> toStringConverter, int indentWidth)
        {
            return AsMultilineString(values, toStringConverter, indentWidth, true);
        }
        public static string AsMultilineString<T>(
            this IEnumerable<T> values, Func<T, string> toStringConverter, int indentWidth, bool writeIndex)
        {
            StringBuilder sb = new StringBuilder();
            string indent = new string('\t', indentWidth);
            if (writeIndex)
            {
                values.ForEach((v, i) => sb.AppendFormatLine("{0}[{1}]: {2}", indent, i, toStringConverter(v)));
            }
            else
            {
                values.ForEach((v, i) => sb.AppendFormatLine("{0}{1}", indent, toStringConverter(v)));
            }
            return sb.ToString();
        }

        public static string AsString(this IEnumerable<byte> arr, Encoding encoding)
        {
            return encoding.GetString(arr.ToArray());
        }

        #endregion

        //public static IEnumerable<T> TakeWhileInclusive<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        //{
        //    var isFirst = true;
        //    foreach (var e in sequence)
        //    {
        //        if (predicate(e))
        //        {
        //            yield return e;
        //        }
        //        else if (!isFirst)
        //        {
        //            yield return e;
        //            yield break;
        //        }
        //        else
        //        {
        //            yield break;
        //        }

        //        isFirst = false;
        //    }
        //}

        public static FindResult<T> First<T>(this IEnumerable<T> sequence, Func<T, int, bool> predicate)
        {
            var i = 0;
            foreach (var item in sequence)
            {
                if (predicate(item, i))
                {
                    return new FindResult<T>(i, item);
                }
                i++;
            }

            throw new InvalidOperationException("Sequence contains no matching element");
        }

        public static IEnumerable<T> EmptyToNull<T>(this IEnumerable<T> sequence)
        {
            return sequence.IsEmpty()
                ? null
                : sequence;
        }

        /// <summary>
        /// Не выбрасывает исключение если sequence.Count <= count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> SkipFromEnd<T>(this IEnumerable<T> sequence, int count)
        {
            var future = sequence.Take(count).ToArray();
            if (future.Length != count)
            {
                yield break;
            }
            else if (count == 0)
            {
                foreach (var item in sequence)
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var item in sequence.Skip(count))
                {
                    yield return future[0];
                    future.MoveLeftSelf(1);
                    future[count - 1] = item;
                }
            }
        }
        public static IEnumerable<T> SkipFromEndWhile<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
        {
            int maxPosition = 0;
            var tmpPosition = 0;
            foreach (var item in sequence)
            {
                if (!predicate(item))
                {
                    maxPosition = tmpPosition;
                }

                tmpPosition++;
            }

            int currPosition = 0;
            foreach (var item in sequence)
            {
                if (currPosition > maxPosition)
                {
                    yield break;
                }
                else
                {
                    yield return item;
                }

                currPosition++;
            }
        }

        /// <summary>
        /// Не меняет порядок следования элементов.
        /// Выбрасывает исключение если sequence.Count < count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> sequence, int count)
        {
            if (count == 0)
            {
                yield break;
            }

            var past = sequence.Take(count).ToArray();
            if (past.Length != count)
            {
                throw new ArgumentOutOfRangeException("past.Length != count");
            }
            else
            {
                foreach (var item in sequence)
                {
                    past.MoveLeftSelf(1);
                    past[count - 1] = item;
                }
                foreach (var item in past)
                {
                    yield return item;
                }
            }
        }
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> sequence, int count, bool throwIfNotEnough)
        {
            if (count == 0)
            {
                yield break;
            }

            var past = sequence.Take(count).ToArray();
            if (past.Length != count && throwIfNotEnough)
            {
                throw new ArgumentOutOfRangeException("past.Length != count");
            }
            else
            {
                foreach (var item in sequence)
                {
                    past.MoveLeftSelf(1);
                    past[past.Length - 1] = item;
                }
                foreach (var item in past)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// This method does not use <seealso cref="object.GetHashCode"/>, so it can be slow.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<TKey, T>> Group<T, TKey>(this IEnumerable<T> sequence, 
            Func<T, TKey> keyExtractor, Func<TKey, TKey, bool> comparer)
        {
            return sequence.GroupBy(v => keyExtractor(v), v => v, new IEqualityComparerAction<TKey>(comparer));
        }
        /// <summary>
        /// This method does not use <seealso cref="object.GetHashCode"/>, so it can be slow.
        /// Consider using <seealso cref="Distinct{T}(IEnumerable{T}, Func{T, T, bool}, Func{T, int})"/>
        /// instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> sequence, Func<T, T, bool> comparer)
        {
            return sequence.Distinct(new IEqualityComparerAction<T>(comparer));
        }
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> sequence, Func<T, T, bool> comparer, Func<T, int> getHashCode)
        {
            return sequence.Distinct(new IEqualityComparerAction<T>(comparer, getHashCode));
        }
        /// <summary>
        /// Uses type's default IEqualityComparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="keyExtractor"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> keyExtractor)
        {
            var keyComparer = EqualityComparer<TKey>.Default;
            var comparer = new IEqualityComparerAction<T>((a, b) => keyComparer.Equals(keyExtractor(a), keyExtractor(b)));
            return sequence.Distinct(comparer);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach(var e in sequence)
            {
                action(e);
            }

            return sequence;
        }
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)
        {
            int index = 0;

            foreach (var elem in sequence)
            {
                action(elem, index);
                index++;
            }

            return sequence;
        }

        #region ##### FindXXX #####

        public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return source.Find(predicate).Index;
        }
        public static int FindIndex<T>(this IEnumerable<T> source, IEnumerable<T> what)
        {
            return source.Find(what).Index;
        }
        public static IEnumerable<int> FindAllIndexes<T>(this IEnumerable<T> source, IEnumerable<T> what)
        {
            return source.FindAll(what).Select(fi => fi.Index);
        }

        public static FindResult<T> Find<T>(this IEnumerable<T> array, T value)
        {
            return array.Find(new[] { value });
        }
        public static IEnumerable<FindResult<T>> FindAll<T>(this IEnumerable<T> array, T value)
        {
            return array.FindAll(new[] { value });
        }
        public static FindResult<T> Find<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int i = 0;
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return new FindResult<T>(i, item);
                }
                i++;
            }

            return new FindResult<T>();
        }
        public static FindResult<T> Find<T>(this IEnumerable<T> source, IEnumerable<T> what)
        {
            return source.Find(what, 0);
        }
        public static FindResult<T> FindLast<T>(this IEnumerable<T> source, IEnumerable<T> what)
        {
            return source.FindAll(what).LastOrDefault() ?? new FindResult<T>();
        }
        public static async Task<FindResult<T>> FindAsync<T>(this IEnumerable<T> source, IEnumerable<T> what)
        {
            return await Task.Run(() => source.Find(what, 0));
        }
        public static async Task<FindResult<T>> FindAsync<T>(this IEnumerable<T> source, IEnumerable<T> what, int startIndex)
        {
            return await Task.Run(() => source.Find(what, startIndex));
        }
        public static FindResult<T> Find<T>(this IEnumerable<T> source, IEnumerable<T> what, int startIndex)
        {
            List<T> sourceCopy = new List<T>(source);
            List<T> whatList = what.ToList();

            int removedCount = 0;
            while (sourceCopy.Count >= whatList.Count)
            {
                int i = sourceCopy.FindIndex(startIndex, b => b.Equals(whatList[0]));
                if (i == -1 || (sourceCopy.Count - i) < whatList.Count)
                    break;

                int j;
                for (j = 0; j < whatList.Count; j++)
                {
                    if (!whatList[j].Equals(sourceCopy[i + j]))
                        break;
                    else if (j == whatList.Count - 1)
                        return new FindResult<T>(removedCount + i, source);
                }

                sourceCopy.RemoveRange(0, i + 1);
                removedCount += i + 1;
            }

            return new FindResult<T>();
        }
        public static IEnumerable<FindResult<T>> FindAll<T>(this IEnumerable<T> source, IEnumerable<T> what)
        {
            List<T> sourceCopy = new List<T>(source);
            List<T> whatList = what.ToList();
            List<int> positions = new List<int>();

            int removedCount = 0;
            while (sourceCopy.Count >= whatList.Count)
            {
                int i = sourceCopy.FindIndex(b => b.Equals(whatList[0]));
                if (i == -1 || (sourceCopy.Count - i) < whatList.Count)
                    break;

                int j;
                for (j = 0; j < whatList.Count; j++)
                {
                    if (!whatList[j].Equals(sourceCopy[i + j]))
                        break;
                    else if (j == whatList.Count - 1)
                    {
                        positions.Add(removedCount + i);
                        yield return new FindResult<T>(removedCount + i, source);
                    }
                }

                sourceCopy.RemoveRange(0, i + 1);
                removedCount += i + 1;
            }
        }

        #endregion

        public static IEnumerable<double> NaNToZero(this IEnumerable<double> numbers)
        {
            return numbers
                .Select(n => n.IsNaN() ? 0 : n);
        }

        public static int Count<T>(this IEnumerable<T> values, T element)
        {
            return values
                .Where(v => v.Equals(element))
                .Count();
        }
        public static bool ContainsAll<T>(this IEnumerable<T> sequence, params T[] elements)
        {
            var remainingElements = elements.ToList();
            foreach (var value in sequence)
            {
                remainingElements.Remove(value);
            }

            return remainingElements.Count == 0;
        }

        public static List<double> Sum(this IEnumerable<double> a1, IEnumerable<double> a2)
        {
            return a1.ToList().SumSelf(a2.ToList());
        }
        public static List<double> Sub(this IEnumerable<double> a1, IEnumerable<double> a2)
        {
            return a1.ToList().SubSelf(a2.ToList());
        }
        public static List<double> Mul(this IEnumerable<double> a1, IEnumerable<double> a2)
        {
            return a1.ToList().MulSelf(a2.ToList());
        }
        public static List<double> Div(this IEnumerable<double> a1, IEnumerable<double> a2)
        {
            return a1.ToList().DivSelf(a2.ToList());
        }
        public static IEnumerable<double> InvertSignEach(this IEnumerable<double> values)
        {
            return values.Select(v => -v);
        }

        #region ##### Primitives de/serialization #####

        public static List<byte> Serialize(this IEnumerable<int> values)
        {
            List<byte> serialized = new List<byte>(values.Count() * sizeof(int));
            foreach (var v in values)
            {
                serialized.AddRange(BitConverter.GetBytes(v));
            }

            return serialized;
        }
        public static List<byte> Serialize(this IEnumerable<float> values)
        {
            List<byte> serialized = new List<byte>(values.Count() * sizeof(float));
            foreach (var v in values)
            {
                serialized.AddRange(BitConverter.GetBytes(v));
            }

            return serialized;
        }
        public static List<byte> Serialize(this IEnumerable<double> values)
        {
            List<byte> serialized = new List<byte>(values.Count() * sizeof(double));
            foreach (var v in values)
            {
                serialized.AddRange(BitConverter.GetBytes(v));
            }

            return serialized;
        }

        public static List<T> Deserialize<T>(this IEnumerable<byte> values)
            where T : struct

        {
            return null;
        }

        #endregion

        #region ##### BASIC MATH OP #####

        public static IEnumerable<double> DivEach(this IEnumerable<double> numbers, double value)
        {
            return numbers.Select(n => n / value);
        }
        public static IEnumerable<double> MulEach(this IEnumerable<double> numbers, double value)
        {
            return numbers.Select(n => n * value);
        }

        public static IEnumerable<double> AddEach(this IEnumerable<double> numbers, double value)
        {
            return numbers.Select(n => n + value);
        }
        public static IEnumerable<double> SubEach(this IEnumerable<double> numbers, double value)
        {
            return numbers.Select(n => n - value);
        }

        public static IEnumerable<double> PowEach(this IEnumerable<double> sequence, double power)
        {
            return sequence.Select(e => Math.Pow(e, power));
        }
        public static IEnumerable<double> RootEach(this IEnumerable<double> sequence, double root)
        {
            return sequence.Select(e => Math.Pow(e, 1 / root));
        }

        public static IEnumerable<double> AbsEach(this IEnumerable<double> sequence)
        {
            return sequence.Select(e => Math.Abs(e));
        }
        public static IEnumerable<double> RoundEach(this IEnumerable<double> sequence, int decimals)
        {
            return sequence.Select(e => Math.Round(e, decimals));
        }

        #endregion

        #endregion

        //public static List<T> Sum<T>(this IEnumerable<T> a1, IEnumerable<T> a2)
        //{
        //    var t = typeof(T);
        //    var addOpType = typeof(Func<T, T, T>);
        //    Func<T, T, T> add;
        //    if (t.IsPrimitive)
        //    {
        //        var adder = new DynamicMethod("_", t, new[] { t, t });
        //        var gen = adder.GetILGenerator();             
        //        gen.Emit(OpCodes.Ldarg_0);
        //        gen.Emit(OpCodes.Ldarg_1);
        //        gen.Emit(OpCodes.Add);
        //        gen.Emit(OpCodes.Ret);
        //        add = (Func<T, T, T>)adder.CreateDelegate(addOpType);
        //    }
        //    else
        //    {
        //        add = (Func<T, T, T>)typeof(T).GetMethod("op_Addition").CreateDelegate(typeof(Func<T, T, T>));
        //    }

        //    var result = a1.ToList();
        //    var i = 0;
        //    foreach (var value in a2)
        //    {
        //        result[i] = add(result[i], value);
        //        i++;
        //    }

        //    return result;
        //}

        #region ##### ICollection #####

        public static bool NotContains<T>(this ICollection<T> collection, T item)
        {
            return !collection.Contains(item);
        }
        public static bool NotContains<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            return !collection.Any(predicate);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        #endregion

        #region ##### IList #####

        static void removeRange<T>(IList<T> collection, int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                collection.RemoveAt(index);
            }
        }
        static List<T> getRange<T>(IList<T> collection, int index, int count)
        {
            List<T> range = new List<T>();
            for (int i = index; i < index + count; i++)
            {
                range.Add(collection[i]);
            }
            return range;
        }
        static void addRange<T>(IList<T> collection, IEnumerable<T> array)
        {
            foreach(T t in array)
            {
                collection.Add(t);
            }
        }

        public static IList<TElement> SetAllExept<TElement>(this IList<TElement> array, TElement value, params TElement[] exeptValues)
        {
            for (int i = 0; i < array.Count; i++)
            {
                var item = array[i];
                if (!exeptValues.Contains(item))
                {
                    array[i] = value;
                }
            }

            return array;
        }

        public static IList<T> Remove<T>(this IList<T> array,  Func<T, bool> predicate)
        {
            for (int i = 0; i < array.Count; i++)
            {
                var item = array[i];
                if (predicate(item))
                {
                    array.RemoveAt(i);
                    i--;
                }
            }

            return array;
        }

        public static IList<T> ForEachSelf<T>(this IList<T> collection, Func<T, T> transformer)
        {
            return ForEachSelf(collection, (i, v) => transformer(v));
        }
        public static IList<T> ForEachSelf<T>(this IList<T> collection, Func<int, T, T> transformer)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                collection[i] = transformer(i, collection[i]);
            }

            return collection;
        }

        /// <summary>
        /// Аналог побитового кольцевого сдвига 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IList<T> MoveRightSelf<T>(this IList<T> collection, int count)
        {
            if (count < 0)
            {
                throw new NotSupportedException();
            }

            return collection.MoveSelf(count);
        }

        public static IList<T> MoveLeftSelf<T>(this IList<T> collection, int count)
        {
            if (count < 0)
            {
                throw new NotSupportedException();
            }

            return collection.MoveSelf(-count);
        }

        public static IList<T> MoveSelf<T>(this IList<T> collection, int count)
        {
            if (collection.Count == 0)
            {
                return collection;
            }
            else if (collection.Count < count.Abs())
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (count > 0)
            {
                var tmp = new List<T>(collection);
                for (int i = 0; i < count; i++)
                {
                    collection[i] = tmp[collection.Count - count + i];
                }
                for (int i = 0; i < collection.Count - count; i++)
                {
                    collection[i + count] = tmp[i];
                }
            }
            else if (count < 0)
            {
                count = count.Abs();
                var tmp = new List<T>(collection);
                for (int i = collection.Count - count, j = 0;  i < collection.Count; i++, j++)
                {
                    collection[i] = tmp[j];
                }
                for (int i = 0; i < collection.Count - count; i++)
                {
                    collection[i] = tmp[i + count];
                }
            }

            return collection;
        }

        #region ##### Get #####

        public static ReadWriteIterator<T> GetReadWriteIterator<T>(this IList<T> collection)
            where T : struct
        {
            return new ReadWriteIterator<T>(collection);
        }

        public static List<T> GetMixed<T>(this IList<T> array)
        {
            var mixed = new List<T>(array.Count);
            Global.Random
                .NextUnique(0, array.Count, array.Count)
                .ForEach((index) => mixed.Add(array[index]));

            return mixed;
        }

        #endregion

        #region ##### Get / Set #####

        public static T FindOrDefault<T>(this IList<T> array, Func<T, bool> predicate)
        {
            foreach (T item in array)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default(T);
        }

        public static T GetFromEnd<T>(this IList<T> array, int index)
        {
            return array[array.Count - 1 - index];
        }

        public static T FirstElement<T>(this IList<T> array)
        {
            return array[0];
        }
        public static T LastElement<T>(this IList<T> array)
        {
            return array[array.Count - 1];
        }

        public static T FirstElementOrNew<T>(this IList<T> array)
            where T : new()
        {
            return array.Count == 0 ? new T() : array[0];
        }
        public static T LastElementOrNew<T>(this IList<T> array)
            where T : new()
        {
            return array.Count == 0 ? new T() : array[array.Count - 1];
        }
        public static T FirstElementOrDefault<T>(this IList<T> array, T defaultValue = default(T))
        {
            return array.Count == 0 ? defaultValue : array[0];
        }
        public static T LastElementOrDefault<T>(this IList<T> array, T defaultValue = default(T))
        {
            return array.Count == 0 ? defaultValue : array[array.Count - 1];
        }

        public static IList<T> Set<T>(this IList<T> array, T value, int startIndex, int endIndex)
        {
            var indexes = ArrayUtils.CreateListByRange(startIndex, 1, endIndex - startIndex);
            return array.Set(value, indexes.ToArray());
        }
        public static IList<T> Set<T>(this IList<T> array, T value, params int[] indexes)
        {
            return array.Set(value, (IEnumerable<int>)indexes);
        }
        public static IList<T> Set<T>(this IList<T> array, T value, IEnumerable<int> indexes)
        {
            foreach (var i in indexes)
            {
                array[i] = value;
            }

            return array;
        }
        public static IList<T> SetExchange<T>(this IList<T> array, T newValue, params T[] oldValues)
        {
            for (int i = 0; i < oldValues.Length; i++)
            {
                var valToSet = oldValues[i];
                for (int j = 0; j < array.Count; j++)
                {
                    var val = array[j];
                    if (val.Equals(valToSet))
                    {
                        array[j] = newValue;
                    }
                }
            }

            return array;
        }

        public static IList<T> SetAll<T>(this IList<T> array, T value)
        {
            for (int i = 0; i < array.Count; i++)
            {
                array[i] = value;
            }

            return array;
        }

        #endregion

        #region ##### Sum~ Sub~ Mul~ Div~ InvertSignEach~ Self #####

        public static T SumSelf<T>(this T a1, T a2)
            where T : IList<double>
        {
            return a1.sumSelf<T, T>(a2);
        }
        public static T SubSelf<T>(this T a1, T a2)
            where T : IList<double>
        {
            return a1.subSelf<T, T>(a2);
        }
        public static T MulSelf<T>(this T a1, T a2)
            where T : IList<double>
        {
            return a1.mulSelf<T, T>(a2);
        }
        public static T DivSelf<T>(this T a1, T a2)
            where T : IList<double>
        {
            return a1.divSelf<T, T>(a2);
        }
       
        static T1 sumSelf<T1, T2>(this T1 a1, T2 a2)
            where T1 : IList<double>
            where T2 : IList<double>
        {
            for (int i = 0; i < a1.Count; i++)
            {
                a1[i] += a2[i];
            }

            return a1;
        }
        static T1 subSelf<T1, T2>(this T1 a1, T2 a2)
            where T1 : IList<double>
            where T2 : IList<double>
        {
            for (int i = 0; i < a1.Count; i++)
            {
                a1[i] -= a2[i];
            }

            return a1;
        }
        static T1 mulSelf<T1, T2>(this T1 a1, T2 a2)
            where T1 : IList<double>
            where T2 : IList<double>
        {
            for (int i = 0; i < a1.Count; i++)
            {
                a1[i] *= a2[i];
            }

            return a1;
        }
        static T1 divSelf<T1, T2>(this T1 a1, T2 a2)
            where T1 : IList<double>
            where T2 : IList<double>
        {
            for (int i = 0; i < a1.Count; i++)
            {
                a1[i] /= a2[i];
            }

            return a1;
        }

        public static T InvertSignEachSelf<T>(this T values)
            where T : IList<double>
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i] *= -1;
            }

            return values;
        }

#endregion

        public static IList<T> TruncateByCount<T>(this IList<T> array, int count, bool throwIfShorter = true)
        {
            int collectionCount = array.Count;
            ThrowUtils.ThrowIf_Negative(count);
            ThrowUtils.ThrowIf_True(count > collectionCount && throwIfShorter, "Truncate operation error: count > collectionCount ({0} > {1})".Format(count, collectionCount));

            if (collectionCount > count && collectionCount != 0)
            {
                removeRange(array, count, collectionCount - count);
            }

            return array;
        }

        #region ##### AddXXX like methods #####

        public static IList<T> Add<T>(this IList<T> array, T element, int count)
        {
            ThrowUtils.ThrowIf_Negative(count);

            for (int i = 0; i < count; i++)
            {
                array.Add(element);
            }

            return array;
        }
        public static IList<T> Add<T>(this IList<T> array, T element)
        {
            return array.Add(element, 1);
        }
        public static IList<T> AddRange<T>(this IList<T> array, params T[] elements)
        {
            addRange(array, elements);
            return array;
        }

        #endregion

        #region ##### GetRange like methods #####

        public static IList<T> Min<T>(this IList<T> array, Func<IList<T>, double> aggregator, int subarrayLength)
        {
            return array
                .Max(subArr => -aggregator(subArr), subarrayLength);
        }
        public static IList<T> Max<T>(this IList<T> array, Func<IList<T>, double> aggregator, int subarrayLength)
        {
            ThrowUtils.ThrowIf_Negative_Zero(subarrayLength);
            ThrowUtils.ThrowIf_NullArgument(aggregator);
            if (array.Count == 0 || array.Count <= subarrayLength)
                return array;

            Dictionary<int, double> index_aggregation_pair = new Dictionary<int, double>();
            for (int i = 0; i < array.Count - subarrayLength; i++)
            {
                double aggregation = aggregator(array.SubArray(i, subarrayLength));
                index_aggregation_pair.Add(i, aggregation);
            }

            int maxIndex = index_aggregation_pair.Max(kvp => kvp.Key);
            return array.SubArray(maxIndex, subarrayLength);
        }

        public static List<T> SubArray<T>(this IList<T> array, int startIndex, int subarrayLength)
        {
            ThrowUtils.ThrowIf_Negative(startIndex, subarrayLength);
            ThrowUtils.ThrowIf_Negative_Zero(subarrayLength);
            ThrowUtils.ThrowIf_True(array.Count - startIndex < subarrayLength, "Cannot extract subarray because of collection length smaller ({0}) than last index ({1})".Format(array.Count, startIndex + subarrayLength));
            if (subarrayLength == 0 || array.Count == 0)
            {
                return new List<T>();
            }

            List<T> subArray = new List<T>();
            for (int i = 0; i < subarrayLength; i++)
            {
                T val = array[startIndex + i];
                subArray.Add(array[startIndex + i]);
            }

            return subArray;
        }
        public static List<T> SubArray<T>(this IList<T> array, IList<int> indexes)
        {
            if (indexes.Count == 0 || array.Count == 0)
            {
                return new List<T>();
            }

            List<T> subArray = new List<T>(indexes.Count);
            for (int i = 0; i < indexes.Count; i++)
            {
                T val = array[indexes[i]];
                subArray.Add(val);
            }

            return subArray;
        }
        public static List<T> SubArray<T>(this IList<T> array, IEnumerable<int> indexes)
        {
            List<T> subArray = new List<T>();
            foreach (int i in indexes)
            {
                T val = array[i];
                subArray.Add(val);
            }

            return subArray;
        }

        public static IList<T> FirstHalf<T>(this IList<T> array)
        {
            ThrowUtils.ThrowIf_True(array.Count % 2 != 0, "Cannot get half of array if its length odd");

            if (array.Count == 0)
            {
                return array;
            }

            return getRange(array, 0, array.Count / 2);
        }

        public static List<List<T>> SubArrays<T>(this IList<T> source, int groupLength)
        {
            return source.SubArrays(groupLength, false);
        }

        // Хорошо работает
        public static List<List<T>> SubArrays<T>(this IList<T> source, int groupLength, bool reverseGroups)
        {
            ThrowUtils.ThrowIf_Negative_Zero(groupLength);
            ThrowUtils.ThrowIf_ZeroLength(source);
            ThrowUtils.ThrowIf_True(source.Count % groupLength != 0, "Cannot separate array by groups if it's not contains whole number of groups");

            List<List<T>> groups = new List<List<T>>();

            for (int i = 0; i < source.Count; i += groupLength)
            {
                List<T> group = getRange(source, i, groupLength);

                if (reverseGroups)
                {
                    group.Reverse();
                }
                groups.Add(group);
            }

            return groups;
        }

        public static List<T> Cut<T>(this IList<T> array, int startIndex, int cutoffLength)
        {
            List<T> area = getRange(array, startIndex, cutoffLength);
            removeRange(array, startIndex, cutoffLength);
            return area;
        }

        #endregion

        public static bool Contains<T>(this IList<T> source, IList<T> what)
        {
            return Find(source, what).Found;
        }

        public static T Replace<T, TElement>(this T array, TElement oldValue, TElement newValue)
            where T : IList<object>
        {
            for (int i = 0; i < array.Count; i++)
            {
                var v = array[i];
                array[i] = v.Equals(oldValue) ? newValue : v;
            }

            return array;
        }

        #region ##### BASIC MATH OP #####

        public static T DivEachSelf<T>(this T numbers, double value)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] /= value;
            }

            return numbers;
        }
        public static T MulEachSelf<T>(this T numbers, double value)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] *= value;
            }

            return numbers;
        }

        public static T AddEachSelf<T>(this T numbers, double value)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] += value;
            }

            return numbers;
        }
        public static T SubEachSelf<T>(this T numbers, double value)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] -= value;
            }

            return numbers;
        }

        public static T PowEachSelf<T>(this T numbers, double power)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = Math.Pow(numbers[i], power);
            }

            return numbers;
        }
        public static T RootEachSelf<T>(this T numbers, double root)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = Math.Pow(numbers[i], 1 / root);
            }

            return numbers;
        }

        public static T AbsEachSelf<T>(this T numbers)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = Math.Abs(numbers[i]);
            }

            return numbers;
        }
        public static T RoundEachSelf<T>(this T numbers, int decimals)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                numbers[i] = Math.Round(numbers[i], decimals);
            }

            return numbers;
        }

        #endregion

        #endregion

        #region ##### IDictionary #####

        public static TValue TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> sequence, TKey key, 
            TValue defaultValue = default)
        {
            var isFound = sequence.TryGetValue(key, out TValue value);

            return isFound ? value : defaultValue;
        }

        public static IDictionary<TKey, TValue> ForEach<TKey, TValue>(this IDictionary<TKey, TValue> sequence, Action<TKey, TValue> action)
        {
            foreach (var e in sequence)
            {
                action(e.Key, e.Value);
            }

            return sequence;
        }
        public static IDictionary<TKey, TValue> ForEach<TKey, TValue>(this IDictionary<TKey, TValue> sequence, Action<TKey, TValue, int> action)
        {
            int index = 0;

            foreach (var e in sequence)
            {
                action(e.Key, e.Value, index);
                index++;
            }

            return sequence;
        }

        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>
            (this IDictionary<TKey, TValue> sourceDictonary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(sourceDictonary);
        }

        public static bool NotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return !dictionary.ContainsKey(key);
        }

        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            var keysArr = keys.ToArray();
            foreach (var key in keysArr)
            {
                dictionary.Remove(key);
            }
        }

        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, bool> selector)
        {
            List<TKey> toRemove = new List<TKey>();
            foreach (var kvp in dictionary)
            {
                if (selector(kvp.Key))
                {
                    toRemove.Add(kvp.Key);
                }
            }

            foreach (var key in toRemove)
            {
                dictionary.Remove(key);
            }
        }

        #endregion

        #region ##### Array #####

        public static IEnumerable<T> Enumerate<T>(this T[,] array2d)
        {
            for (int row = 0; row < array2d.GetLength(0); row++)
            {
                for (int column = 0; column < array2d.GetLength(1); column++)
                {
                    yield return array2d[row, column];
                }
            }
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }

        #endregion

        #region ##### List #####

        public static IEnumerable<T> ToEnumerable<T>(this List<T> list)
        {
            return list;
        }

        #endregion
    }
}