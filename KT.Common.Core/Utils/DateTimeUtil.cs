using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace KT.Common.Core.Utils
{
    /// <summary>
    /// 时间格式转换
    /// 本地时间与UTC时间转换
    /// 本地时间类型：DateTime String(yyyy-MM-dd HH:mm:ss)
    /// UTC 时间类型：Long
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long UtcNowMillis()
        {
            return DateTime.UtcNow.ToMillis();
        }

        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ToMillis(this DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1);
            //精确到毫秒
            return (long)ts.TotalMilliseconds;
        }

        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ToMillis(this string dateTime)
        {
            return Convert.ToDateTime(dateTime).ToMillis();
        }

        /// <summary>
        /// 获取当前UTC时间值（秒）
        /// </summary>
        /// <returns></returns>
        public static long UtcNowSeconds()
        {
            return DateTime.UtcNow.ToSeconds();
        }

        /// <summary>
        /// 获取当前UTC时间值（秒）
        /// </summary>
        /// <returns></returns>
        public static long ToSeconds(this DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1);
            //精确到毫秒
            return (long)ts.TotalSeconds;
        }

        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ToSeconds(this string dateTime)
        {
            return Convert.ToDateTime(dateTime).ToSeconds();
        }

        /// <summary>
        /// 时间戳加天数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddDayMillis(this long source, int data)
        {
            return source + data.ToDayMillis();
        }

        /// <summary>
        /// 天数转毫秒
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToDayMillis(this int source)
        {
            var result = source * 24 * 60 * 60 * 1000L;
            return result;
        }

        /// <summary>
        /// 转换成开始时间（如 2020-01-12 00:00:00.000）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDayStart(this DateTime source)
        {
            string date = source.ToString("yyyy-MM-dd");
            string time = "00:00:00.000";
            return DateTime.Parse(date + " " + time);
        }

        /// <summary>
        /// 转换成结束时间（如 2020-01-12 23:59:59.999）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDayEnd(this DateTime source)
        {
            string date = source.ToString("yyyy-MM-dd");
            string time = "23:59:59.999";
            return DateTime.Parse(date + " " + time);
        }

        /// <summary>
        /// 转换成开始时间（如 2020-01-12 00:00:00.000）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime DayStart()
        {
            return DateTime.Now.ToDayStart();
        }

        /// <summary>
        /// 转换成结束时间（如 2020-01-12 23:59:59.999）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime DayEnd()
        {
            return DateTime.Now.ToDayEnd();
        }

        /// <summary>
        /// 转换成开始时间（如 2020-01-12 00:00:00.000）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DayStartMilliString()
        {
            return DayStart().ToMilliString();
        }

        /// <summary>
        /// 转换成结束时间（如 2020-01-12 23:59:59.999）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DayEndMilliString()
        {
            return DayEnd().ToMilliString();
        }

        /// <summary>
        /// 转换成时间字符串，精确到秒
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToMilliString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 转换成时间字符串，精确到秒
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSecondString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换成时间字符串，精确到分
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToMinuteString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 转换成时间字符串，精确到天
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDayString(this DateTime? source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            return source.Value.ToDayString();
        }

        /// <summary>
        /// 转换成时间字符串，精确到天
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToDayString(this DateTime source)
        {
            return source.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 转换成时间字符串，精确到秒
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NowSecondString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换成时间字符串，精确到分
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NowMinuteString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 转换成时间字符串，精确到天
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string NowDayString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static DateTime? ToZoneDateTime(this DateTime? source)
        {
            //DateTime 类型
            if (source == null)
            {
                return null;
            }
            return TimeZoneInfo.ConvertTimeFromUtc((DateTime)source, TimeZoneInfo.Local);
        }

        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static string ToZoneDateTimeString(this string source, string format)
        {
            var dateTime = ToDateTime(source);
            var zoneDateTime = ToZoneDateTime(dateTime);
            if (!zoneDateTime.HasValue)
            {
                return string.Empty;
            }
            return zoneDateTime.Value.ToString(format);
        }

        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ToUtcSeconds(this string dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(dateTime), TimeZoneInfo.Utc).ToSeconds();
        }

        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long ToUtcMillis(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Utc).ToMillis();
        }

        /// <summary>
        /// 获取当前UTC时间值(毫秒)
        /// </summary>
        /// <returns></returns>
        public static string ToUtcTimeString(this string dateTime, string format)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(dateTime), TimeZoneInfo.Utc);
            return utcTime.ToString(format);
        }
        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static DateTime? ToZoneDateTime(this long? source)
        {
            //DateTime 类型
            if (source == null)
            {
                return null;
            }

            return source.Value.ToZoneDateTimeByMillis();
        }

        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static DateTime ToZoneDateTimeByMillis(this long source, DateTime defalut)
        {
            var dateTime = ToZoneDateTimeByMillis(source);
            if (dateTime.HasValue)
            {
                return dateTime.Value;
            }
            else
            {
                return defalut;
            }
        }

        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static DateTime? ToZoneDateTimeByMillis(this long source)
        {
            //DateTime 类型
            if (source == 0)
            {
                return null;
            }

            //String 或 Long 类型
            DateTime? dt = source.ToDateTimeByMillis();
            if (dt.HasValue)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(dt.Value, TimeZoneInfo.Local);
            }
            return null;
        }

        /// <summary>
        /// 转换成当前时区时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToZoneDateTime(this DateTime? source, DateTime defaultValue)
        {
            DateTime? dt = source.ToZoneDateTime();
            if (dt.HasValue)
            {
                return dt.Value;
            }
            return defaultValue;
        }



        ///// <summary>
        ///// 转换成当前时区时间
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public static DateTime? ToDateTime(this object source)
        //{
        //    if (source == null)
        //    {
        //        return null;
        //    }
        //    DateTime? dt = null;
        //    if (source is long)
        //    {
        //        ((long)source).ToDateTime();
        //    }
        //    else
        //    {
        //        source.ToString().ToDateTime();
        //    }

        //    if (dt.HasValue)
        //    {
        //        return dt.Value;
        //    }
        //    return null;
        //}

        /// <summary>
        /// 转换成当前时区时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long? source, DateTime defaultValue)
        {
            DateTime? dt = source.ToDateTimeByMillis();
            if (dt.HasValue)
            {
                return dt.Value;
            }
            return defaultValue;
        }

        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static DateTime ToDateTimeByMillis(this long source)
        {
            var startTime = new System.DateTime(1970, 1, 1);

            //DateTime 类型
            if (source == 0)
            {
                return startTime;
            }

            // 当地时区
            DateTime dt = startTime.AddMilliseconds(source);
            return dt;
        }

        /// <summary>
        /// UTC转换成当前时区时间
        /// </summary>
        /// <returns></returns>
        public static DateTime? ToDateTimeByMillis(this long? source)
        {
            //DateTime 类型
            if (source == null)
            {
                return null;
            }

            return source.Value.ToDateTimeByMillis();
        }

        /// <summary>
        /// 转换成当前时区时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string source)
        {
            if (source == null || source.Equals(DBNull.Value))
            {
                return null;
            }
            //时间格式转换
            DateTime result;
            if (DateTime.TryParse(source.ToString().Trim(), out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 转换成当前时区时间
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source, DateTime defaultValue)
        {
            DateTime? dt = source.ToDateTime();
            if (dt.HasValue)
            {
                return dt.Value;
            }
            return defaultValue;
        }

    }
}
