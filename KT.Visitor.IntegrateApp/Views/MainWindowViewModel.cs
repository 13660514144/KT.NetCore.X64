using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.IntegrateApp.Views.Register;
using KT.Visitor.Interface.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.IntegrateApp.Views
{
    public class MainWindowViewModel : Prism.Mvvm.BindableBase
    {
        private RegisterHomeControl _integrateRegisterControl;
        private TopPanelViewModel _topPanel;
        private ConfigHelper _configHelper;
        public SystemInfoViewModel _systemInfo;

        private StatisticTimerHelper _statisticTimerHelper;
        private IVisitorApi _visitorApi;
        private IFunctionApi _functionApi;
        private ILogger _logger;

        public MainWindowViewModel()
        {
            RegisterHomeControl = ContainerHelper.Resolve<RegisterHomeControl>();
            TopPanel = ContainerHelper.Resolve<TopPanelViewModel>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _statisticTimerHelper = ContainerHelper.Resolve<StatisticTimerHelper>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _logger = ContainerHelper.Resolve<ILogger>();

            SystemInfo = ContainerHelper.Resolve<SystemInfoViewModel>();

            _statisticTimerHelper.Start(SetStatisticAsync);
        }

        private async Task SetStatisticAsync()
        {
            try
            {
                var statistic = await _visitorApi.StatisticAsync();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TopPanel.TodyVisitorNum = statistic.VisitorNum;
                    TopPanel.VisitingNum = statistic.TotalVisitorNum;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("获取统计数据错误：{0} ", ex);
            }
        }

        public TopPanelViewModel TopPanel
        {
            get
            {
                return _topPanel;
            }

            set
            {
                SetProperty(ref _topPanel, value);
            }
        }

        public RegisterHomeControl RegisterHomeControl
        {
            get
            {
                return _integrateRegisterControl;
            }

            set
            {
                SetProperty(ref _integrateRegisterControl, value);
            }
        }

        public SystemInfoViewModel SystemInfo
        {
            get
            {
                return _systemInfo;
            }

            set
            {
                SetProperty(ref _systemInfo, value);
            }
        }
    }
}
