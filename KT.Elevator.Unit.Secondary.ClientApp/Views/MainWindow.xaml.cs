using ContralServer.CfgFileRead;
using KT.Common.Core.Helpers;
using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Device.Hitach;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using System;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using HelperTools;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace KT.Elevator.Unit.Secondary.ClientApp.Views
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
        private ScommModel _ScommModel;
        public static string dir = AppDomain.CurrentDomain.BaseDirectory;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _viewModel = ContainerHelper.Resolve<MainWindowViewModel>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _logger = ContainerHelper.Resolve<ILogger>();
            string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
            if (ElevatorType == "1")
            {
                _ScommModel = ContainerHelper.Resolve<ScommModel>();//这里必需引入电梯队列
            }
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
            //_logger.LogInformation($"_QueueElevaor:{JsonConvert.SerializeObject(_ScommModel._QueueElevaor)}");
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
