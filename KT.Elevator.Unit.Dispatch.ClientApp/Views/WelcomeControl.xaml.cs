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

namespace KT.Elevator.Unit.Dispatch.ClientApp.Views
{
    /// <summary>
    /// WelcomeControl.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeControl : UserControl
    {
        public WelcomeControlViewModel ViewModel { get; private set; }

        public WelcomeControl()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<WelcomeControlViewModel>();

            this.DataContext = ViewModel;
        }
    }
}
