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
    /// IdCardCheckWarnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IdCardCheckWarnWindow : WindowX
    {
        public IdCardCheckWarnWindowViewModel ViewModel { get; set; }
        public IdCardCheckWarnWindow()
        {
            InitializeComponent();
            ViewModel = ContainerHelper.Resolve<IdCardCheckWarnWindowViewModel>();

            this.DataContext = ViewModel;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        internal void SetButtonText(string text)
        {
            CancelButton.Content = text;
        }
    }
}
