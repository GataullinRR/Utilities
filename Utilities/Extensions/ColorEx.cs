using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class ColorEx
    {
        static readonly Random _rnd = Global.Random;

        public static Color Random(this Color color)
        {
            return Color.FromArgb(_rnd.NextByte(), _rnd.NextByte(), _rnd.NextByte());
        }
    }
}
