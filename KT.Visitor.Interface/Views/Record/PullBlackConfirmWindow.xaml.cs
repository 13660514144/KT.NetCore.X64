using Panuon.UI.Silver;
using System.Windows;

namespace KT.Visitor.Interface.Views
{
    /// <summary>
    /// PullBlackConfirmWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PullBlackConfirmWindow : WindowX
    {
        public PullBlackConfirmWindowViewModel ViewModel;
        public PullBlackConfirmWindow()
        {
            InitializeComponent();

            this.ViewModel = new PullBlackConfirmWindowViewModel();

            this.DataContext = ViewModel;
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
