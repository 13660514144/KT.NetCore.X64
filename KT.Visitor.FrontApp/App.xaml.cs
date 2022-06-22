using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.Common.Tools.Printer.Helpers;
using KT.Visitor.Common.Tools.Printer.PrintOperator;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Data.Daos;
using KT.Visitor.Data.IDaos;
using KT.Visitor.Data.IServices;
using KT.Visitor.Data.Services;
using KT.Visitor.Data.Updates;
using KT.Visitor.FrontApp.Views;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Views;
using KT.Visitor.Interface.Views.Auth;
using KT.Visitor.Interface.Views.Auth.Controls;
using KT.Visitor.Interface.Views.Common;
using KT.Visitor.Interface.Views.Controls;
using KT.Visitor.Interface.Views.Register;
using KT.Visitor.Interface.Views.Setting;
using KT.Visitor.Interface.Views.User;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace KT.Visitor.FrontApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger<App> _logger;
        private IEventAggregator _eventAggregator;
        public App()
        {
            _logger = new Log4gHelper<App>();

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Visitor.FrontApp", out bool createNew);

            if (!createNew)
            {
                MessageBox.Show("已经存在运行的应用程序，不能同时运行多个应用程序！");
                App.Current.Shutdown();
                Environment.Exit(0);
            }

            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger?.LogError($"App_DispatcherUnhandledException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            var exception = e.Exception.GetInner();

            Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);

            //发布错误事件
            _eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);

            //标识事件已提交
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            _logger?.LogError($"CurrentDomain_UnhandledException Error：{JsonConvert.SerializeObject(ex, JsonUtil.JsonPrintSettings)} ");

            var exception = ex.GetInner();
            Dispatcher.Invoke(() =>
            {
                Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);
            });
            //发布错误事件
            _eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger?.LogError($"TaskScheduler_UnobservedTaskException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            //异步处理错误消息
            TaskSchedulerUnobservedTaskExceptionExecAsync(e);

            //将异常标识为已经观察到 
            e.SetObserved();
        }

        private Task TaskSchedulerUnobservedTaskExceptionExecAsync(UnobservedTaskExceptionEventArgs e)
        {
            return Task.Run(() =>
            {
                var exception = e.GetInner();
                Dispatcher.Invoke(() =>
                {
                    Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);
                });

                //发布错误事件
                _eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);
            });
        }

        private async Task RegistedAsync()
        {
            //更新数据库
            await DbUpdateHelper.InitDbAsync();

            //更新配置
            var configHelper = Container.Resolve<ConfigHelper>();
            await configHelper.RefreshAsync();

            _eventAggregator = Container.Resolve<IEventAggregator>();

            //设备在线状态
            var onlineHeatHelper = Container.Resolve<OnlineHeatHelper>();
            onlineHeatHelper.Start(30);

            //系统时间同步
            var systemTimeHelper = ContainerHelper.Resolve<SystemTimeHelper>();
            await systemTimeHelper.StartAsync();

            //文件清除
            var cleanFileHelper = Container.Resolve<CleanFileHelper>();
            var cleanFileSettings = Container.Resolve<CleanFileSettings>();
            await cleanFileHelper.StartAsync(cleanFileSettings);
        }

        protected override Window CreateShell()
        {
            var window = Container.Resolve<LoginWindow>();
            var dialogHelper = Container.Resolve<DialogHelper>();
            dialogHelper.AddFirst(window);
            return window;
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                //获取View名称
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

                //获取ViewModel名称
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = $"{viewName}{suffix}, {viewAssemblyName}";

                _logger.LogInformation($"ViewModel绑定：viewName：{viewName} viewModelName：{viewModelName}");

                //返回结果
                return Type.GetType(viewModelName);
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var logger = new Log4gHelper();

            containerRegistry.RegisterInstance(Container);

            ContainerHelper.SetProvider(logger, Container);

            //确定值的类
            containerRegistry.RegisterInstance<ILogger>(logger);

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            containerRegistry.RegisterInstance<IMemoryCache>(memoryCache);

            //弹窗
            containerRegistry.RegisterDialog<ContentConfirmWindow, ContentConfirmWindowViewModel>();

            containerRegistry.Register<MessageWarnBox>();
            containerRegistry.RegisterSingleton<DialogHelper>();

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var idReaderSettings = PrismDependency.GetConfiguration().GetSection("IdReaderSettings").Get<IdReaderSettings>();
            containerRegistry.RegisterInstance(idReaderSettings);
            var arcFaceSettings = PrismDependency.GetConfiguration().GetSection("ArcFaceSettings").Get<ArcFaceSettings>();
            containerRegistry.RegisterInstance(arcFaceSettings);
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            //后台Api接口
            containerRegistry.RegisterInstance<ILogger<FrontVisitorApi>>(new Log4gHelper<FrontVisitorApi>());
            containerRegistry.RegisterInstance<ILogger<BackendApiBase>>(new Log4gHelper<BackendApiBase>());
            containerRegistry.RegisterInstance<ILogger<FrontCompanyApi>>(new Log4gHelper<FrontCompanyApi>());
            containerRegistry.RegisterInstance<ILogger<FrontLoginApi>>(new Log4gHelper<FrontLoginApi>());
            containerRegistry.RegisterInstance<ILogger<OpenApi>>(new Log4gHelper<OpenApi>());
            containerRegistry.RegisterInstance<ILogger<UploadImgApi>>(new Log4gHelper<UploadImgApi>());
            containerRegistry.RegisterInstance<ILogger<FrontBlacklistApi>>(new Log4gHelper<FrontBlacklistApi>());
            containerRegistry.RegisterInstance<ILogger<FrontFunctionApi>>(new Log4gHelper<FrontFunctionApi>());
            containerRegistry.RegisterInstance<ILogger<ElevatorAuthInfoApi>>(new Log4gHelper<ElevatorAuthInfoApi>());

            containerRegistry.RegisterInstance<ILogger<LoginUserDataDao>>(new Log4gHelper<LoginUserDataDao>());
            containerRegistry.RegisterInstance<ILogger<SystemConfigDataDao>>(new Log4gHelper<SystemConfigDataDao>());
            containerRegistry.RegisterInstance<ILogger<LoginUserDataService>>(new Log4gHelper<LoginUserDataService>());
            containerRegistry.RegisterInstance<ILogger<SystemConfigDataService>>(new Log4gHelper<SystemConfigDataService>());
            containerRegistry.RegisterInstance<ILogger<Reader>>(new Log4gHelper<Reader>());
            containerRegistry.RegisterInstance<ILogger<HD900>>(new Log4gHelper<HD900>());
            containerRegistry.RegisterInstance<ILogger<CVR100U>>(new Log4gHelper<CVR100U>());
            containerRegistry.RegisterInstance<ILogger<CVR100XG>>(new Log4gHelper<CVR100XG>());
            containerRegistry.RegisterInstance<ILogger<THPR210>>(new Log4gHelper<THPR210>());
            containerRegistry.RegisterInstance<ILogger<FS531>>(new Log4gHelper<FS531>());
            containerRegistry.RegisterInstance<ILogger<Development>>(new Log4gHelper<Development>());
            containerRegistry.RegisterInstance<ILogger<ConfigHelper>>(new Log4gHelper<ConfigHelper>());
            containerRegistry.RegisterInstance<ILogger<SmallTicketOperator>>(new Log4gHelper<SmallTicketOperator>());
            containerRegistry.RegisterInstance<ILogger<ReaderFactory>>(new Log4gHelper<ReaderFactory>());
            containerRegistry.RegisterInstance<ILogger<MainWindow>>(new Log4gHelper<MainWindow>());
            containerRegistry.RegisterInstance<ILogger<VisitorRecordListControlViewModel>>(new Log4gHelper<VisitorRecordListControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorRecordListControl>>(new Log4gHelper<VisitorRecordListControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorDetailControl>>(new Log4gHelper<VisitorDetailControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorDynamicDetailControl>>(new Log4gHelper<VisitorDynamicDetailControl>());
            containerRegistry.RegisterInstance<ILogger<ShowTakePictureControlViewModel>>(new Log4gHelper<ShowTakePictureControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyWarnControl>>(new Log4gHelper<CompanyWarnControl>());
            containerRegistry.RegisterInstance<ILogger<AssistantVisitorControlViewModel>>(new Log4gHelper<AssistantVisitorControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<AssistantVisitorControl>>(new Log4gHelper<AssistantVisitorControl>());
            containerRegistry.RegisterInstance<ILogger<SystemLoginWindow>>(new Log4gHelper<SystemLoginWindow>());
            containerRegistry.RegisterInstance<ILogger<LoginWindowViewModel>>(new Log4gHelper<LoginWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<LoginWindow>>(new Log4gHelper<LoginWindow>());
            containerRegistry.RegisterInstance<ILogger<NotifyControl>>(new Log4gHelper<NotifyControl>());
            containerRegistry.RegisterInstance<ILogger<NotifyControlViewModel>>(new Log4gHelper<NotifyControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindow>>(new Log4gHelper<SystemSettingWindow>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindowViewModel>>(new Log4gHelper<SystemSettingWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyTreeCheckHelper>>(new Log4gHelper<CompanyTreeCheckHelper>());
            containerRegistry.RegisterInstance<ILogger<VistitorConfigHelper>>(new Log4gHelper<VistitorConfigHelper>());
            containerRegistry.RegisterInstance<ILogger<NavPageControlViewModel>>(new Log4gHelper<NavPageControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<NavPageControl>>(new Log4gHelper<NavPageControl>());
            containerRegistry.RegisterInstance<ILogger<GenderControl>>(new Log4gHelper<GenderControl>());
            containerRegistry.RegisterInstance<ILogger<CameraTakePhotoWindow>>(new Log4gHelper<CameraTakePhotoWindow>());
            containerRegistry.RegisterInstance<ILogger<PullBlackConfirmWindowViewModel>>(new Log4gHelper<PullBlackConfirmWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<PullBlackConfirmWindow>>(new Log4gHelper<PullBlackConfirmWindow>());
            containerRegistry.RegisterInstance<ILogger<AddBlacklistControlViewModel>>(new Log4gHelper<AddBlacklistControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<AddBlacklistControl>>(new Log4gHelper<AddBlacklistControl>());
            containerRegistry.RegisterInstance<ILogger<RegistVisitorViewModel>>(new Log4gHelper<RegistVisitorViewModel>());
            containerRegistry.RegisterInstance<ILogger<TreeCheckCompanyViewModel>>(new Log4gHelper<TreeCheckCompanyViewModel>());
            containerRegistry.RegisterInstance<ILogger<NavPageViewModel>>(new Log4gHelper<NavPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<ItemsCheckViewModel>>(new Log4gHelper<ItemsCheckViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyViewModel>>(new Log4gHelper<CompanyViewModel>());
            containerRegistry.RegisterInstance<ILogger<CaptureImageViewModel>>(new Log4gHelper<CaptureImageViewModel>());
            containerRegistry.RegisterInstance<ILogger<AuthorizeTimeLimitViewModel>>(new Log4gHelper<AuthorizeTimeLimitViewModel>());
            containerRegistry.RegisterInstance<ILogger<MainWindow>>(new Log4gHelper<MainWindow>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindow>>(new Log4gHelper<SystemSettingWindow>());
            containerRegistry.RegisterInstance<ILogger<CompanyControl>>(new Log4gHelper<CompanyControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyControlViewModel>>(new Log4gHelper<CompanyControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorsWindow>>(new Log4gHelper<AccompanyVisitorsWindow>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorsWindowViewModel>>(new Log4gHelper<AccompanyVisitorsWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowDetailControl>>(new Log4gHelper<CompanyShowDetailControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowDetailControlViewModel>>(new Log4gHelper<CompanyShowDetailControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorInputControl>>(new Log4gHelper<VisitorInputControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorInputControlViewModel>>(new Log4gHelper<VisitorInputControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorRegisterControl>>(new Log4gHelper<VisitorRegisterControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorRegisterControlViewModel>>(new Log4gHelper<VisitorRegisterControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanySelectListControl>>(new Log4gHelper<CompanySelectListControl>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorControl>>(new Log4gHelper<AccompanyVisitorControl>());

            //后台Api接口
            containerRegistry.Register<OpenApi>();
            containerRegistry.Register<BackendApiBase>();
            containerRegistry.Register<IBlacklistApi, FrontBlacklistApi>();
            containerRegistry.Register<ICompanyApi, FrontCompanyApi>();
            containerRegistry.Register<IFunctionApi, FrontFunctionApi>();
            containerRegistry.Register<ILoginApi, FrontLoginApi>();
            containerRegistry.Register<IVisitorApi, FrontVisitorApi>();
            containerRegistry.Register<IUploadImgApi, UploadImgApi>();
            containerRegistry.Register<IElevatorAuthInfoApi, ElevatorAuthInfoApi>();

            //Local Data
            containerRegistry.Register<ILoginUserDataDao, LoginUserDataDao>();
            containerRegistry.Register<ISystemConfigDataDao, SystemConfigDataDao>();
            containerRegistry.Register<ILoginUserDataService, LoginUserDataService>();
            containerRegistry.Register<ISystemConfigDataService, SystemConfigDataService>();

            //Device
            containerRegistry.Register<IReader, Reader>();

            //静态工具 
            containerRegistry.RegisterSingleton<ConfigHelper>();
            containerRegistry.RegisterSingleton<ReaderFactory>();
            containerRegistry.RegisterSingleton<PrintSourceProvider>();

            //注册日记
            containerRegistry.RegisterInstance<ILogger<IdentityAuthControl>>(new Log4gHelper<IdentityAuthControl>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthControlViewModel>>(new Log4gHelper<IdentityAuthControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthActiveControl>>(new Log4gHelper<IdentityAuthActiveControl>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthActiveControlViewModel>>(new Log4gHelper<IdentityAuthActiveControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthSearchControl>>(new Log4gHelper<IdentityAuthSearchControl>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthSearchControlViewModel>>(new Log4gHelper<IdentityAuthSearchControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthControl>>(new Log4gHelper<InviteAuthControl>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthControlViewModel>>(new Log4gHelper<InviteAuthControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthActiveControl>>(new Log4gHelper<InviteAuthActiveControl>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthActiveControlViewModel>>(new Log4gHelper<InviteAuthActiveControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthSearchControl>>(new Log4gHelper<InviteAuthSearchControl>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthSearchControlViewModel>>(new Log4gHelper<InviteAuthSearchControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanySearchSelectControl>>(new Log4gHelper<CompanySearchSelectControl>());
            containerRegistry.RegisterInstance<ILogger<CompanySearchSelectControlViewModel>>(new Log4gHelper<CompanySearchSelectControlViewModel>());

            //containerRegistry.Register<RegisterHomeControl>();
            //containerRegistry.Register<RegisterHomeControlViewModel>();
            //containerRegistry.RegisterInstance<ILogger<RegisterHomeControl>>(new Log4gHelper<RegisterHomeControl>());
            //containerRegistry.RegisterInstance<ILogger<RegisterHomeControlViewModel>>(new Log4gHelper<RegisterHomeControlViewModel>());

            containerRegistry.RegisterInstance<ILogger<AddAccompanyCardWindow>>(new Log4gHelper<AddAccompanyCardWindow>());
            containerRegistry.RegisterInstance<ILogger<AddAccompanyCardWindowViewModel>>(new Log4gHelper<AddAccompanyCardWindowViewModel>());


            //containerRegistry.Register<AddAccompanyPhotoWindow>();
            //containerRegistry.Register<AddAccompanyPhotoWindowViewModel>();
            containerRegistry.RegisterInstance<ILogger<AddAccompanyPhotoWindow>>(new Log4gHelper<AddAccompanyPhotoWindow>());
            containerRegistry.RegisterInstance<ILogger<AddAccompanyPhotoWindowViewModel>>(new Log4gHelper<AddAccompanyPhotoWindowViewModel>());

            containerRegistry.Register<AccompanyVisitorControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorControlViewModel>>(new Log4gHelper<AccompanyVisitorControlViewModel>());

            //containerRegistry.Register<VisitorBlacklistViewModel>();
            //containerRegistry.Register<SuccessWindow>();

            containerRegistry.Register<IdentityAuthDetailControl>();
            containerRegistry.Register<IdentityAuthDetailControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<IdentityAuthDetailControl>>(new Log4gHelper<IdentityAuthDetailControl>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthDetailControlViewModel>>(new Log4gHelper<IdentityAuthDetailControlViewModel>());

            //containerRegistry.Register<InviteAuthDetailControl>();
            //containerRegistry.Register<InviteAuthDetailControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<InviteAuthDetailControl>>(new Log4gHelper<InviteAuthDetailControl>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthDetailControlViewModel>>(new Log4gHelper<InviteAuthDetailControlViewModel>());
            //操作

            containerRegistry.RegisterSingleton<ArcIdSdkHelper>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());

            _ = RegistedAsync();
        }
    }
}
