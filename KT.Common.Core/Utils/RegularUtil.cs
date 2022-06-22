using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KT.Common.Core.Utils
{
    public static class RegularUtil
    {
        public static bool MatchRegular(  string source, string regular)
        {
            Regex reg = new Regex(regular);
            //例如我想提取line中的NAME值
            Match match = reg.Match(source);

            return match.Length > 0;
        }
    }
}
