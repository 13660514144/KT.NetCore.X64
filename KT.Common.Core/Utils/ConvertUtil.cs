using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Core.Utils
{
    public class ConvertUtil
    {
        public static bool ToBool(object obj, bool defaultValue)
        {
            bool? result = ToBool(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        public static bool? ToBool(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }

            //1,0 TryParse无法转换
            var sourceText = obj.ToString().Trim();
            if (sourceText == 1.ToString())
            {
                return true;
            }
            else if (sourceText == 0.ToString())
            {
                return false;
            }

            bool result;
            if (!bool.TryParse(sourceText, out result))
            {
                return null;
            }
            return result;
        }
        public static long ToLong(object obj, long defaultValue)
        {
            long? result = ToLong(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        public static long? ToLong(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }
            long result;
            if (!long.TryParse(obj.ToString().Trim(), out result))
            {
                return null;
            }
            return result;
        }

        public static string ToStringTrim(object obj)
        {
            return ToString(obj).Trim();
        }

        public static decimal? ToDecimal(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }
            decimal result;
            if (decimal.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            return null;
        }
        public static double ToDouble(object obj, double defaultValue)
        {
            var result = ToDouble(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        public static double? ToDouble(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }
            double result;
            if (double.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            return null;
        }
        public static Int32 ToInt32(object obj, Int32 defaultValue)
        {
            var result = ToInt32(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        public static Int32 ToInt32(object obj, Func<int> defaultValueAction)
        {
            var result = ToInt32(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValueAction.Invoke();
            }
        }
        public static ushort ToUInt16(object obj, ushort defaultValue)
        {
            var result = ToUInt16(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        public static uint ToUInt32(object obj, ushort defaultValue)
        {
            var result = ToUInt32(obj);
            if (result.HasValue)
            {
                return result.Value;
            }
            else
            {
                return defaultValue;
            }
        }
        public static Int32? ToInt32(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }
            Int32 result;
            if (Int32.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            return null;
        }
        public static uint? ToUInt32(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }
            uint result;
            if (uint.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            return null;
        }

        public static ushort? ToUInt16(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return null;
            }
            ushort result;
            if (ushort.TryParse(obj.ToString().Trim(), out result))
            {
                return result;
            }
            return null;
        }

        public static string ToString(object obj)
        {
            if (obj == null || obj.Equals(DBNull.Value))
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}
