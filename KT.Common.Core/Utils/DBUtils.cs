using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Core.Utils
{
    public static class DBUtils
    {
        public static string IsNull(this object value)
        {
            if (value == null || value.Equals(DBNull.Value))
            {
                return string.Empty;
            }
            return value.ToString();
        }

        /// <summary>
        /// 当list<int>等情况list.count为0时，使用fistordefault失效
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsListNull<T>(this IEnumerable<T> value)
        {
            if (value == null || value.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public static string IsNull(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value.ToString();
        }

        public static int IsNull(this int value)
        {
            if (value < 0)
            {
                return 0;
            }
            return value;
        }

        /// <summary>
        /// 小于等于0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IfNullOrLessEqualZero(this int? value)
        {
            if (!value.HasValue)
            {
                return true;
            }
            if (value.Value <= 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 大于0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IfNotNullAndGreaterZero(this int? value)
        {
            if (!value.HasValue)
            {
                return false;
            }
            if (value.Value <= 0)
            {
                return false;
            }
            return true;
        }

        public static string IsNullTrim(this object value)
        {
            if (value == null || value.Equals(DBNull.Value))
            {
                return string.Empty;
            }
            return value.ToString().Trim();
        }
    }
}
