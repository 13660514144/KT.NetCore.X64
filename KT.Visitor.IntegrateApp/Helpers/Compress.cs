
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;

namespace HelperTools

{
    public class CompressByte
    {
        /*压缩解压*/
        public static string CompressString(string str)
        {
            if (string.IsNullOrEmpty(str) == false)
            {
                var compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);
                var compressAfterByte = Compress(compressBeforeByte);
                string compressString = Convert.ToBase64String(compressAfterByte);
                string s1 = compressString.Replace("+", "-");
                string s2 = s1.Replace("/", "*");
                return s2;
            }
            else
            {
                return "";
            }
        }

        public static string DecompressString(string str)
        {
            if (string.IsNullOrEmpty(str) == false)
            {
                string s1 = str.Replace("-", "+");
                string s2 = s1.Replace("*", "/");
                var compressBeforeByte = Convert.FromBase64String(s2);
                var compressAfterByte = Decompress(compressBeforeByte);
                string compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);
                return compressString;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Compress
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Compress(byte[] data)
        {
            var ms = new MemoryStream();
            var zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(data, 0, data.Length);
            zip.Close();
            var buffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, buffer.Length);
            ms.Close();
            return buffer;
        }

        /// <summary>
        /// Decompress
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Decompress(byte[] data)
        {
            var ms = new MemoryStream(data);
            var zip = new GZipStream(ms, CompressionMode.Decompress, true);
            var msreader = new MemoryStream();
            var buffer = new byte[0x1000];
            while (true)
            {
                var reader = zip.Read(buffer, 0, buffer.Length);
                if (reader <= 0)
                {
                    break;
                }
                msreader.Write(buffer, 0, reader);
            }
            zip.Close();
            ms.Close();
            msreader.Position = 0;
            buffer = msreader.ToArray();
            msreader.Close();
            return buffer;

        }

        /// <summary>
        /// ZipArchive压缩
        /// </summary>
        /// <param name="str_ZipFilePath">压缩文件存放的路径（如：/UploadFiles/quber.zip）</param>
        /// <param name="str_OldFilePath">压缩文件的上级文件夹名称路径（如：/UploadFiles/）</param>
        /// <param name="bl_IsDeleteZip">是否首先删除已经存在的zip压缩包</param>
        public static bool ZAZIP(string str_ZipFilePath, string str_OldFilePath, bool bl_IsDeleteZip)
        {
            bool bl_Result = false;

            //删除已经存在的压缩包
            if (System.IO.File.Exists((str_ZipFilePath)))
            {
                System.IO.File.Delete((str_ZipFilePath));
            }

            string[] str_AllFileName = GetAllFileName(str_OldFilePath);
            using (var zip = ZipFile.Open((str_ZipFilePath), ZipArchiveMode.Create))
            {
                if (str_AllFileName.Length > 0)
                {
                    for (int i = 0; i < str_AllFileName.Length; i++)
                    {

                        zip.CreateEntryFromFile((str_OldFilePath) + str_AllFileName[i], str_AllFileName[i]);
                        bl_Result = true;
                    }
                }
            }
            return bl_Result;
        }

        /// <summary>
        /// 获取文件夹下的所有文件名称
        /// </summary>
        /// <param name="str_FilePath">文件夹路径名称（如：/UploadFiles/）</param>
        /// <returns></returns>
        public static string[] GetAllFileName(string str_FilePath)
        {
            string[] str_AllFileName = { };

            DirectoryInfo di = new DirectoryInfo((str_FilePath));
            FileSystemInfo[] dis = di.GetFileSystemInfos();
            if (dis.Length > 0)
            {
                str_AllFileName = new string[dis.Length];
                int i_Count = 0;
                foreach (FileSystemInfo fitemp in dis)
                {
                    str_AllFileName[i_Count] = fitemp.Name;
                    i_Count++;
                }
            }
            return str_AllFileName;

        }

        /*压缩解压*/

        /*MD5*/
        #region  MD5加密
        public static string MD5_16D(string ConvertString) //16位大写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string MD5_16X(string ConvertString) //16位小写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
        public static string MD5_32D(string ConvertString) //32位大写
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2;
        }
        public static string MD5_32X(string ConvertString) //32位小写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2.ToLower();
        }
        #endregion

        #region BASE64编码解码
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);

            encode = Convert.ToBase64String(bytes);

            string s1 = encode.Replace("+", "-");
            string s2 = s1.Replace("/", "*");
            return s2;
        }
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            string s1 = code.Replace("-", "+");
            string s2 = s1.Replace("*", "/");
            byte[] bytes = Convert.FromBase64String(s2);

            decode = Encoding.GetEncoding(code_type).GetString(bytes);

            return decode;
        }
        /// <summary>
        ///返回GUID
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            string Guid = System.Guid.NewGuid().ToString();
            string Newguid = Guid.Replace("-", "").ToUpper();
            return Newguid;
        }
        /// <summary>
        /// 1-10000随机数
        /// </summary>
        /// <returns></returns>
        public static string GetRand()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            int rndNum = rnd.Next(1, 1000);
            string s = rndNum.ToString().PadLeft(4, '0');
            return s;
        }
        /// <summary>
        /// 自编码算法器 按年月是时分秒+随机数
        /// </summary>
        /// <returns></returns>
        public static string GetCustomCode(bool Flg = false, string Title = "")
        {
            string Code = string.Empty;
            string s = DateTime.Now.ToString("yyMMddHHmmssffff");
            if (!Flg)
            {
                Code = $"{s}{GetRand()}";
            }
            else
            {
                Code = $"{Title}-{s}";
            }
            return Code;
        }
        /// <summary>
        /// 预览图片附件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string GetImgBase64(string FilePath)
        {
            string Result = string.Empty;

            System.Drawing.Image _Image = System.Drawing.Image.FromFile(FilePath);
            if (!string.IsNullOrEmpty(FilePath))
            {
                System.IO.MemoryStream _ImageMem = new System.IO.MemoryStream();
                _Image.Save(_ImageMem, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] _ImageBytes = _ImageMem.ToArray();
                Result = Convert.ToBase64String(_ImageBytes);
                _Image.Clone();
                _Image.Dispose();
            }
            return Result;
        }
        #endregion
    }
}