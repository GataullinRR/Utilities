using System;
using System.Collections.Generic;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions.Debugging
{
    public static class Dumping
    {
        public static void Dump(this object obj, string name)
        {
            Console.WriteLine($"{name}: {obj.ToString()}");
        }
    }
}
