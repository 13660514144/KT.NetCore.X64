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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateCompanyShowDetailControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateCompanyShowDetailControl : UserControl
    {
        public IntegrateCompanyShowDetailControlViewModel ViewModel;
        public IntegrateCompanyShowDetailControl(IntegrateCompanyShowDetailControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.DataContext = ViewModel;
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
