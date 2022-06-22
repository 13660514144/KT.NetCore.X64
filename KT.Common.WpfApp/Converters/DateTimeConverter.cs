using KT.Common.Core.Utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KT.Common.WpfApp.Converters
{
    /// <summary>
    /// 时间选择与输入转换
    /// </summary>
    public class DateTimeShowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = value?.ToString().ToDateTime();
            if (!dt.HasValue)
            {
                return value.IsNull();
            }
            if (parameter == null || string.IsNullOrEmpty(parameter.ToString()))
            {
                return value.ToString();
            }
            return dt.Value.ToString(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
