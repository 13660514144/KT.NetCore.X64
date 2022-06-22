using KT.Common.WpfApp.Helpers;
using Panuon.UI.Silver;
using System.ComponentModel;
using System.Windows;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// SuccessWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SuccessWindow : WindowX
    {
        public SuccessWindowViewModel ViewModel;

        public SuccessWindow()
        {
            InitializeComponent();

            ViewModel = ContainerHelper.Resolve<SuccessWindowViewModel>();

            this.DataContext = ViewModel;
        }

        private void Btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
