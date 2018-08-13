﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities.Extensions
{
    public static class ReflectionEx
    {
        public static bool IsStatic(this Type t)
        {
            return t.IsAbstract && t.IsSealed;
        }
    }
}
