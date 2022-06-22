using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace KT.Common.Core.Utils
{
    public static class StringUtil
    {
        public static string UrlEncode(string source)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(source);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }

        /// <summary>
        /// 向字符串两端增加括号
        /// 空字符不加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecorateBrackets(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return "(" + value.ToString() + ")";
        }

        /// <summary>
        /// 向字符串两端增加单引号
        /// 空字符不加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecorateSingleQuotes(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return "'" + value + "'";
        }

        /// <summary>
        /// 向字符串右边增加逗号
        /// 空字符不加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RightDecorateComma(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value + ",";
        }

        /// <summary>
        /// 向字符串右边增加逗号
        /// 空字符不加
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static string RightDecorates(this List<string> sources, string comma)
        {
            if (sources == null)
            {
                return string.Empty;
            }

            var result = string.Empty;
            foreach (var item in sources)
            {
                result += item + comma;
            }

            return result.TrimEndString(comma);
        }

        /// <summary>
        /// 向字符串两端增加空格
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecorateBlank(this string value)
        {
            if (value == null)
            {
                return " ";
            }
            return " " + value + " ";
        }

        /// <summary>
        /// 向字符串两端增加空格
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string LeftBlank(this string value)
        {
            if (value == null)
            {
                return " ";
            }
            return " " + value;
        }

        /// <summary>
        /// 向字符串两端增加空格
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RightBlank(this string value)
        {
            if (value == null)
            {
                return " ";
            }
            return value + " ";
        }

        /// <summary>
        /// 去除字符串右边增加逗号
        /// 空字符不加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimEndComma(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return value.TrimEnd(',');
        }

        /// <summary>
        /// 从当前字符串对象移除指定的字符的所有尾部匹配项
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Trim(this string value, string trimStr)
        {
            value = value.TrimEndString(trimStr);
            value = value.TrimStartString(trimStr);
            return value;
        }
        /// <summary>
        /// 从当前字符串对象移除指定的字符的所有尾部匹配项
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimEndString(this string value, string trimStr)
        {
            if (value == null)
            {
                return string.Empty;
            }
            //是否尾部匹配
            bool isEndsWith = value.EndsWith(trimStr);
            //去掉尾部一个字符串
            if (isEndsWith)
            {
                if (value.Length > trimStr.Length)
                {
                    return value.Substring(0, value.Length - trimStr.Length);
                }
                else
                {
                    return string.Empty;
                }
            }
            return value;
        }
        /// <summary>
        /// 从当前字符串对象移除指定的字符的所有尾部匹配项
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimStartString(this string value, string trimStr)
        {
            if (value == null)
            {
                return string.Empty;
            }
            //是否尾部匹配
            bool isStartsWith = value.StartsWith(trimStr);
            //去掉尾部一个字符串
            if (isStartsWith && value.Length > trimStr.Length)
            {
                return value.Substring(trimStr.Length);
            }
            return value;
        }


        /// <summary>
        /// 列表转换成用符号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ToSeparateString(this List<string> value, string separator)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            foreach (var item in value)
            {
                result += item + separator;
            }
            return result.TrimEndString(separator);
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaString(this List<string> value)
        {
            return ToSeparateString(value, ",");
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaString(this Dictionary<string, string> value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            foreach (var item in value)
            {
                result += $"{item.Key}:{item.Value},";
            }
            return result.TrimEndString(",");
        }

        /// <summary>
        /// 列表转换成用符号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ToSeparateString(this List<int> value, string separator)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            foreach (var item in value)
            {
                result += item + separator;
            }
            return result.TrimEndString(separator);
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaString(this List<int> value)
        {
            return ToSeparateString(value, ",");
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaString(this byte[] value)
        {
            return ToSeparateString(value.Select(x => x.ToString()).ToList(), ",");
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaPrintString(this byte[] value)
        {
            return ToCommaPrintString(value?.ToList());
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaPrintString(this List<byte> value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            foreach (var item in value)
            {
                result += item + ",";
            }
            result = result.TrimEndString(",");
            result += ":";
            result += BitConverter.ToString(value.ToArray()).Replace("-", " ");

            return result;
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToCommaPrintString(this List<string> value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            foreach (var item in value)
            {
                result += item + ",";
            }
            result = result.TrimEndString(",");
            return result;
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToBitsString(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            for (int i = 0; i < (value.Length / 8); i++)
            {
                result += value.Substring(i * 8, 8) + " ";
            }

            //剩余字符
            if ((value.Length % 8) > 0)
            {
                result += value.Substring((value.Length / 8) * 8);
            }
            return result;
        }

        /// <summary>
        /// 列表转换成用逗号分隔的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToIPEndPointString(this List<IPEndPoint> value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = string.Empty;
            foreach (var item in value)
            {
                result += item.Address + ":" + item.Port + ",";
            }
            return result.TrimEndString(",");
        }

        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RightWrap(this string source)
        {
            return source + Environment.NewLine;
        }

        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string LeftWrap(this string source)
        {
            return Environment.NewLine + source;
        }

        /// <summary>
        /// 获取16进制字符串
        /// </summary>
        /// <param name="bytes">源数据</param>
        /// <returns>16进制字符串结果</returns>
        public static string Get16String(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }
            var stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                stringBuilder.Append(b.ToString("X2"));
            }
            var result = stringBuilder.ToString();
            return result;
        }

        /// <summary>
        /// 实现字符串反转
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Reverse(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = value.Length - 1; i >= 0; i--)
            {
                sb.Append(value[i]);
            }
            return sb.ToString();
        }
    }
}
