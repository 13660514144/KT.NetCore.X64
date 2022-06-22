using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone
{
    using Newtonsoft.Json;
    using System.Xml;

    public static partial class Convertion
    {

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="value">对象</param>
        /// <returns></returns>
        public static string toJson<T>(this T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T fromJson<T>(this string value) where T : class, new()
        {
            return JsonConvert.DeserializeObject(value, typeof(T)) as T;
        }


        private static Encoding _encoding = Encoding.ASCII;
        public static string GetString(this byte[] buffer)
        {
            var text = _encoding.GetString(buffer);
            return text;
        }


        public static string GetString(this byte[] buffer,int index,int len)
        {
            var value = new byte[len];
            Array.Copy(buffer, index, value, 0, len);


            //buffer.Copyto(value, index, len);
            var text = _encoding.GetString(value);
            return text;
        }

        /// <summary>
        /// 转为1个字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte toBtye(this int value)
        {
            var temp = (byte)value;
            return temp;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="from">数据源</param>
        /// <param name="buffer">接收数据源</param>
        /// <param name="index">粘贴指定索引</param>
        public static void Copyto(this byte[] fromBuffer, byte[] toBuffer, int index)
        {
            var len = fromBuffer.Length;
            Copyto(fromBuffer, toBuffer, index, len);
        }

        public static void Copyto(this byte[] fromBuffer, byte[] toBuffer, int index,int len)
        {
            Array.Copy(fromBuffer, 0, toBuffer, index, len);
        }

        /// <summary>
        /// 转换16Bit的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHexString(this int value)
        {
            var text = Convert.ToString(value, 16).ToUpper(); 
            return text;
        }

        /// <summary>
        /// 转换16Bit的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHexString(this long value)
        {
            var text = Convert.ToString(value, 16).ToUpper();
            return text;
        }

        /// <summary>
        /// 转换16Bit的字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetHexString(this byte[] buffer)
        {
            var value = BitConverter.ToString(buffer);
            var text = value.Replace("-", "");
            return text;
        }

        /// <summary>
        /// 转换16Bit的字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index">起始位</param>
        /// <param name="len">字节</param>
        /// <returns></returns>
        public static string GetHexString(this byte[] buffer,int index,int len)
        {
            var value = new byte[len];
            Array.Copy(buffer, index, value, 0, len);
            return GetHexString(value);
        }


        /// <summary>
        /// 转换16Bit的数字
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static long toHexInt(this string hex)
        {
            var value = Convert.ToInt64(hex, 16);
            return value;
        }

        /// <summary>
        /// 转换16Bit的数字
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static long toHexInt(this byte[] buffer)
        {
            var hex = GetHexString(buffer);
            return toHexInt(hex);
        }

        /// <summary>
        /// 转换16Bit的字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index">起始位</param>
        /// <param name="len">字节</param>
        /// <returns></returns>
        public static long toHexInt(this byte[]  buffer,int index,int len)
        {
            var hex = GetHexString(buffer, index, len);
            return toHexInt(hex);
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] toHexBytes(this string hex)
        {
            var list = new List<byte>();
            for (int i = 0; i < hex.Length; i+=2)
            {
                var temp = hex.Substring(i, 2);
                var value = byte.Parse(temp, NumberStyles.HexNumber);
                list.Add(value);
            }
            return list.ToArray();
        }


        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static byte[] toHexBtyes(this string hex, int bit)
        {
            var length = bit * 2;
            var value = hex.PadLeft(length, '0');
            return toHexBytes(value);
        }

        /// <summary>
        /// 转换Hex.Int
        /// </summary>
        /// <param name="value">数字</param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static byte[] toHexBtyes(this int value, int bit)
        {
            var hex = GetHexString(value);
            return toHexBtyes(hex, bit);
        }

        public static byte[] toHexBtyes(this long value, int bit)
        {
            var hex = GetHexString(value);
            return toHexBtyes(hex, bit);
        }

        /// <summary>
        /// 转换Hex.DateTime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static byte[] GetHexBytes(this DateTime date, int bit)
        {
            var length = bit * 2;
            var buffer = new byte[bit];

            var value = DateTime.Now.ToUniversalTime().Ticks;
            var result = (value - 0x89f7ff5f7b58000L) / 0x2710L;

            var num1 = 0x19db1ded53e8000L;
            var num2 = Convert.ToInt64(Convert.ToString(result).PadRight(0x11, '0'));

            var vaue = num1 + num2;

            return toHexBtyes(value, bit);
        }


        public static bool TryXmlNodeText(this XmlElement element, string name, out int result)
        {
            var IsExists = TryXmlNodeText(element, name, out string value);
            int.TryParse(value, out result);
            return IsExists;
        }

        public static bool TryXmlNodeText(this XmlElement element, string name, out string text)
        {
            text = null;
            Boolean IsExists = false;
            var list = element.ChildNodes;
            foreach (XmlNode node in list)
            {
                if (node.Name == name)
                {
                    text = node.InnerText;
                    IsExists = true;
                    break;
                }
            }
            return IsExists;
        }
    }
}
