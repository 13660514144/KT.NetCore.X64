using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.FrontApp.Views.Register;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Models;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views;
using KT.Visitor.Interface.Views.Register;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.FrontApp.Views
{
    public class MainWindowViewModel : BindableBase
    {
        //访客登记
        private ICommand _linkRegisterCommand;
        public ICommand LinkRegisterCommand => _linkRegisterCommand ??= new DelegateCommand(RegisterHome);

        //访客记录
        private ICommand _linkVisitorRecordCommand;
        public ICommand LinkVisitorRecordCommand => _linkVisitorRecordCommand ??= new DelegateCommand(VisitorRecordList);

        //黑名单
        private ICommand _linkBlacklistCommand;
        public ICommand LinkBlacklistCommand => _linkBlacklistCommand ??= new DelegateCommand(VisitorBlacklist);

        //通知
        private ICommand _notifyCommand;
        public ICommand NotifyCommand => _notifyCommand ??= new DelegateCommand(Notify);

        private TotalVisitorViewModel _totalVisitor;
        public SystemInfoViewModel _systemInfo;

        private RegisterHomeControl _registerHomeControl;
        private VisitorRecordListControl _visitorRecordListControl;
        private VisitorImportRecordListControl _visitorImportRecordListControl;
        private VisitorBlacklistControl _visitorBlacklistControl;
        private NotifyControl _notifyControl;
        private AddBlacklistControl _addBlacklistControl;
        private VisitorDetailControl _visitorDetailControl;
        private VisitorImportDetailListControl _visitorImportDetailListControl;
        private Timer _totalTimer;

        private IRegion _contentRegion;
        private IFunctionApi _functionApi;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private ILogger _logger;
        private IVisitorApi _visitorApi;

        public MainWindowViewModel()
        {
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();

            //链接
            _eventAggregator.GetEvent<NavLinkEvent>().Subscribe(SubscribeLink);
            _eventAggregator.GetEvent<VisitorRegisteEvent>().Subscribe(RegisterHome);
            //登记成功
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(EndRegisted);
            //新增黑名单
            _eventAggregator.GetEvent<AddBlacklistEvent>().Subscribe(AddBlacklist);

            SystemInfo = ContainerHelper.Resolve<SystemInfoViewModel>();

            TotalVisitorAsync();
        }

        private void AddBlacklist()
        {
            _ = _addBlacklistControl.ViewModel.InitAsync();
            _contentRegion.Activate(_addBlacklistControl);
        }

        /// <summary>
        /// 访客登记结束
        /// </summary>
        private void EndRegisted()
        {
            InitAsync();
        }

        /// <summary>
        /// 访客统计
        /// </summary>
        private void TotalVisitorAsync()
        {
            _totalVisitor = new TotalVisitorViewModel();
            _totalTimer = new Timer(TotalVisitorCallback, null, 100, 60000);
        }

        /// <summary>
        /// 获取访客统计
        /// </summary>
        /// <param name="state"></param>
        private void TotalVisitorCallback(object state)
        {
            //异步更新
            RefreshTotalVisitorAsync();
        }

        private async void RefreshTotalVisitorAsync()
        {
            try
            {
                var result = await _visitorApi.StatisticAsync();
                if (result == null)
                {
                    return;
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    TotalVisitor.TodayVistorNum = result.VisitorNum;
                    TotalVisitor.TotalVisitorNum = result.TotalVisitorNum;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("统计错误：ex:{0} ", ex);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="navLink">页面跳转参数</param>
        private async void SubscribeLink(NavLinkModel navLink)
        {
            // 跳转到访客登记页面 
            if (navLink.View.Value == FontNavEnum.VISITOR_REGISTE.Value)
            {
                //当前页面为选择公司页面
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISTIOR_SELECT_COMPANY;

                RegisterHome();
            }
            // 跳转到访客记录 
            else if (navLink.View.Value == FontNavEnum.VISITOR_RECORD.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_RECORD;

                VisitorRecordList();
            }
            // 跳转到黑名单      
            else if (navLink.View.Value == FontNavEnum.BLACKLIST.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.BLACKLIST;

                VisitorBlacklist();
            }
            // 修改黑名单      
            else if (navLink.View.Value == FontNavEnum.EDIT_BLACKLIST.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.EDIT_BLACKLIST;

                await _addBlacklistControl.ViewModel.RefreshEditAsync(navLink.Data.ToLong());
                _contentRegion.Activate(_addBlacklistControl);
            }
            // 访客详情    
            else if (navLink.View.Value == FontNavEnum.VISITOR_DETAIL.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_DETAIL;

                await _visitorDetailControl.InitAsync(navLink.Data.ToLong());
                _contentRegion.Activate(_visitorDetailControl);
            }
            // 访客详情    
            else if (navLink.View.Value == FontNavEnum.VISITOR_IMPORT_DETAIL.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_IMPORT_DETAIL;

                await _visitorImportDetailListControl.InitAsync((VisitorImportModel)navLink.Data);
                _contentRegion.Activate(_visitorImportDetailListControl);
            }
            // 访客详情    
            else if (navLink.View.Value == FontNavEnum.VISITOR_IMPORT.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_IMPORT;

                //激活访客列表页面
                _visitorImportRecordListControl.InitViewModel();
                _contentRegion.Activate(_visitorImportRecordListControl);
            }
        }

        /// <summary>
        /// 页面加载完成初始化数据
        /// </summary>
        public async void InitAsync()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _contentRegion.Activate(_registerHomeControl);
            });

            await Task.CompletedTask;
        }

        public void ViewLoadedInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _contentRegion = _regionManager.Regions[RegionNameHelper.FontMainContentRegion];

            _registerHomeControl = ContainerHelper.Resolve<RegisterHomeControl>();
            _visitorRecordListControl = ContainerHelper.Resolve<VisitorRecordListControl>();
            _visitorImportRecordListControl = ContainerHelper.Resolve<VisitorImportRecordListControl>();
            _visitorBlacklistControl = ContainerHelper.Resolve<VisitorBlacklistControl>();
            _notifyControl = ContainerHelper.Resolve<NotifyControl>();
            _addBlacklistControl = ContainerHelper.Resolve<AddBlacklistControl>();
            _visitorDetailControl = ContainerHelper.Resolve<VisitorDetailControl>();
            _visitorImportDetailListControl = ContainerHelper.Resolve<VisitorImportDetailListControl>();

            _contentRegion.Add(_registerHomeControl);
            _contentRegion.Add(_visitorRecordListControl);
            _contentRegion.Add(_visitorImportRecordListControl);
            _contentRegion.Add(_visitorBlacklistControl);
            _contentRegion.Add(_notifyControl);
            _contentRegion.Add(_addBlacklistControl);
            _contentRegion.Add(_visitorDetailControl);
            _contentRegion.Add(_visitorImportDetailListControl);

            //初始化数据
            InitAsync();
        }


        /// <summary>
        /// 通知
        /// </summary>
        private void Notify()
        {
            //激活访客登记页面
            _ = _notifyControl.ViewModel.RefreshAsync();
            _contentRegion.Activate(_notifyControl);
        }

        /// <summary>
        /// 导航到访客登记页面,创建新页面
        /// </summary>
        private void RegisterHome()
        {
            //激活访客登记页面
            _contentRegion.Activate(_registerHomeControl);
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Publish();
        }

        /// <summary>
        /// 导航到访客列表页面,创建新页面
        /// </summary>
        private void VisitorRecordList()
        {
            //激活访客列表页面
            _visitorRecordListControl.InitViewModel();
            _contentRegion.Activate(_visitorRecordListControl);
        }

        /// <summary>
        /// 导航到黑名单页面,创建新页面
        /// </summary>
        private void VisitorBlacklist()
        {
            //激活黑名单页面
            _visitorBlacklistControl.InitViewModel();
            _contentRegion.Activate(_visitorBlacklistControl);
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

        public TotalVisitorViewModel TotalVisitor
        {
            get
            {
                return _totalVisitor;
            }

            set
            {
                SetProperty(ref _totalVisitor, value);
            }
        }
    }
}
