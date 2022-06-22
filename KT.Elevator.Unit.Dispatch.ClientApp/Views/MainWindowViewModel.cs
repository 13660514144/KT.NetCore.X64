using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Network;
using KT.Elevator.Unit.Dispatch.ClientApp.Views.Controls;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        private UserControl _showControl;
        private WelcomeControl _welcomeControl;
        private WarnTipControl _warnTipControl;
         
        private SuccessControl _successControl;
        private HubHelper _hubHelper;
        private ConfigHelper _configHelper;

        private ILogger _logger;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private AppSettings _appSettings;
        private INettyServerHost _nettyServerHost;

        public MainWindowViewModel()
        {
            WelcomeControl = ContainerHelper.Resolve<WelcomeControl>();
            WarnTipControl = ContainerHelper.Resolve<WarnTipControl>();
            _successControl = ContainerHelper.Resolve<SuccessControl>();

            _regionManager = ContainerHelper.Resolve<IRegionManager>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<HandleElevatorReceiveEvent>().Subscribe(HandleElevatorReceive);
            _eventAggregator.GetEvent<LinkToHomeEvent>().Subscribe(LinkToHome);
        }

        private void HandleElevatorReceive(HitachiReceiveModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ShowControl = _successControl;
            });
        }

        /// <summary>
        /// 回到首页
        /// </summary>
        private void LinkToHome()
        {
            _welcomeControl.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public async Task InitAsync()
        {
            //启动http服务
            _nettyServerHost = ContainerHelper.Resolve<INettyServerHost>();
            await _nettyServerHost.RunAsync(_appSettings.Port + 1);
        }

        internal async Task EndAsync()
        {
            if (_nettyServerHost != null)
            {
                await _nettyServerHost.CloseAsync();
            }
        }

        public WelcomeControl WelcomeControl
        {
            get
            {
                return _welcomeControl;
            }

            set
            {
                SetProperty(ref _welcomeControl, value);
            }
        }
        public WarnTipControl WarnTipControl
        {
            get
            {
                return _warnTipControl;
            }

            set
            {
                SetProperty(ref _warnTipControl, value);
            }
        }

        public UserControl ShowControl
        {
            get
            {
                return _showControl;
            }

            set
            {
                SetProperty(ref _showControl, value);
            }
        }
    }
}
