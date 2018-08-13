﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;

namespace Utilities
{
    public static class MatlabUtils
    {
        // Не хочу поднимать версию фреймворка для использования System.Numerics
        public struct ComplexT
        {
            public readonly double Re, Im;

            public ComplexT(double re, double im)
            {
                Re = re;
                Im = im;
            }

            public override string ToString()
            {
                return "Re = {0}; Im = {1};".Format(Re, Im);
            }
        }

        public static ComplexT[] ParseVariableViewRow_Complex(string row)
        {
            string[] complexNumberStrings = row.Replace(" ", "").Split('\t').ToArray();
            ComplexT[] parsed = complexNumberStrings.Select(complexFromString).ToArray();

            return parsed;

            ComplexT complexFromString(string arg)
            {
                double re = 0, im = 0;
                int imNumExpSignIndex = arg.Length - 1 - 3;
                for (int i = arg.Length - 1; i >= 0; i--)
                {
                    char currentChar = arg[i];
                    if ((currentChar == '-' || currentChar == '+') && i != imNumExpSignIndex)
                    {
                        int from = i;
                        int reNumLen = arg.Length - 1 - from;
                        string imStr = arg.Substring(from, reNumLen);
                        im = double.Parse(imStr);

                        string reStr = arg.Substring(0, i);
                        re = double.Parse(reStr);

                        return new ComplexT(re, im);
                    }
                }

                throw new ArgumentException("Bad format");
            }
        }

        public static double[] ColonOp(double arg1, double arg2)
        {
            return ColonOp(arg1, 1, arg2);
        }
        public static int[] ColonOp(int arg1, int arg2)
        {
            return ColonOp(arg1, 1, arg2);
        }
        public static double[] ColonOp(double arg1, double arg2, double arg3)
        {
            int length = (int)((arg3 - arg1) / arg2);
            length = length < 0 ? 0 : length + 1;
            var result = new double[length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arg1 + i * arg2;
            }

            return result;
        }
        public static int[] ColonOp(int arg1, int arg2, int arg3)
        {
            int length = (int)((arg3 - arg1) / arg2);
            length = length < 0 ? 0 : length + 1;
            var result = new int[length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arg1 + i * arg2;
            }

            return result;
        }

        public static List<double> Diff(IList<double> arr)
        {
            List<double> result = new List<double>();
            for (int i = 1; i < arr.Count; i++)
            {
                result.Add(arr[i] - arr[i - 1]);
            }

            return result;
        }

        public static double Fix(double val)
        {
            return val > 0 ? val.Floor() : val.Ceiling();
        }

        public static double[] Tabulate(Func<double, double> func, double xFrom, double xTo, int numOfPoints)
        {
            double[] result = new double[numOfPoints];
            double step = (xTo - xFrom) / (numOfPoints - 1);
            double x = xFrom;
            for (int i = 0; i < numOfPoints; i++)
            {
                result[i] = func(x);
                x += step;
            }
            return result;
        }
    }
}
