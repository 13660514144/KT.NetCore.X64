using System;
using System.Globalization;
using System.Windows.Data;

namespace KT.Common.WpfApp.Converters
{

    [ValueConversion(typeof(double), typeof(double))]
    public class LengthPathHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double par = double.Parse(parameter.ToString().Trim());
            return 24 * par;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value;
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class LengthPathWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double par = double.Parse(parameter.ToString().Trim());
            return 24 * par;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value;
        }
    }
}
