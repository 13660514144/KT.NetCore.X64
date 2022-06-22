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
    /// IntegrateAuthModeControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateAuthModeControl : UserControl
    {
        public IntegrateAuthModeControlViewModel ViewModel;
        public IntegrateAuthModeControl(IntegrateAuthModeControlViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            this.DataContext = ViewModel;
        }

        public RoutedEventHandler CancleClick;
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            CancleClick?.Invoke(this, e);
        }

        public RoutedEventHandler ConfirmClick;
        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            //更改授权方式
            ViewModel.RememberAuthType();
            ConfirmClick?.Invoke(this, e);
        }
    }
}
