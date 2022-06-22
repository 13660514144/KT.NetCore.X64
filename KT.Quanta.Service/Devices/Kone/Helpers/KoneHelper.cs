using KT.Quanta.Model.Kone;
using System;

namespace KT.Quanta.Service.Devices.Kone
{
    public class KoneHelper
    {
        /// <summary>
        /// 获取本地文件时间
        /// </summary>
        /// <returns>文件时间戳</returns>
        public static ulong GetLocalFileTime()
        {
            var value = DateTime.Now.ToUniversalTime().Ticks;
            var result = (value - 0x89f7ff5f7b58000L) / 0x2710L;

            var num1 = 0x19db1ded53e8000L;
            var num2 = Convert.ToInt64(Convert.ToString(result).PadRight(0x11, '0'));

            return (ulong)(num1 + num2);
        }

        public static KoneSystemConfigModel SystemConfig { get; set; } = new KoneSystemConfigModel();
    }
}
