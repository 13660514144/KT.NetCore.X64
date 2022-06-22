using KT.Common.Core.Utils;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KT.Common.WpfApp.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class DoubleMultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double res = double.Parse(value.ToString().Trim());
            double par = double.Parse(parameter.ToString().Trim());
            return res * par;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value;
        }
    }
    /// <summary>
    /// 长度相加
    /// </summary>
    public class DoublePlusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertUtil.ToDouble(value, 0) + ConvertUtil.ToDouble(parameter, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertUtil.ToDouble(value, 0) - ConvertUtil.ToDouble(parameter, 0);
        }
    }
    /// <summary>
    /// 长度相减
    /// </summary>
    public class DoubleReduceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertUtil.ToDouble(value, 0) - ConvertUtil.ToDouble(parameter, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertUtil.ToDouble(value, 0) + ConvertUtil.ToDouble(parameter, 0);
        }
    }
}
