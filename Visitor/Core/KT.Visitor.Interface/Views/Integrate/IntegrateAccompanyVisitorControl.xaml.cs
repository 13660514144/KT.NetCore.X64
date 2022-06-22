using KT.Visitor.Interface.Views.Visitor.Controls;
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
    /// IntegrateAccompanyVisitorControl.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateAccompanyVisitorControl : UserControl
    {
        public IntegrateAccompanyVisitorControlViewModel ViewModel { get; set; }

        public IntegrateAccompanyVisitorControl(IntegrateAccompanyVisitorControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            ViewModel.BindingControl = this;
            this.DataContext = ViewModel;
        }

        private void PUTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.IsFocus = true;
        }

        private void PUTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ViewModel.IsFocus = false;
        }
    }
}
