using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KT.Common.Core.Utils
{
    public static class MD5Util
    {
        /// <summary>
        /// MD5加密
        /// 
        /// js 加密码方式 https://blueimp.github.io/JavaScript-MD5/
        /// </summary>
        /// <param name="base64Source"></param>
        /// <returns></returns>
        public static string Encrypt(this string source)
        {
            using (MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider())
            {
                byte[] bytes = CodePagesEncodingProvider.Instance.GetEncoding("gb2312").GetBytes(source);
                byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (var a in array)
                {
                    sb.AppendFormat("{0:x2}", a);
                }
                return sb.ToString();
            }
        }
    }
}
