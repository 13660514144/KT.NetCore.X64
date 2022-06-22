using KT.Common.Core.Utils;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace KT.Common.WpfApp.Converters
{
    /// <summary>
    /// True显示
    /// </summary>
    public class TrueVisbileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = ConvertUtil.ToBool(value, false);
            if (b)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible == (Visibility)value;
        }
    }
    /// <summary>
    /// True隐藏
    /// </summary>
    public class TrueCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = ConvertUtil.ToBool(value, false);
            if (b)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed == (Visibility)value;
        }
    }

    /// <summary>
    /// 显示取反
    /// </summary>
    public class VisibleCollapsedInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (Visibility)value;
            if (b == Visibility.Visible)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (Visibility)value;
            if (b == Visibility.Collapsed)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
    }

    /// <summary>
    /// 显示取反
    /// </summary>
    public class VisibleHiddenInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (Visibility)value;
            if (b == Visibility.Visible)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (Visibility)value;
            if (b == Visibility.Hidden)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }
    }

    /// <summary>
    /// True隐藏
    /// </summary>
    public class TrueHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = ConvertUtil.ToBool(value, false);
            if (b)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Hidden == (Visibility)value;
        }
    }

    /// <summary>
    /// True隐藏
    /// </summary>
    public class FalseHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = ConvertUtil.ToBool(value, false);
            if (b)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible == (Visibility)value;
        }
    }

    /// <summary>
    /// 两个值相等显示
    /// </summary>
    public class EqualValuesVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || (values.Count() == 2 && ConvertUtil.ToStringTrim(values[0]).ToLower() == ConvertUtil.ToStringTrim(values[1]).ToLower()))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
    /// <summary>
    /// 两个值相等隐藏
    /// </summary>
    public class EqualValuesCollapsedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || (values.Count() == 2 && ConvertUtil.ToStringTrim(values[0]).ToLower() == ConvertUtil.ToStringTrim(values[1]).ToLower()))
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
    /// <summary>
    /// 数值相等显示 
    /// </summary>
    public class NumParVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? b = ConvertUtil.ToDecimal(value);
            decimal? c = ConvertUtil.ToDecimal(parameter);
            if (b == c)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    /// <summary>
    /// 数值相等隐藏
    /// </summary>
    public class NumParCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? b = ConvertUtil.ToDecimal(value);
            decimal? c = ConvertUtil.ToDecimal(parameter);
            if (b == c)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    /// <summary>
    /// 字符相等显示
    /// </summary>
    public class StringParVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string b = ConvertUtil.ToString(value);
            string c = ConvertUtil.ToString(parameter);
            if (b.Equals(c))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed;
        }
    }
    /// <summary>
    /// 字符值相等隐藏
    /// </summary>
    public class StringParCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string b = ConvertUtil.ToString(value);
            string c = ConvertUtil.ToString(parameter);
            if (b == c)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible;
        }
    }
    /// <summary>
    /// 字符是否相等
    /// </summary>
    public class StringParTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string b = ConvertUtil.ToString(value);
            string c = ConvertUtil.ToString(parameter);
            if (b.Equals(c))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
    /// <summary>
    /// 字符是否不相等
    /// </summary>
    public class StringParFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string b = ConvertUtil.ToString(value);
            string c = ConvertUtil.ToString(parameter);
            if (b.Equals(c))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
    /// <summary>
    /// 空字符串隐藏
    /// </summary>
    public class StringNullCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed;
        }
    }
    /// <summary>
    /// 空字符串显示
    /// </summary>
    public class StringNullVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible;
        }
    }

    /// <summary>
    /// 大于0显示
    /// </summary>
    public class MoreThanZeroVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? a = ConvertUtil.ToDecimal(value);
            if (a.HasValue && a.Value > 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed;
        }
    }
    /// <summary>
    /// 大于0隐藏
    /// </summary>
    public class MoreThanZeroCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal? a = ConvertUtil.ToDecimal(value);
            if (a.HasValue && a.Value > 0)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible;
        }
    }

    /// <summary>
    /// True为0
    /// </summary>
    public class TrueZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = ConvertUtil.ToBool(value, true);
            if (b)
            {
                return 0;
            }
            else
            {
                return parameter;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int b = ConvertUtil.ToInt32(value, -1);
            return b == 0;
        }
    }

    /// <summary>
    /// false为0
    /// </summary>
    public class FalseZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = ConvertUtil.ToBool(value, false);
            if (b)
            {
                return parameter;
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int b = ConvertUtil.ToInt32(value, -1);
            return b != 0;
        }
    }
}