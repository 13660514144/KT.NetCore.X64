using KT.Common.Core.Utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KT.Common.WpfApp.Converters
{
    /// <summary>
    /// 字符串为空返回True
    /// </summary>
    public class StringEmptyTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                return value.ToString() == string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
    /// <summary>
    /// 字符串为空返回False
    /// </summary>
    public class StringEmptyFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                return value.ToString() != string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 返回True False 反义结果
    /// </summary>
    public class BoolAntonymConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = ConvertUtil.ToBool(value, false);
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !ConvertUtil.ToBool(value, true);
        }
    }

    /// <summary>
    /// 返回True False 反义结果
    /// </summary>
    public class NullableBoolAntonymConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = bool.TryParse(value?.ToString(), out bool val);
            if (result)
            {
                return !val;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = bool.TryParse(value?.ToString(), out bool val);
            if (result)
            {
                return !val;
            }
            else
            {
                return null;
            }
        }
    }
}