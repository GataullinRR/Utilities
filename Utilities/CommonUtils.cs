﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities
{
    public static class CommonUtils
    {
        #region ##### Try #####

        public static async Task<bool> TryAsync(Func<Task> action)
        {
            try
            {
                await action();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Try(Action action)
        {
            return Try(action, out Exception ex);
        }
        public static bool Try(Action action, out Exception exception)
        {
            try
            {
                action();
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        public static bool Try<T>(Func<T> action, out T result)
        {
            try
            {
                result = action();
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }

        public static T TryOrDefault<T>(Func<T> func)
        {
            return TryOrDefault(func, default(T));
        }
        public static T TryOrDefault<T>(Func<T> func, T defaultResult)
        {
            return TryOrDefault(func, defaultResult, out Exception ex);
        }
        public static T TryOrDefault<T>(Func<T> func, T defaultResult, out Exception exception)
        {
            exception = null;
            T result = defaultResult;
            try
            {
                result = func();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return result;
        }
        public static bool TryOrDefault<T>(Func<T> func, out T result, T defaultResult = default(T))
        {
            try
            {
                result = func();
                return true;
            }
            catch
            {
                result = defaultResult;
                return false;
            }
        }

        //public static T TryTillNotDefault<T>(Func<T> func, T defaultResult = default(T))
        //{
        //    T result = defaultResult;
        //    while (!TryOrDefault(func, out result, defaultResult))
        //    {

        //    }

        //    return result;
        //}

        public static T TryTillSuccessOrDefault<T>(Func<T> func, int maxRetryCount, T defaultResult = default(T))
        {
            T result = defaultResult;
            var retryCount = 0;
            while(retryCount < maxRetryCount && !TryOrDefault(func, out result, defaultResult))
            {
                retryCount++;
            }

            return result;
        }
        public static async Task<T> TryTillSuccessOrDefaultAsync<T>(Func<Task<T>> func, int maxRetryCount, T defaultResult = default(T))
        {
            T result = defaultResult;
            for (int retryCount = 0; retryCount < maxRetryCount; retryCount++)
            {
                try
                {
                    result = await func();
                    break;
                }
                catch { }
            }

            return result;
        }

        public static async Task<bool> TryTillTrueOrFalseAsync(Func<Task<bool>> func, int maxRetryCount)
        {
            bool result = false;
            for (int retryCount = 0; retryCount < maxRetryCount; retryCount++)
            {
                try
                {
                    result = await func();
                    if (result)
                    {
                        break;
                    }
                }
                catch { }
            }

            return result;
        }

        #endregion

        /// <summary>
        /// Выполняет все <paramref name="actions"/> и выбрасывает <see cref="AggregateException"/> если хотябы один завершился с исключением
        /// </summary>
        /// <param name="actions"></param>
        public static void ExecuteAnyway(params Action[] actions)
        {
            var exceptions = new List<Exception>();
            foreach (var action in actions)
            {
                var ok = Try(action, out Exception ex);
                if (!ok)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Count != 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public static T Select<T>(int index, params T[] elements)
        {
            return elements[index];
        }
        public static T Select<T>(Func<T, bool> rule, params T[] elements)
        {
            foreach (var e in elements)
            {
                if (rule(e))
                {
                    return e;
                }
            }

            throw new ArgumentOutOfRangeException();
        }
        public static int Classify<T>(T element, params T[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (element?.Equals(elements[i]) ?? elements[i] == null)
                {
                    return i;
                }
            }

            throw new ArgumentOutOfRangeException();
        }

        public static ref T Assign<T>(int index, T value, ref T element1, ref T element2)
        {
            switch (index)
            {
                case 0:
                    element1 = value;
                    return ref element1;
                case 1:
                    element2 = value;
                    return ref element2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        public static ref T Assign<T>(int index, T value, ref T element1, ref T element2, ref T element3)
        {
            switch (index)
            {
                case 0:
                    element1 = value;
                    return ref element1;
                case 1:
                    element2 = value;
                    return ref element2;
                case 3:
                    element3 = value;
                    return ref element3;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static T Max<T>(Func<T, double> parameterExtractor, params T[] elements)
        {
            double maxParameter = double.MinValue;
            T maxElement = elements[0];
            foreach (var e in elements)
            {
                var parameter = parameterExtractor(e);
                if (parameter > maxParameter)
                {
                    maxParameter = parameter;
                    maxElement = e;
                }
            }

            return maxElement;
        }
        public static T Min<T>(Func<T, double> parameterExtractor, params T[] elements)
        {
            double minParameter = double.MinValue;
            T minElement = elements[0];
            foreach (var e in elements)
            {
                var parameter = parameterExtractor(e);
                if (parameter < minParameter)
                {
                    minParameter = parameter;
                    minElement = e;
                }
            }

            return minElement;
        }

        public static bool AnyTrue(params bool[] values)
        {
            return values.Any(v => v);
        }

        public static bool SatisfiesRange(string value, int from, int to, bool strictFrom = false, bool strictTo = false)
        {
            bool satisfies = int.TryParse(value, out int result);
            satisfies &= result.IsRangeValid(from, to, strictFrom, strictTo);

            return satisfies;
        }
        public static bool SatisfiesRange(string value, double from, double to, bool strictFrom = false, bool strictTo = false)
        {
            bool satisfies = double.TryParse(value, out double result);
            satisfies &= result.IsRangeValid(from, to, strictFrom, strictTo);

            return satisfies;
        }

        public static void Validate<T>(this T value, params T[] acceptableValues)
        {
            if (acceptableValues.NotContains(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "Значение не допустимо");
            }
        }



        public static void EnableAll(params IEnableable[] enableableObjs)
        {
            foreach (IEnableable obj in enableableObjs)
                obj.Enabled = true;
        }

        public static void DisableAll(params IEnableable[] enableableObjs)
        {
            foreach (IEnableable obj in enableableObjs)
                obj.Enabled = false;
        }
    }
}
