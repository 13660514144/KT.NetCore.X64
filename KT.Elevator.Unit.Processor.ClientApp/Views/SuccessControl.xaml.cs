using KT.Common.WpfApp.Helpers;
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

namespace KT.Elevator.Unit.Processor.ClientApp.Views
{
    /// <summary>
    /// SuccessControl.xaml 的交互逻辑
    /// </summary>
    public partial class SuccessControl : UserControl
    {
        public SuccessControlViewModel ViewModel { get; private set; }
        public SuccessControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<SuccessControlViewModel>();

            this.DataContext = ViewModel;
        }
    }
}
