using KT.Common.WpfApp.Helpers;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Register
{
    /// <summary>
    /// MultiRegisterWarnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MultiRegisterWarnWindow : WindowX
    {
        public MultiRegisterWarnWindowViewModel ViewModel { get; set; }
        public MultiRegisterWarnWindow()
        {
            InitializeComponent();
            ViewModel = ContainerHelper.Resolve<MultiRegisterWarnWindowViewModel>();

            this.DataContext = ViewModel;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            _ = ReregisterAsync();
        }

        private async Task ReregisterAsync()
        {
            await ViewModel.ReappointAsync();
            this.DialogResult = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.IsCancel = true;
            this.Close();
        }
    }
}
