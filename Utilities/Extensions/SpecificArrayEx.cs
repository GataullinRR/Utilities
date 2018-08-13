using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class SpecificArrayEx
    {
        #region ##### IEnumerable #####

        public static string ToBase64(this IEnumerable<byte> sequence)
        {
            return Convert.ToBase64String(sequence.ToArray());
        }

        public static MemoryStream ToMemoryStream(this IEnumerable<byte> sequence)
        {
            return new MemoryStream(sequence.ToArray());
        }

        /// <summary>
        /// Преобразует <paramref name="sequence"/> в строку
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static string Aggregate(this IEnumerable<char> sequence)
        {
            return new string(sequence.ToArray());
        }

        public static int Sum(this IEnumerable<byte> sequence)
        {
            return sequence.Aggregate(0, (last, val) => val + last);
        }

        public static IEnumerable<TValue> SelectKVPValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> sequence)
        {
            return sequence.Select(kvp => kvp.Value);
        }
        public static IEnumerable<TKey> SelectKVPKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> sequence)
        {
            return sequence.Select(kvp => kvp.Key);
        }

        #endregion

        #region ##### ICollection #####

        #endregion

        #region ##### IList #####

        public static T InsertZeros<T>(this T a1, int position, int count)
            where T : IList<double>
        {
            for (int i = 0; i < count; i++)
            {
                a1.Insert(position, 0);
            }

            return a1;
        }

        public static IList<double?> ToNullable(this IList<double> sourceArray, IList<bool> nullMask)
        {
            ThrowUtils.ThrowIf_NullArgument(nullMask);

            return sourceArray
                .ToArray()
                .ToNullable(nullMask.ToArray())
                .ToList();
        }

        public static IList<bool> InvertValues(this IList<bool> sourceArray)
        {
            for (int i = 0; i < sourceArray.Count; i++)
            {
                sourceArray[i] = !sourceArray[i];
            }

            return sourceArray;
        }

        public static T NaNToZero<T>(this T numbers)
             where T : IList<double>
        {
            return numbers.ReplaceNaN(0);
        }
        public static T ReplaceNaN<T>(this T numbers, double newValue)
            where T : IList<double>
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                double n = numbers[i];
                numbers[i] = n.IsNaN() ? newValue : n;
            }

            return numbers;
        }

        public static T NormalizeBySumSelf<T>(this T values, double requiredSum = 1)
            where T : IList<double>
        {
            double sum = values.Sum();

            return values
                .DivEachSelf(sum)
                .MulEachSelf(requiredSum);
        }
        public static T NormalizeByPeakSelf<T>(this T values, double requiredPeak = 1)
            where T : IList<double>
        {
            double coef = requiredPeak / values.Max();

            return values
                .MulEachSelf(coef);
        }

        #endregion

        #region ##### Array #####

        public static double?[] ToNullable(this double[] sourceArray, bool[] nullMask)
        {
            ThrowUtils.ThrowIf_True(nullMask.Length != sourceArray.Length, "Cannot convert to nullable array because nullMask.Length != sourceArray.Length");

            double?[] nullableArray = new double?[sourceArray.Length];
            for (int i = 0; i < nullableArray.Length; i++)
            {
                nullableArray[i] = nullMask[i] ? null : (double?)sourceArray[i];
            }

            return nullableArray;
        }

        public static IEnumerable<object> ToEnumerable(this Array array)
        {
            foreach (var item in array)
            {
                yield return item;
            }
        }

        #endregion
    }
}
