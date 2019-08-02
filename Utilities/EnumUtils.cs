﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.Extensions;

namespace Utilities
{
    public static class EnumUtils
    {
        static Random _rnd = Global.Random;

        public static T GetRandom<T>()
            where T : struct
        {
            Type enumType = typeof(T);
            string[] names = Enum.GetNames(enumType);
            string random = names[_rnd.Next(0, names.Length)];

            return (T)Enum.Parse(enumType, random);
        }

        /// <summary>
        /// Throws exception if <typeparamref name="TEnum"/> doesn't contain a value for given <paramref name="value"/>
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum CastSafe<TEnum>(int value)
            where TEnum : struct
        {
            var enumValue = GetValues<TEnum>().Find(v => v.Equals((TEnum)(dynamic)value));
            if (enumValue.Found)
            {
                return enumValue.Value;
            }
            else
            {
                throw new InvalidOperationException($"Enum {typeof(TEnum).Name} doesn't have value {value}");
            }
        }

        public static IEnumerable<T> GetValues<T>()
            where T : struct
        {
            return (T[])Enum.GetValues(typeof(T)); 
        }

        public static string[] GetNames<T>()
            where T : struct
        {
            return Enum.GetNames(typeof(T));
        }

        public static T ParseOrDefault<T>(string value) 
            where T : struct
        {
            bool ok = GetNames<T>().Contains(value);
            return ok ? (T)Enum.Parse(typeof(T), value.ToUpperInvariant()) : default(T);
        }
        public static T Parse<T>(string value)
            where T : struct
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
