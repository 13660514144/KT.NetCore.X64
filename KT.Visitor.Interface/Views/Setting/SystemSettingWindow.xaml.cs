using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.Controls.BaseWindows;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.Interface.Views.Setting
{
    /// <summary>
    /// SystemSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SystemSettingWindow : WindowX
    {

        private SystemSettingWindowViewModel _viewModel;
        private ILogger _logger;
        private SmallTicketOperator _smallTicketOperator;
        private DialogHelper _dialogHelper;

        public SystemSettingWindow()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<SystemSettingWindowViewModel>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _smallTicketOperator = ContainerHelper.Resolve<SmallTicketOperator>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

            this.Loaded += IntegrateSystemSettingWindow_Loaded;
        }

        private void IntegrateSystemSettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewModel;
        }

        /// <summary>
        /// 初始化发卡机
        /// </summary>
        private void InitCardDevice()
        {
            throw new NotImplementedException();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            _ = SaveAsync();
        }

        private async Task SaveAsync()
        {
            await _viewModel.SaveSettingAsync();
            ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("保存成功！");
            //关闭窗口
            this.DialogResult = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            //关闭窗口
            this.DialogResult = false;
        }

        private void Btn_Print_Test_Click(object sender, RoutedEventArgs e)
        {
            _ = _viewModel.TestPrintAsync();
        }


    }
}
