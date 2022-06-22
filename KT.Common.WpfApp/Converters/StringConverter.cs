using KT.Common.Core.Utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KT.Common.WpfApp.Converters
{
    /// <summary>
    /// 字符拼接
    /// </summary>
    public class StringAppendConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return string.Empty;
            }
            return value.IsNull() + parameter.IsNull();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    /// <summary>
    /// 字符拼接
    /// </summary>
    public class StringInsertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return string.Empty;
            }
            return parameter.IsNull() + value.IsNull();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
