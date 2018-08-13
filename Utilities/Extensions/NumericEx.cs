using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Utilities.Extensions
{
    public static class NumericEx
    {
        #region ##### ToStringInvariant #####

        public static string ToStringInvariant(this sbyte number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this byte number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this short number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this ushort number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this int number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this uint number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this long number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this ulong number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this double number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this float number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this decimal number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringInvariant(this int number, string format)
        {
            return number.ToString(format, CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this double number, string format)
        {
            return number.ToString(format, CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this float number, string format)
        {
            return number.ToString(format, CultureInfo.InvariantCulture);
        }
        public static string ToStringInvariant(this decimal number, string format)
        {
            return number.ToString(format, CultureInfo.InvariantCulture);
        }

        #endregion

        #region ##### ToXXX #####

        public static char ToChar(this short value) => Convert.ToChar(value);
        public static char ToChar(this int value) => Convert.ToChar(value);

        public static byte ToByte(this decimal value) => Convert.ToByte(value);
        public static byte ToByte(this double value) => Convert.ToByte(value);
        public static byte ToByte(this float value) => Convert.ToByte(value);
        public static byte ToByte(this short value) => Convert.ToByte(value);
        public static byte ToByte(this char value) => Convert.ToByte(value);
        public static byte ToByte(this int value) => Convert.ToByte(value);

        public static int ToInt32(this decimal value) => Convert.ToInt32(value);
        public static int ToInt32(this double value) => Convert.ToInt32(value);
        public static int ToInt32(this float value) => Convert.ToInt32(value);
        public static int ToInt32(this short value) => Convert.ToInt32(value);
        public static int ToInt32(this char value) => Convert.ToInt32(value);
        public static int ToInt32(this byte value) => Convert.ToInt32(value);
        public static int ToInt32(this uint value) => Convert.ToInt32(value);

        public static uint ToUInt32(this decimal value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this double value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this float value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this int value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this short value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this char value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this byte value) =>  Convert.ToUInt32(value);
        public static uint ToUInt32(this int val, bool negativeToZero)
        {
            return negativeToZero ? (val < 0 ? 0 : (uint)val) : (uint)Math.Abs(val);
        }

        public static UInt16 ToUInt16(this decimal value) => Convert.ToUInt16(value);
        public static UInt16 ToUInt16(this double value) => Convert.ToUInt16(value);
        public static UInt16 ToUInt16(this float value) => Convert.ToUInt16(value);
        public static UInt16 ToUInt16(this int value) => Convert.ToUInt16(value);
        public static UInt16 ToUInt16(this char value) => Convert.ToUInt16(value);
        public static UInt16 ToUInt16(this byte value) => Convert.ToUInt16(value);
        public static UInt16 ToUInt16(this int val, bool negativeToZero)
        {
            return negativeToZero ? (val < 0 ? (UInt16)0 : (UInt16)val) : (UInt16)Math.Abs(val);
        }

        public static float ToSingle(this decimal value) => Convert.ToSingle(value);
        public static float ToSingle(this double value) => Convert.ToSingle(value);
        public static float ToSingle(this short value) => Convert.ToSingle(value);
        public static float ToSingle(this char value) => Convert.ToSingle(value);
        public static float ToSingle(this byte value) => Convert.ToSingle(value);

        public static double ToDouble(this decimal value) => Convert.ToDouble(value);
        public static double ToDouble(this float value) => Convert.ToDouble(value);
        public static double ToDouble(this short value) => Convert.ToDouble(value);
        public static double ToDouble(this char value) => Convert.ToDouble(value);
        public static double ToDouble(this byte value) => Convert.ToDouble(value);

        #endregion

        #region ##### IsXXX #####

        public static bool IsInteger(this double val)
        {
            return val.Round() == val;
        }
        public static bool IsInteger(this float value)
        {
            return value == value.Round();
        }

        public static bool IsNaN(this float val)
        {
            return float.IsNaN(val);
        }
        public static bool IsNaN(this double val)
        {
            return double.IsNaN(val);
        }

        public static bool IsPositive(this int value)
        {
            return value >= 0;
        }
        public static bool IsNegative(this int value)
        {
            return value < 0;
        }
        public static int NegativeToZero(this int val)
        {
            return val < 0 ? 0 : val;
        }

        public static bool IsRangeInvalid(this int value, int rangeFrom, int rangeTo,
            bool strictFrom = false, bool strictTo = false)
        {
            return !value.IsRangeValid(rangeFrom, rangeTo, strictFrom, strictTo);
        }
        public static bool IsRangeValid(this int value, int rangeFrom, int rangeTo,
            bool strictFrom = false, bool strictTo = false)
        {
            bool isFromValid = value > rangeFrom || (value >= rangeFrom && !strictFrom);
            bool isToValid = value < rangeTo || (value <= rangeTo && !strictTo);
            return isFromValid && isToValid;
        }
        public static bool IsRangeValid(this double value, double rangeFrom, double rangeTo,
            bool strictFrom = false, bool strictTo = false)
        {
            bool isFromValid = value > rangeFrom || (value >= rangeFrom && !strictFrom);
            bool isToValid = value < rangeTo || (value <= rangeTo && !strictTo);
            return isFromValid && isToValid;
        }

        #endregion

        #region ##### Math opperations #####

        public static double Round(this double val, int decimals) => Math.Round(val, decimals);
        public static float Round(this float val, int decimals) => (float)Math.Round(val, decimals);
        public static int Round(this double val) => (int)Math.Round(val);
        public static int Round(this float val) => (int)Math.Round(val);

        public static int Ceiling(this double val) => (int)Math.Ceiling(val);
        public static int Ceiling(this float val) => (int)Math.Ceiling(val);

        public static int Floor(this double val) => (int)Math.Floor(val);
        public static int Floor(this float val) => (int)Math.Floor(val);

        public static double Pow(this double val, double power)
        {
            return Math.Pow(val, power);
        }
        public static float Pow(this float val, double power)
        {
            return Math.Pow(val, power).ToSingle();
        }
        public static int Pow(this int val, double power)
        {
            return Math.Pow(val, power).ToInt32();
        }
        public static double Root(this double val, double power)
        {
            return Math.Pow(val, 1 / power);
        }
        public static float Root(this float val, double power)
        {
            return Math.Pow(val, 1 / power).ToSingle();
        }
        public static int Root(this int val, double power)
        {
            return Math.Pow(val, 1 / power).ToInt32();
        }

        public static int Sign(this double val)
        {
            return Math.Sign(val);
        }
        public static int Sign(this float val)
        {
            return Math.Sign(val);
        }
        public static int Sign(this int val)
        {
            return Math.Sign(val);
        }

        public static double Abs(this double val)
        {
            return Math.Abs(val);
        }
        public static float Abs(this float val)
        {
            return Math.Abs(val);
        }
        public static int Abs(this int val)
        {
            return Math.Abs(val);
        }

        #endregion

        #region ##### Basic math opperations #####

        public static byte Add(this byte val1, byte val2) => (byte)(val1 + val2);
        public static int Add(this int val1, int val2) => val1 + val2;
        public static double Add(this double val1, double val2) => val1 + val2;

        public static byte Sub(this byte val1, byte val2) => (byte)(val1 - val2);
        public static int Sub(this int val1, int val2) => val1 - val2;
        public static double Sub(this double val1, double val2) => val1 - val2;

        public static int Mul(this int val1, int val2) => val1 * val2;
        public static double Mul(this double val1, double val2) => val1 * val2;

        public static int Div(this int val1, int val2) => val1 / val2;
        public static double Div(this double val1, double val2) => val1 / val2;

        #endregion

        public static T Exchange<T>(this T current, T exchangingValue, T newValue)
        {
            return current.Equals(exchangingValue) ? newValue : current;
        }

        public static byte BitXOR(this byte current, byte mask)
        {
            return (byte)(current ^ mask);
        }
        public static byte BitAND(this byte current, byte mask)
        {
            return (byte)(current & mask);
        }
        public static byte BitOR(this byte current, byte mask)
        {
            return (byte)(current | mask);
        }
        public static byte BitNOT(this byte current)
        {
            return (byte)(~current);
        }

        public static float ApplyRange(this float value, float from, float to)
        {
            return (float)((double)value).ApplyRange(from, to);
        }
        public static int ApplyRange(this int value, int from, int to)
        {
            return (int)((double)value).ApplyRange(from, to);
        }
        public static double ApplyRange(this double value, double from, double to)
        {
            ThrowUtils.ThrowIf_IntervalInvalid(from, to, true);

            if (value < from)
                return from;
            else if (value > to)
                return to;
            else
                return value;
        }
    }
}