using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;

namespace Utilities.Extensions
{
    public static class MiscEx
    {
        public static async Task<Match> MatchAsync(this Regex regex, string str)
        {
            return await Task.Run(() => regex.Match(str));
        }
    }
}
