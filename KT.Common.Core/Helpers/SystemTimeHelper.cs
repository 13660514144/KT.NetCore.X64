using KT.Common.Core.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Core.Helpers
{
    /// <summary>
    /// 同步服务时间
    /// </summary>
    public class SystemTimeHelper
    {
        /// <summary>
        /// 获取本地时间
        /// </summary>
        /// <param name="dateTime">当前时区时间</param>
        [DllImport("Kernel32.dll")]
        private static extern void GetLocalTime(ref DateTimeStruct dateTime);

        /// <summary>
        /// 设置本地时间
        /// </summary>
        /// <param name="dateTime">当前时区时间</param>
        [DllImport("Kernel32.dll")]
        private static extern bool SetLocalTime(ref DateTimeStruct dateTime);

        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <param name="dateTime">UTC时间</param>
        [DllImport("Kernel32.dll")]
        private static extern void GetSystemTime(ref DateTimeStruct dateTime);

        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="dateTime">UTC时间</param>
        [DllImport("Kernel32.dll")]
        private static extern bool SetSystemTime(ref DateTimeStruct dateTime);

        [StructLayout(LayoutKind.Sequential)]
        private struct DateTimeStruct
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="utcMillisAction">需要设置的时间获取委托方法，需要统计时间，获取超时不再执行修改操作</param>
        /// <returns>返回系统时间设置状态，true为成功，false为失败</returns>
        public static async Task<bool> SetSystemTimeAsync(Func<Task<long?>> utcMillisAction)
        {
            try
            {
                if (utcMillisAction == null)
                {
                    Console.WriteLine($"修改系统时间获取时间委托为空！");
                    return false;
                }

                var stopwatch = new Stopwatch();
                stopwatch.Restart();

                var utcMilis = await utcMillisAction.Invoke();
                if (!utcMilis.HasValue)
                {
                    Console.WriteLine($"修改系统时间获取的时间为空！");
                    return false;
                }

                var utcMilisValue = utcMilis.Value;
                var utcNowMillis = DateTimeUtil.UtcNowMillis();
                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > (10 * 1000))
                {
                    Console.WriteLine($"修改系统时间耗时过长：{stopwatch.ElapsedMilliseconds}");
                    return false;
                }

                var timeSpan = Math.Abs((utcNowMillis - utcMilisValue));
                if (timeSpan < (20 * 1000))
                {
                    Console.WriteLine($"修改系统时间与本地时间相同不修改：{timeSpan}");
                    return false;
                }

                return SetSystemTime(utcMilisValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"修改系统时间失败：{ex}");
                return false;
            }
        }

        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="dt">需要设置的时间</param>
        /// <returns>返回系统时间设置状态，true为成功，false为失败</returns>
        private static bool SetSystemTime(long utcMillis)
        {
            var utcDateTime = DateTimeUtil.ToDateTimeByMillis(utcMillis);
            var dataTimeStruct = GetDateTimeStruct(utcDateTime);
            bool rt = SetSystemTime(ref dataTimeStruct);
            return rt;
        }

        /// <summary>
        /// 获取时间结构
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>时间结构数据</returns>
        private static DateTimeStruct GetDateTimeStruct(DateTime dateTime)
        {
            DateTimeStruct dateTimeStruct;

            dateTimeStruct.year = (short)dateTime.Year;
            dateTimeStruct.month = (short)dateTime.Month;
            dateTimeStruct.dayOfWeek = (short)dateTime.DayOfWeek;
            dateTimeStruct.day = (short)dateTime.Day;
            dateTimeStruct.hour = (short)dateTime.Hour;
            dateTimeStruct.minute = (short)dateTime.Minute;
            dateTimeStruct.second = (short)dateTime.Second;
            dateTimeStruct.milliseconds = (short)dateTime.Millisecond;

            return dateTimeStruct;
        }
    }
}
