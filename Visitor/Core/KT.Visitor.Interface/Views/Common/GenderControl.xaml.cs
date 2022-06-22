using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KT.Visitor.Interface.Views.Common
{
    /// <summary>
    /// GenderControl.xaml 的交互逻辑
    /// </summary>
    public partial class GenderControl : UserControl
    {
        public GenderControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty GenderValueProperty = DependencyProperty.Register("GenderValue", typeof(string), typeof(GenderControl), new PropertyMetadata("MALE", OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (GenderControl)d;
            if (e.NewValue != null)
            {
                control.SelectGender(e.NewValue.ToString());
            }
        }

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

        public void SelectGender(string sex)
        {
            if (sex == "MALE")
            {
                Color color = (Color)ColorConverter.ConvertFromString("{DynamicResource Scb_Theme08}");

                btnMan.Background = new SolidColorBrush(color);
                btnMan.Foreground = new SolidColorBrush(Colors.White);
                img_man.Source = new BitmapImage(new Uri("/Resources/Images/male.png", UriKind.RelativeOrAbsolute));

                //woman
                btnWoMan.Background = new SolidColorBrush(Colors.White);

                btnWoMan.Foreground = new SolidColorBrush(Colors.Black);
                img_woman.Source = new BitmapImage(new Uri("/Resources/Images/female1.png", UriKind.RelativeOrAbsolute));
            }
            else if (sex == "FEMALE")
            {
                Color color = (Color)ColorConverter.ConvertFromString("{DynamicResource Scb_Theme08}");

                btnWoMan.Background = new SolidColorBrush(color);
                btnWoMan.Foreground = new SolidColorBrush(Colors.White);
                img_man.Source = img_man.Source = new BitmapImage(new Uri("/Resources/Images/male1.png", UriKind.RelativeOrAbsolute));
                //woman
                btnMan.Background = new SolidColorBrush(Colors.White);
                btnMan.Foreground = new SolidColorBrush(Colors.Black);
                img_woman.Source = new BitmapImage(new Uri("/Resources/Images/female.png", UriKind.RelativeOrAbsolute)); ;
            }
        }
    }
}
