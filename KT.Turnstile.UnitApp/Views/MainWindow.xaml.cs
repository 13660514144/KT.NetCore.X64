
using HelperTools;
using KT.Common.Core.Helpers;
using KT.Common.WebApi.HttpApi;
using KT.Common.WpfApp.Helpers;
using KT.Turnstile.Unit.ClientApp.Service.Helpers;
using KT.Turnstile.Unit.ClientApp.Views;
using KT.Turnstile.Unit.ClientApp.WsScoket;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Turnstile.Unit.ClientApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowX
    {
        private MainWindowViewModel _viewModel;
        private KeyboardHook _keyboardHook;
        private HubHelper _hubHelper;
        private ILogger _logger;
        private AppSettings _appSettings;
        private WebSocket4net _WsSocket;

       
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<MainWindowViewModel>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _WsSocket = ContainerHelper.Resolve<WebSocket4net>();
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
            //初始化 WS socket 数据通道
            _WsSocket.WebSocketInit();
            
        }
        
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            EnvironmentExitHelper.Start();

            //销毁
            try
            {

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
