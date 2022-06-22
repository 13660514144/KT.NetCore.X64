using System;
using System.Security.Cryptography;
using System.Text;

namespace KT.Common.Core.Utils
{
    public static class AesUtil
    {
        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AesEncryptHex(this string source, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(source);

            return AesEncryptHex(toEncryptArray, keyArray);
        }

        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AesEncryptHex(this byte[] toEncryptArray, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            return toEncryptArray.AesEncryptHex(keyArray);
        }

        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string AesEncryptHex(this byte[] toEncryptArray, byte[] keyArray)
        {
            return AesEncrypt(toEncryptArray, keyArray).BytesToHex();
        }


        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static byte[] AesEncrypt(this string source, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(source);

            return AesEncrypt(toEncryptArray, keyArray);
        }

        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static byte[] AesEncrypt(this byte[] toEncryptArray, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            return toEncryptArray.AesEncrypt(keyArray);
        }

        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static byte[] AesEncrypt(this byte[] toEncryptArray, byte[] keyArray)
        {
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return resultArray;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AesDecryptHex(this string source, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = source.HexToBytes();

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// 16进制字符串转字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string hex)
        {
            byte[] result = new byte[hex.Length / 2];
            for (int i = 0; i < result.Length; i++)
                result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return result;
        }

        /**//// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHex(this byte[] bytes)
        {
            StringBuilder result = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    result.Append(bytes[i].ToString("X2"));
                }
            }
            return result.ToString();
        }
    }
}
