using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace Utilities
{
    public static class Global
    {
        public static readonly string NL = Environment.NewLine;

        public static readonly Random Random = new Random();

        //public static void ConsoleWaitForExit()
        //{
        //    Console.WriteLine("Press any key to exit");
        //    Console.ReadKey();
        //}

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();

        //public static void OpenConsole()
        //{
        //    AllocConsole();
        //}
    }
}
