using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace KT.Common.Core.Utils
{
    public class DesUtil
    {
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="source">待加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static byte[] Encrypt3Des(byte[] source, string key)
        {
            var keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //加密参数
            var provider = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //加密
            //var decryptor = provider.CreateDecryptor(provider.Key, provider.IV);
            var encryptor = provider.CreateEncryptor();
            //var bytes = encryptor.TransformFinalBlock(source, 0, source.Length);
            //return bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(source, 0, source.Length);
                    cs.FlushFinalBlock();
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="source">加密的字符串</param>
        /// <param name="key">密钥</param> 
        /// <returns>解密的字符串</returns>
        public static byte[] Decrypt3Des(byte[] source, string key)
        {
            var keyArray = Encoding.UTF8.GetBytes(key);
            var provider = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var decryptor = provider.CreateDecryptor();
            //var bytes = decryptor.TransformFinalBlock(source, 0, source.Length);
            //return bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(source, 0, source.Length);
                    cs.FlushFinalBlock();
                }
                return ms.ToArray();
            }
        }

        ///// <summary>
        ///// 加密数据
        ///// </summary>
        ///// <param name="Text"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static string DesEncrypt(string Text, string key)
        //{
        //    var inputByteArray = UTF8Encoding.UTF8.GetBytes(Text);
        //    return DesEncrypt(inputByteArray, key);

        //    //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    //byte[] inputByteArray;
        //    ////des.Key = ASCIIEncoding.ASCII.GetBytes(key);
        //    ////des.IV = ASCIIEncoding.ASCII.GetBytes(key);  
        //    //des.Key = UTF8Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //    //des.IV = UTF8Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //    //using (MemoryStream ms = new MemoryStream())
        //    //{
        //    //    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        //    //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    //    cs.FlushFinalBlock();

        //    //    return UTF8Encoding.UTF8.GetString(ms.ToArray());
        //    //}

        //    ////StringBuilder ret = new StringBuilder();
        //    ////foreach (byte b in ms.ToArray())
        //    ////{
        //    ////    ret.AppendFormat("{0:X2}", b);
        //    ////}
        //    ////return ret.ToString();
        //}

        ///// <summary>
        ///// 加密数据
        ///// </summary>
        ///// <param name="Text"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static string DesEncrypt(byte[] inputByteArray, string key)
        //{
        //    var keyArray = UTF8Encoding.UTF8.GetBytes(key);

        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    des.Key = keyArray;
        //    des.IV = keyArray;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();

        //        return UTF8Encoding.UTF8.GetString(ms.ToArray());
        //    }
        //}

        ///// <summary>
        ///// 解密数据
        ///// </summary>
        ///// <param name="Text"></param>
        ///// <param name="sKey"></param>
        ///// <returns></returns>
        //public static string DesDecrypt(string Text, string key)
        //{
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    int len;
        //    len = Text.Length / 2;
        //    byte[] inputByteArray = new byte[len];
        //    int x, i;
        //    for (x = 0; x < len; x++)
        //    {
        //        i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
        //        inputByteArray[x] = (byte)i;
        //    }
        //    //des.Key = ASCIIEncoding.ASCII.GetBytes(key);
        //    //des.IV = ASCIIEncoding.ASCII.GetBytes(key);
        //    des.Key = UTF8Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //    des.IV = UTF8Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    return Encoding.Default.GetString(ms.ToArray());
        //}

        ///// <summary>
        ///// DES加密字符串
        ///// </summary>
        ///// <param name="source">待加密的字符串</param>
        ///// <param name="key">加密密钥,要求为8位</param>
        ///// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        //public static string DesEncrypt(string source, string key)
        //{
        //    StringBuilder ret = new StringBuilder();
        //    try
        //    {
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        byte[] inputByteArray = UTF8Encoding.UTF8.GetBytes(source);
        //        des.Key = UTF8Encoding.UTF8.GetBytes(key);
        //        des.IV = UTF8Encoding.UTF8.GetBytes(key);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        //            cs.Write(inputByteArray, 0, inputByteArray.Length);
        //            cs.FlushFinalBlock();
        //            //foreach (byte b in ms.ToArray())
        //            //{
        //            //    ret.AppendFormat("{0:X2}", b);
        //            //}
        //            //return ret.ToString();
        //            return UTF8Encoding.UTF8.GetString(ms.ToArray());
        //        }
        //    }
        //    catch
        //    {
        //        return source;
        //    }
        //}

        ///// <summary>
        ///// DES解密字符串
        ///// </summary>
        ///// <param name="source">待解密的字符串</param>
        ///// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
        ///// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        //public static string DesDecrypt(string source, string key)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    try
        //    {
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        byte[] inputByteArray = new byte[source.Length / 2];
        //        for (int x = 0; x < source.Length / 2; x++)
        //        {
        //            int i = (Convert.ToInt32(source.Substring(x * 2, 2), 16));
        //            inputByteArray[x] = (byte)i;
        //        }
        //        des.Key = UTF8Encoding.UTF8.GetBytes(key);
        //        des.IV = UTF8Encoding.UTF8.GetBytes(key);
        //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();
        //        return UTF8Encoding.UTF8.GetString(ms.ToArray());
        //    }
        //    catch
        //    {
        //        return source;
        //    }
        //}
    }
}
