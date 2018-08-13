using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class ParsingEx
    {
        const NumberStyles IntegerStyle = NumberStyles.Any ^ NumberStyles.AllowThousands;

        public static sbyte? TryParseToSByteInvariant(this string str)
        {
            var ok = sbyte.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out sbyte parsed);
            return ok ? (sbyte?)parsed : null;
        }
        public static byte? TryParseToByteInvariant(this string str)
        {
            var ok = byte.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out byte parsed);
            return ok ? (byte?)parsed : null;
        }
        public static short? TryParseToInt16Invariant(this string str)
        {
            var ok = short.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out short parsed);
            return ok ? (short?)parsed : null;
        }
        public static ushort? TryParseToUInt16Invariant(this string str)
        {
            var ok = ushort.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out ushort parsed);
            return ok ? (ushort?)parsed : null;
        }
        public static int? TryParseToInt32Invariant(this string str)
        {
            var ok = int.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out int parsed);
            return ok ? (int?)parsed : null;
        }
        public static uint? TryParseToUInt32Invariant(this string str)
        {
            var ok = uint.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out uint parsed);
            return ok ? (uint?)parsed : null;
        }
        public static long? TryParseToInt64Invariant(this string str)
        {
            var ok = long.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out long parsed);
            return ok ? (long?)parsed : null;
        }
        public static ulong? TryParseToUInt64Invariant(this string str)
        {
            var ok = ulong.TryParse(str, IntegerStyle, CultureInfo.InvariantCulture, out ulong parsed);
            return ok ? (ulong?)parsed : null;
        }
        public static double? TryParseToDoubleInvariant(this string str)
        {
            var ok = double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsed);
            return ok ? (double?)parsed : null;
        }
        public static float? TryParseToSingleInvariant(this string str)
        {
            var ok = float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed);
            return ok ? (float?)parsed : null;
        }


        public static IEnumerable<int> ParseToInt32Invariant(this IEnumerable<string> str)
        {
            return str.Select(ParseToInt32Invariant);
        }
        public static IEnumerable<byte> ParseToByteInvariant(this IEnumerable<string> str)
        {
            return str.Select((s) => ParseToByteInvariant(s));
        }
        public static IEnumerable<double> ParseToDoubleInvariant(this IEnumerable<string> str)
        {
            return str.Select(ParseToDoubleInvariant);
        }

        public static DateTime ParseToDateTimeInvariant(this string str, string format)
        {
            return DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }
        public static ushort ParseToUInt16Invariant(this string str)
        {
            return ushort.Parse(str, IntegerStyle, CultureInfo.InvariantCulture);
        }
        public static int ParseToInt32Invariant(this string str)
        {
            return int.Parse(str, IntegerStyle, CultureInfo.InvariantCulture);
        }
        public static byte ParseToByteInvariant(this string str)
        {
            return byte.Parse(str, IntegerStyle, CultureInfo.InvariantCulture);
        }
        public static byte ParseToByteInvariant(this string str, int @base)
        {
            return Convert.ToByte(str, @base);
        }
        public static float ParseToSingleInvariant(this string str)
        {
            return float.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
        }
        public static double ParseToDoubleInvariant(this string str)
        {
            return double.Parse(str, NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public static double ParseToDouble(this string str, CultureInfo culture)
        {
            return double.Parse(str, NumberStyles.Float, culture);
        }
    }
}
