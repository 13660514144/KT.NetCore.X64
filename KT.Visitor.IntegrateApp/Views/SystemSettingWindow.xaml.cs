using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Views.Setting;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.IntegrateApp.Views
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

            this.Loaded += SystemSettingWindow_Loaded;
        }

        private void SystemSettingWindow_Loaded(object sender, RoutedEventArgs e)
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
