using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using Panuon.UI.Silver;
using System.Windows;

namespace KT.Visitor.Interface.Controls.BaseWindows
{
    /// <summary>
    /// 消息弹窗
    /// MessageWarnBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWarnBox : WindowX
    {
        public MessageWarnBoxViewModel ViewModel;

        private DialogHelper _dialogHelper;

        public MessageWarnBox()
        {
            InitializeComponent();

            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            ViewModel = ContainerHelper.Resolve<MessageWarnBoxViewModel>();

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
