using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Core.Utils
{
    public static class IdUtil
    {
        public static string NewId()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static string EmptyId()
        {
            return Guid.Empty.ToString("N");
        }
        public static bool IsIdNull(this string source)
        {
            if (string.IsNullOrEmpty(source)
                 || source == "0"
                 || source == EmptyId())
            {
                return true;
            }
            return false;
        }
    }
}
