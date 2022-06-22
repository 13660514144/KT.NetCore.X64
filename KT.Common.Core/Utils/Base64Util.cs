using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Core.Utils
{
    public static class Base64Util
    {
        /// <summary>
        /// 将普通文本转换成Base64编码的文本
        /// </summary>
        /// <param name="base64">Base64编码的文本</param>
        /// <returns></returns>
        public static string ToBase64String(this string source)
        {
            if (source != "")
            {
                byte[] bytes = UTF8Encoding.UTF8.GetBytes(source);
                string str = Convert.ToBase64String(bytes);
                return str;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 将Base64编码的文本转换成普通文本
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ToBase64Source(this string base64)
        {
            byte[] bpath = Convert.FromBase64String(base64);
            return UTF8Encoding.UTF8.GetString(bpath);
        }
    }
}
