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

namespace KT.Visitor.Interface.Views.Visitor.Controls
{
    /// <summary>
    /// CompanyShowSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class CompanyShowSelectControl : UserControl
    {
        public CompanyShowSelectControlViewModel ViewModel;

        public CompanyShowSelectControl(CompanyShowSelectControlViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }

        public RoutedEventHandler SelectedClick;

        private void BtnOperate_Click(object sender, RoutedEventArgs e)
        {
            SelectedClick?.Invoke(this, e);
        }
    }
}
