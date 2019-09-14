using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;
using System.Runtime.CompilerServices;

namespace Utilities.Extensions
{
    public static class CommonEx
    {
        public static IEnumerable<T> ToSequence<T>(this T value)
        {
            yield return value;
        }

        ///// <summary>
        ///// Works only for Enums!
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <returns><see cref="null"/> if there is no description attribute</returns>
        //public static string GetEnumValueDescription<T>(this T value) 
        //    where T : struct
        //{
        //    var t = typeof(T);
        //    if (t.IsEnum)
        //    {
        //        var memInfo = t.GetMember(value.ToString());
        //        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        return (attributes.SingleOrDefault() as DescriptionAttribute)?.Description;
        //    }
        //    else
        //    {
        //        throw new NotSupportedException("This method supposed to be used with Enums!");
        //    }
        //}

        public static IEnumerable<int> Range(this int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
            }
        }

        public static bool NullToFalse(this object value)
        {
            return value != null;
        }

        public static bool IsOneOf<T>(this T value, params T[] values)
        {
            foreach (T v in values)
            {
                if (value.Equals(v))
                {
                    return true;
                }
            }

            return false;
        }

        public static T To<T>(this object obj)
        {
            return (T)obj;
        }

        public static T Ternar<T>(this bool condition, T trueResult, T falseResult)
        {
            return condition ? trueResult : falseResult;
        }

        //public static T MakeDeepClone<T>(this T obj)
        //{
        //    var serializer = new BinaryFormatter();
        //    var stream = new MemoryStream();
        //    serializer.Serialize(stream, obj);
        //    stream.Position = 0;

        //    return (T)serializer.Deserialize(stream);
        //}

        ///// <summary>
        ///// Uses <see cref="BinaryFormatter"/>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static IEnumerable<byte> Serialize<T>(this T obj)
        //{
        //    var serializer = new BinaryFormatter();
        //    var stream = new MemoryStream();
        //    serializer.Serialize(stream, obj);

        //    return stream.ToArray();
        //}
        /// <summary>
        /// Uses <see cref="BinaryFormatter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public static T Deserialize<T>(this IEnumerable<byte> obj)
        //{
        //    var serializer = new BinaryFormatter();
        //    var stream = new MemoryStream(obj.ToArray());

        //    return (T)serializer.Deserialize(stream);
        //}
        ///// <summary>
        ///// Uses <see cref="BinaryFormatter"/>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static T Deserialize<T>(this Stream obj)
        //{
        //    var serializer = new BinaryFormatter();

        //    return (T)serializer.Deserialize(obj);
        //}

        public static IEnumerable<T> Select<T>(this object obj, params (Func<object, bool> predicate, T result)[] conditions)
        {
            foreach (var c in conditions)
            {
                if(c.predicate(obj))
                {
                    yield return c.result;
                }
            }
        }
    }
}
