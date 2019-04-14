using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class CommonEx
    {
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

        public static T MakeDeepClone<T>(this T obj)
        {
            var serializer = new BinaryFormatter();
            var stream = new MemoryStream();
            serializer.Serialize(stream, obj);
            stream.Position = 0;

            return (T)serializer.Deserialize(stream);
        }

        /// <summary>
        /// Uses <see cref="BinaryFormatter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<byte> Serialize<T>(this T obj)
        {
            var serializer = new BinaryFormatter();
            var stream = new MemoryStream();
            serializer.Serialize(stream, obj);

            return stream.ToArray();
        }
        /// <summary>
        /// Uses <see cref="BinaryFormatter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this IEnumerable<byte> obj)
        {
            var serializer = new BinaryFormatter();
            var stream = new MemoryStream(obj.ToArray());

            return (T)serializer.Deserialize(stream);
        }
        /// <summary>
        /// Uses <see cref="BinaryFormatter"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this Stream obj)
        {
            var serializer = new BinaryFormatter();

            return (T)serializer.Deserialize(obj);
        }
    }
}
