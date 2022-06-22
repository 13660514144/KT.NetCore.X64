using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using System;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Setting
{
    /// <summary>
    /// IntegrateSystemSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateSystemSettingWindow : WindowX
    {
        private SystemSettingWindowViewModel _viewModel;
        private ILogger<IntegrateSystemSettingWindow> _logger;
        private SmallTicketOperator _smallTicketOperator;

        public IntegrateSystemSettingWindow(ILogger<IntegrateSystemSettingWindow> logger,
            SystemSettingWindowViewModel viewModel,
            SmallTicketOperator smallTicketOperator)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _logger = logger;
            _smallTicketOperator = smallTicketOperator;

            this.Loaded += IntegrateSystemSettingWindow_Loaded;
        }

        private void IntegrateSystemSettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.Init();
            this.DataContext = _viewModel;

            Init();
        }

        private void Init()
        {
            // 打印机
            InitPrinter();
            // 发卡机
            //InitCardDevice();
        }

        /// <summary>
        /// 初始化打印机
        /// </summary>
        private void InitPrinter()
        {
            if (PrinterSettings.InstalledPrinters == null
                || PrinterSettings.InstalledPrinters.Count <= 0)
            {
                return;
            }
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                _viewModel.Printers.Add(item.ToString());
            }
            _viewModel.Printers.Insert(0, string.Empty);
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
            MessageWarnBox.Show("保存成功！");
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
