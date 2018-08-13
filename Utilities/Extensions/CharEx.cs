using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace Utilities.Extensions
{
    public static class CharEx
    {
        public static int AsInt(this char c)
        {
            return Convert.ToInt32(c);
        }
    }
}
