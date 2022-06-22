using KT.Common.WpfApp.Helpers;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Views.Visitor.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// AccompanyVisitorControl.xaml 的交互逻辑
    /// </summary>
    public partial class AccompanyVisitorControl : UserControl
    {
        public AccompanyVisitorControlViewModel ViewModel { get; set; }

        public AccompanyVisitorControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<AccompanyVisitorControlViewModel>();

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
