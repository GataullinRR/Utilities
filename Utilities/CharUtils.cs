using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities
{
    public static class CharUtils
    {
        public static byte ParseHex(char hexChar)
        {
            hexChar = char.ToUpper(hexChar);

            int result = (int)hexChar < (int)'A' ?
                ((int)hexChar - (int)'0') :
                10 + ((int)hexChar - (int)'A');
            return result.ToByte();
        }
    }
}
