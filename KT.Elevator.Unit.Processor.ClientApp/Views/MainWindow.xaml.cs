using KT.Common.Core.Helpers;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using System;
using System.Windows;
using System.Windows.Input;

namespace KT.Elevator.Unit.Processor.ClientApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowX
    {
        private MainWindowViewModel _viewModel;
        private KeyboardHook _keyboardHook;

        private HubHelper _hubHelper;
        private AppSettings _appSettings;
        private ILogger _logger;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<MainWindowViewModel>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();

            this.DataContext = _viewModel;

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;

            //安装键盘钩子 
            _keyboardHook = new KeyboardHook();
            _keyboardHook.KeyDownEvent += _keyboardHook_KeyDownEvent;
            _keyboardHook.Start();
        }

        private void _keyboardHook_KeyDownEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
                Environment.Exit(-1);
            }
            else
            {
                _viewModel.AddCardNumberKey(e);
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置全屏
            WindowHelper.ReleaseFullWindow(this, _appSettings.IsFullScreen);

            // 初始化
            _viewModel.InitOrderAsync();

        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            EnvironmentExitHelper.Start();

            //销毁
            try
            {
                await _viewModel.EndAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"关闭程序销毁数据失败：ex:{ex} ");
            }
            finally
            {
                Environment.Exit(-1);
            }
        }
    }
}
