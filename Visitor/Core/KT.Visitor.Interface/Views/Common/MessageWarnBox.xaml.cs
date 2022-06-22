using Panuon.UI.Silver;
using System;
using System.Windows;

namespace KT.Visitor.Interface.Controls.BaseWindows
{
    /// <summary>
    /// 消息弹窗
    /// MessageWarnBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWarnBox : WindowX
    {
        private MessageWarnBoxViewModel viewModel;

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="mesg"></param>
        /// <param name="title"></param>
        /// <param name="warnType"></param>
        /// <returns></returns>
        public static bool? Show(string mesg, string title = "错误", string confirmName = "确定", string cancelName = "")
        {
            return new MessageWarnBox().MesgShow(mesg, title, confirmName, cancelName);
        }

        /// <summary>
        /// 非UI线程弹窗提示
        /// 用于解决其它类库要弹出错误信息无法引用到弹窗的问题
        /// </summary>
        public static bool? DispatcherShow(string mesg, string title = "错误", string confirmName = "确定", string cancelName = "")
        {
            bool? result = null;
            Application.Current.MainWindow.Dispatcher.Invoke(new Action(() =>
            {
                result = new MessageWarnBox().MesgShow(mesg, title, confirmName, cancelName);
            }));
            return result;
        }

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="mesg"></param>
        /// <param name="title"></param>
        /// <param name="warnType"></param>
        /// <returns></returns>
        private bool? MesgShow(string mesg, string title, string confirmName, string cancelName)
        {
            this.viewModel.Message = mesg;
            this.viewModel.Title = title;
            this.viewModel.ConfirmName = confirmName;
            this.viewModel.CancelName = cancelName;

            return this.ShowDialog();
        }

        private MessageWarnBox()
        {
            InitializeComponent();
            this.viewModel = new MessageWarnBoxViewModel();
            this.DataContext = this.viewModel;
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
    }
}