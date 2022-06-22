using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateSuccessWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateSuccessWindow : Window
    {
        public IntegrateSuccessWindow()
        {
            InitializeComponent();
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
