using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using Panuon.UI.Silver;
using System.Windows;

namespace KT.Visitor.Interface.Controls.BaseWindows
{
    /// <summary>
    /// 消息弹窗
    /// MessageInfoBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageInfoBox : WindowX
    {
        public MessageInfoBoxViewModel ViewModel;

        private DialogHelper _dialogHelper;

        public MessageInfoBox()
        {
            InitializeComponent();

            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            ViewModel = ContainerHelper.Resolve<MessageInfoBoxViewModel>();

            this.DataContext = ViewModel;
        }
        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            e.Handled = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            e.Handled = true;
        }

        public bool? ShowMessage(string mesg, string title = "错误", string confirmName = "确定", string cancelName = "")
        {
            ViewModel.SetValue(mesg, title, confirmName, cancelName);
            return _dialogHelper.ShowDialog(this);
        }
    }
}
