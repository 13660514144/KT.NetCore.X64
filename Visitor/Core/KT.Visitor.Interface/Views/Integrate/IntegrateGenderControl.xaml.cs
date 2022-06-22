using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Common
{
    /// <summary>
    /// IntegrateGenderControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateGenderControl : UserControl
    {
        public IntegrateGenderControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty GenderValueProperty = DependencyProperty.Register("GenderValue", typeof(string), typeof(IntegrateGenderControl), new PropertyMetadata("MALE"));

        /// <summary>
        /// 性别值
        /// </summary>
        public string GenderValue
        {
            get { return (string)GetValue(GenderValueProperty); }
            set { SetValue(GenderValueProperty, value); }
        }

        private void BtnMan_Click(object sender, RoutedEventArgs e)
        {
            GenderValue = "MALE";
        }

        private void BtnWoMan_Click(object sender, RoutedEventArgs e)
        {
            GenderValue = "FEMALE";
        }
    }
}
