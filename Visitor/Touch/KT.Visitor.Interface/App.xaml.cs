using KT.Common.Core.Utils;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Core.Helpers;
using KT.Visitor.Data.Base;
using KT.Visitor.Data.Daos;
using KT.Visitor.Data.IDaos;
using KT.Visitor.Data.IServices;
using KT.Visitor.Data.Services;
using KT.Visitor.Data.Updates;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Core.Controls.BaseWindows;
using KT.Visitor.Core.Settings;
using KT.Visitor.Core.Tools.ImageHelper;
using KT.Visitor.Core.Tools.Printer.PrintOperator;
using KT.Visitor.Core.ViewModels;
using KT.Visitor.Core.Views.Auth;
using KT.Visitor.Core.Views.Auth.Controls;
using KT.Visitor.Core.Views.Blacklist;
using KT.Visitor.Core.Views.Common;
using KT.Visitor.Core.Views.Helper;
using KT.Visitor.Core.Views.Integrate;
using KT.Visitor.Core.Views.Setting;
using KT.Visitor.Core.Views.User;
using KT.Visitor.Core.Views.Visitor;
using KT.Visitor.Core.Views.Visitor.Controls;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KT.Visitor.Core;

namespace KT.Visitor.Interface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static IContainerProvider ContainerProvider;
        private ILogger<App> _logger;
        public App()
        {
            _logger = new Log4gHelper<App>();

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception.InnerException ?? e.Exception;
            _logger?.LogError("App_DispatcherUnhandledException Error:{0} ", JsonConvert.SerializeObject(exception, JsonUtil.JsonSettings));
            MessageWarnBox.Show(e.Exception.InnerException?.Message ?? e.Exception.Message);
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            var exception = ex.InnerException ?? ex;
            _logger?.LogError("CurrentDomain_UnhandledException Error:{0} ", JsonConvert.SerializeObject(exception, JsonUtil.JsonSettings));
            MessageWarnBox.Show(ex.InnerException?.Message ?? ex?.Message);
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception.InnerException ?? e.Exception;
            _logger?.LogError("TaskScheduler_UnobservedTaskException Error:{0} ", JsonConvert.SerializeObject(exception, JsonUtil.JsonSettings));
            Dispatcher.Invoke(() =>
            {
                MessageWarnBox.Show(e.Exception.InnerException?.Message ?? e.Exception.Message);
            });
            //将异常标识为已经观察到 
            e.SetObserved();
        }

        private void InitData()
        {
            //更新数据库
            DbUpdateHelper.InitDbAsync().Wait();

            var configHelper = Container.Resolve<ConfigHelper>();
            configHelper.Refresh();
        }

        protected override Window CreateShell()
        {
            ContainerProvider = Container;
            return Container.Resolve<IntegrateLoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ILogger>(new Log4gHelper());
            containerRegistry.RegisterInstance(Container);

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var idReaderSettings = PrismDependency.GetConfiguration().GetSection("IdReaderSettings").Get<IdReaderSettings>();
            containerRegistry.RegisterInstance(idReaderSettings);
            containerRegistry.Register<SqliteContext>();

            //后台Api接口
            containerRegistry.RegisterInstance<ILogger<AppointmentApi>>(new Log4gHelper<AppointmentApi>());
            containerRegistry.RegisterInstance<ILogger<BackendBaseApi>>(new Log4gHelper<BackendBaseApi>());
            containerRegistry.RegisterInstance<ILogger<CompanyApi>>(new Log4gHelper<CompanyApi>());
            containerRegistry.RegisterInstance<ILogger<LoginApi>>(new Log4gHelper<LoginApi>());
            containerRegistry.RegisterInstance<ILogger<NotifyApi>>(new Log4gHelper<NotifyApi>());
            containerRegistry.RegisterInstance<ILogger<PushApi>>(new Log4gHelper<PushApi>());
            containerRegistry.RegisterInstance<ILogger<UploadImgApi>>(new Log4gHelper<UploadImgApi>());
            containerRegistry.RegisterInstance<ILogger<VisitorAuthApi>>(new Log4gHelper<VisitorAuthApi>());
            containerRegistry.RegisterInstance<ILogger<BlacklistApi>>(new Log4gHelper<BlacklistApi>());
            containerRegistry.RegisterInstance<ILogger<VisitorRecordApi>>(new Log4gHelper<VisitorRecordApi>());
            containerRegistry.RegisterInstance<ILogger<VisitorTotalApi>>(new Log4gHelper<VisitorTotalApi>());
            containerRegistry.RegisterInstance<ILogger<VistitorConfigApi>>(new Log4gHelper<VistitorConfigApi>());
            containerRegistry.RegisterInstance<ILogger<FrontBaseApi>>(new Log4gHelper<FrontBaseApi>());

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
            containerRegistry.RegisterInstance<ILogger<MainFrameHelper>>(new Log4gHelper<MainFrameHelper>());
            containerRegistry.RegisterInstance<ILogger<SmallTicketOperator>>(new Log4gHelper<SmallTicketOperator>());
            containerRegistry.RegisterInstance<ILogger<ReaderFactory>>(new Log4gHelper<ReaderFactory>());
            containerRegistry.RegisterInstance<ILogger<FrontMainWindow>>(new Log4gHelper<FrontMainWindow>());
            containerRegistry.RegisterInstance<ILogger<MainWindowViewModel>>(new Log4gHelper<MainWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorRegisterPageViewModel>>(new Log4gHelper<VisitorRegisterPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorRegisterPage>>(new Log4gHelper<VisitorRegisterPage>());
            containerRegistry.RegisterInstance<ILogger<VisitorRecordListPageViewModel>>(new Log4gHelper<VisitorRecordListPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorRecordListPage>>(new Log4gHelper<VisitorRecordListPage>());
            containerRegistry.RegisterInstance<ILogger<VisitorDetailPage>>(new Log4gHelper<VisitorDetailPage>());
            containerRegistry.RegisterInstance<ILogger<VisitorDynamicDetailControl>>(new Log4gHelper<VisitorDynamicDetailControl>());
            containerRegistry.RegisterInstance<ILogger<TakePictureControlViewModel>>(new Log4gHelper<TakePictureControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<TakePictureControl>>(new Log4gHelper<TakePictureControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyWarnControl>>(new Log4gHelper<CompanyWarnControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowDetailControlViewModel>>(new Log4gHelper<CompanyShowDetailControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowDetailControl>>(new Log4gHelper<CompanyShowDetailControl>());
            containerRegistry.RegisterInstance<ILogger<CompanySelectListControlViewModel>>(new Log4gHelper<CompanySelectListControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanySelectListControl>>(new Log4gHelper<CompanySelectListControl>());
            containerRegistry.RegisterInstance<ILogger<AssistantVisitorControlViewModel>>(new Log4gHelper<AssistantVisitorControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<AssistantVisitorControl>>(new Log4gHelper<AssistantVisitorControl>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorControlViewModel>>(new Log4gHelper<AccompanyVisitorControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorControl>>(new Log4gHelper<AccompanyVisitorControl>());
            containerRegistry.RegisterInstance<ILogger<SystemLoginWindow>>(new Log4gHelper<SystemLoginWindow>());
            containerRegistry.RegisterInstance<ILogger<LoginWindowViewModel>>(new Log4gHelper<LoginWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<LoginWindow>>(new Log4gHelper<LoginWindow>());
            containerRegistry.RegisterInstance<ILogger<ConfigSetting>>(new Log4gHelper<ConfigSetting>());
            containerRegistry.RegisterInstance<ILogger<NotifyPage>>(new Log4gHelper<NotifyPage>());
            containerRegistry.RegisterInstance<ILogger<NotifyPageViewModel>>(new Log4gHelper<NotifyPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindow>>(new Log4gHelper<SystemSettingWindow>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindowViewModel>>(new Log4gHelper<SystemSettingWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyTreeCheckHelper>>(new Log4gHelper<CompanyTreeCheckHelper>());
            containerRegistry.RegisterInstance<ILogger<VistitorConfigHelper>>(new Log4gHelper<VistitorConfigHelper>());
            containerRegistry.RegisterInstance<ILogger<SuccessPage>>(new Log4gHelper<SuccessPage>());
            containerRegistry.RegisterInstance<ILogger<NavPageControlViewModel>>(new Log4gHelper<NavPageControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<NavPageControl>>(new Log4gHelper<NavPageControl>());
            containerRegistry.RegisterInstance<ILogger<GenderControl>>(new Log4gHelper<GenderControl>());
            containerRegistry.RegisterInstance<ILogger<CameraTakePhotoWindow>>(new Log4gHelper<CameraTakePhotoWindow>());
            containerRegistry.RegisterInstance<ILogger<VisitorBlacklist>>(new Log4gHelper<VisitorBlacklist>());
            containerRegistry.RegisterInstance<ILogger<PullBlackConfirmWindowViewModel>>(new Log4gHelper<PullBlackConfirmWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<PullBlackConfirmWindow>>(new Log4gHelper<PullBlackConfirmWindow>());
            containerRegistry.RegisterInstance<ILogger<AddBlacklistPageViewModel>>(new Log4gHelper<AddBlacklistPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<AddBlacklistPage>>(new Log4gHelper<AddBlacklistPage>());
            containerRegistry.RegisterInstance<ILogger<ReadIdCardPage>>(new Log4gHelper<ReadIdCardPage>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthActivePageViewModel>>(new Log4gHelper<InviteAuthActivePageViewModel>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthActivePage>>(new Log4gHelper<InviteAuthActivePage>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthPage>>(new Log4gHelper<IdentityAuthPage>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthActivePageViewModel>>(new Log4gHelper<IdentityAuthActivePageViewModel>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthActivePage>>(new Log4gHelper<IdentityAuthActivePage>());
            containerRegistry.RegisterInstance<ILogger<AuthMsgPage>>(new Log4gHelper<AuthMsgPage>());
            containerRegistry.RegisterInstance<ILogger<AppointAuthDetailControl>>(new Log4gHelper<AppointAuthDetailControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorInfoViewModel>>(new Log4gHelper<VisitorInfoViewModel>());
            containerRegistry.RegisterInstance<ILogger<TreeCheckViewModel>>(new Log4gHelper<TreeCheckViewModel>());
            containerRegistry.RegisterInstance<ILogger<NavPageViewModel>>(new Log4gHelper<NavPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<ItemsCheckViewModel>>(new Log4gHelper<ItemsCheckViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyViewModel>>(new Log4gHelper<CompanyViewModel>());
            containerRegistry.RegisterInstance<ILogger<ComboViewModel>>(new Log4gHelper<ComboViewModel>());
            containerRegistry.RegisterInstance<ILogger<CaptureImageViewModel>>(new Log4gHelper<CaptureImageViewModel>());
            containerRegistry.RegisterInstance<ILogger<AuthorizeTimeLimitViewModel>>(new Log4gHelper<AuthorizeTimeLimitViewModel>());
            containerRegistry.RegisterInstance<ILogger<QRCodeHelper>>(new Log4gHelper<QRCodeHelper>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowSelectControl>>(new Log4gHelper<CompanyShowSelectControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowSelectControlViewModel>>(new Log4gHelper<CompanyShowSelectControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanySearchSelectControl>>(new Log4gHelper<CompanySearchSelectControl>());
            containerRegistry.RegisterInstance<ILogger<CompanySearchSelectControlViewModel>>(new Log4gHelper<CompanySearchSelectControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanySelectControl>>(new Log4gHelper<CompanySelectControl>());
            containerRegistry.RegisterInstance<ILogger<CompanySelectControlViewModel>>(new Log4gHelper<CompanySelectControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<MainBarInfoViewModel>>(new Log4gHelper<MainBarInfoViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorsControl>>(new Log4gHelper<VisitorsControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorsControlViewModel>>(new Log4gHelper<VisitorsControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateMainWindow>>(new Log4gHelper<IntegrateMainWindow>());
            containerRegistry.RegisterInstance<ILogger<IntegrateMainWindowViewModel>>(new Log4gHelper<IntegrateMainWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateLoginWindow>>(new Log4gHelper<IntegrateLoginWindow>());
            containerRegistry.RegisterInstance<ILogger<IntegrateSystemSettingWindow>>(new Log4gHelper<IntegrateSystemSettingWindow>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanyControl>>(new Log4gHelper<IntegrateCompanyControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanyControlViewModel>>(new Log4gHelper<IntegrateCompanyControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateAccompanyVisitorsWindow>>(new Log4gHelper<IntegrateAccompanyVisitorsWindow>());
            containerRegistry.RegisterInstance<ILogger<IntegrateAccompanyVisitorsWindowViewModel>>(new Log4gHelper<IntegrateAccompanyVisitorsWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateAuthModeControl>>(new Log4gHelper<IntegrateAuthModeControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanyShowDetailControl>>(new Log4gHelper<IntegrateCompanyShowDetailControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanyShowDetailControlViewModel>>(new Log4gHelper<IntegrateCompanyShowDetailControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateVisitorControl>>(new Log4gHelper<IntegrateVisitorControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateVisitorControlViewModel>>(new Log4gHelper<IntegrateVisitorControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<InterateVisitorRegisterControl>>(new Log4gHelper<InterateVisitorRegisterControl>());
            containerRegistry.RegisterInstance<ILogger<InterateVisitorRegisterControlViewModel>>(new Log4gHelper<InterateVisitorRegisterControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanySelectListControl>>(new Log4gHelper<IntegrateCompanySelectListControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateAuthModeControlViewModel>>(new Log4gHelper<IntegrateAuthModeControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateAccompanyVisitorControl>>(new Log4gHelper<IntegrateAccompanyVisitorControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateGenderControl>>(new Log4gHelper<IntegrateGenderControl>());


            //后台Api接口
            containerRegistry.Register<AppointmentApi>();
            containerRegistry.Register<BackendBaseApi>();
            containerRegistry.Register<CompanyApi>();
            containerRegistry.Register<LoginApi>();
            containerRegistry.Register<NotifyApi>();
            containerRegistry.Register<PushApi>();
            containerRegistry.Register<UploadImgApi>();
            containerRegistry.Register<VisitorAuthApi>();
            containerRegistry.Register<BlacklistApi>();
            containerRegistry.Register<VisitorRecordApi>();
            containerRegistry.Register<VisitorTotalApi>();
            containerRegistry.Register<VistitorConfigApi>();

            //Local Data
            containerRegistry.Register<ILoginUserDataDao, LoginUserDataDao>();
            containerRegistry.Register<ISystemConfigDataDao, SystemConfigDataDao>();
            containerRegistry.Register<ILoginUserDataService, LoginUserDataService>();
            containerRegistry.Register<ISystemConfigDataService, SystemConfigDataService>();

            //Device
            containerRegistry.Register<IReader, Reader>();
            containerRegistry.Register<HD900>();
            containerRegistry.Register<CVR100U>();
            containerRegistry.Register<CVR100XG>();
            containerRegistry.Register<THPR210>();
            containerRegistry.Register<FS531>();
            containerRegistry.Register<Development>();

            //静态工具 
            containerRegistry.RegisterSingleton<ConfigHelper>();
            containerRegistry.RegisterSingleton<MainFrameHelper>();
            containerRegistry.RegisterSingleton<SmallTicketOperator>();
            containerRegistry.RegisterSingleton<ReaderFactory>();

            //控件与视频  containerRegistry.Register<>();
            containerRegistry.Register<FrontMainWindow>();
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<VisitorRegisterPageViewModel>();
            containerRegistry.Register<VisitorRegisterPage>();
            containerRegistry.Register<VisitorRecordListPageViewModel>();
            containerRegistry.Register<VisitorRecordListPage>();
            containerRegistry.Register<VisitorDetailPage>();
            containerRegistry.Register<VisitorDynamicDetailControl>();
            containerRegistry.Register<TakePictureControlViewModel>();
            containerRegistry.Register<TakePictureControl>();
            containerRegistry.Register<CompanyWarnControl>();
            containerRegistry.Register<CompanyShowDetailControlViewModel>();
            containerRegistry.Register<CompanyShowDetailControl>();
            containerRegistry.Register<CompanySelectListControlViewModel>();
            containerRegistry.Register<CompanySelectListControl>();

            containerRegistry.Register<AssistantVisitorControlViewModel>();
            containerRegistry.Register<AssistantVisitorControl>();
            containerRegistry.Register<AccompanyVisitorControlViewModel>();
            containerRegistry.Register<AccompanyVisitorControl>();
            containerRegistry.Register<SystemLoginWindow>();
            containerRegistry.Register<LoginWindowViewModel>();
            containerRegistry.Register<LoginWindow>();
            containerRegistry.Register<ConfigSetting>();
            containerRegistry.Register<NotifyPage>();
            containerRegistry.Register<NotifyPageViewModel>();
            containerRegistry.Register<SystemSettingWindow>();
            containerRegistry.Register<SystemSettingWindowViewModel>();
            containerRegistry.Register<CompanyTreeCheckHelper>();
            containerRegistry.Register<VistitorConfigHelper>();
            containerRegistry.Register<SuccessPage>();
            containerRegistry.Register<NavPageControlViewModel>();
            containerRegistry.Register<NavPageControl>();
            containerRegistry.Register<GenderControl>();
            containerRegistry.Register<CameraTakePhotoWindow>();
            containerRegistry.Register<VisitorBlacklist>();
            containerRegistry.Register<PullBlackConfirmWindowViewModel>();
            containerRegistry.Register<PullBlackConfirmWindow>();
            containerRegistry.Register<AddBlacklistPageViewModel>();
            containerRegistry.Register<AddBlacklistPage>();
            containerRegistry.Register<ReadIdCardPage>();
            containerRegistry.Register<InviteAuthActivePageViewModel>();
            containerRegistry.Register<InviteAuthActivePage>();
            containerRegistry.Register<IdentityAuthPage>();
            containerRegistry.Register<IdentityAuthActivePageViewModel>();
            containerRegistry.Register<IdentityAuthActivePage>();
            containerRegistry.Register<AuthMsgPage>();
            containerRegistry.Register<AppointAuthDetailControl>();
            containerRegistry.Register<VisitorInfoViewModel>();
            containerRegistry.Register<TreeCheckViewModel>();
            containerRegistry.Register<NavPageViewModel>();
            containerRegistry.Register<ItemsCheckViewModel>();
            containerRegistry.Register<CompanyViewModel>();
            containerRegistry.Register<ComboViewModel>();
            containerRegistry.Register<CaptureImageViewModel>();
            containerRegistry.Register<AuthorizeTimeLimitViewModel>();
            containerRegistry.Register<QRCodeHelper>();
            containerRegistry.Register<CompanyShowSelectControl>();
            containerRegistry.Register<CompanyShowSelectControlViewModel>();
            containerRegistry.Register<CompanySearchSelectControl>();
            containerRegistry.Register<CompanySearchSelectControlViewModel>();
            containerRegistry.Register<CompanySelectControl>();
            containerRegistry.Register<CompanySelectControlViewModel>();
            containerRegistry.Register<MainBarInfoViewModel>();
            containerRegistry.Register<VisitorsControl>();
            containerRegistry.Register<VisitorsControlViewModel>();

            // 一体机
            containerRegistry.Register<IntegrateMainWindow>();
            containerRegistry.Register<IntegrateMainWindowViewModel>();
            containerRegistry.Register<IntegrateLoginWindow>();
            containerRegistry.Register<IntegrateSystemSettingWindow>();
            containerRegistry.Register<IntegrateCompanyControl>();
            containerRegistry.Register<IntegrateCompanyControlViewModel>();
            containerRegistry.Register<IntegrateAccompanyVisitorsWindow>();
            containerRegistry.Register<IntegrateAccompanyVisitorsWindowViewModel>();
            containerRegistry.Register<IntegrateAuthModeControl>();
            containerRegistry.Register<IntegrateCompanyShowDetailControl>();
            containerRegistry.Register<IntegrateCompanyShowDetailControlViewModel>();
            containerRegistry.Register<IntegrateVisitorControl>();
            containerRegistry.Register<IntegrateVisitorControlViewModel>();
            containerRegistry.Register<InterateVisitorRegisterControl>();
            containerRegistry.Register<InterateVisitorRegisterControlViewModel>();
            containerRegistry.Register<IntegrateCompanySelectListControl>();
            containerRegistry.Register<IntegrateAuthModeControlViewModel>();
            containerRegistry.Register<IntegrateAccompanyVisitorControl>();
            containerRegistry.Register<IntegrateGenderControl>();
            containerRegistry.Register<IntegrateIdentityAuthControl>();
            containerRegistry.Register<IntegrateIdentityAuthControlViewModel>();
            containerRegistry.Register<IntegrateIdentityAuthActiveControl>();
            containerRegistry.Register<IntegrateIdentityAuthActiveControlViewModel>();
            containerRegistry.Register<IntegrateIdentityAuthSearchControl>();
            containerRegistry.Register<IntegrateIdentityAuthSearchControlViewModel>();
            containerRegistry.Register<IntegrateInviteAuthControl>();
            containerRegistry.Register<IntegrateInviteAuthControlViewModel>();
            containerRegistry.Register<IntegrateInviteAuthActiveControl>();
            containerRegistry.Register<IntegrateInviteAuthActiveControlViewModel>();
            containerRegistry.Register<IntegrateInviteAuthSearchControl>();
            containerRegistry.Register<IntegrateInviteAuthSearchControlViewModel>();
            containerRegistry.Register<IntegrateCompanySearchSelectControl>();
            containerRegistry.Register<IntegrateCompanySearchSelectControlViewModel>();

            //注册日记
            containerRegistry.RegisterInstance<ILogger<IntegrateIdentityAuthControl>>(new Log4gHelper<IntegrateIdentityAuthControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateIdentityAuthControlViewModel>>(new Log4gHelper<IntegrateIdentityAuthControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateIdentityAuthActiveControl>>(new Log4gHelper<IntegrateIdentityAuthActiveControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateIdentityAuthActiveControlViewModel>>(new Log4gHelper<IntegrateIdentityAuthActiveControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateIdentityAuthSearchControl>>(new Log4gHelper<IntegrateIdentityAuthSearchControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateIdentityAuthSearchControlViewModel>>(new Log4gHelper<IntegrateIdentityAuthSearchControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateInviteAuthControl>>(new Log4gHelper<IntegrateInviteAuthControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateInviteAuthControlViewModel>>(new Log4gHelper<IntegrateInviteAuthControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateInviteAuthActiveControl>>(new Log4gHelper<IntegrateInviteAuthActiveControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateInviteAuthActiveControlViewModel>>(new Log4gHelper<IntegrateInviteAuthActiveControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateInviteAuthSearchControl>>(new Log4gHelper<IntegrateInviteAuthSearchControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateInviteAuthSearchControlViewModel>>(new Log4gHelper<IntegrateInviteAuthSearchControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanySearchSelectControl>>(new Log4gHelper<IntegrateCompanySearchSelectControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateCompanySearchSelectControlViewModel>>(new Log4gHelper<IntegrateCompanySearchSelectControlViewModel>());


            containerRegistry.Register<IntegrateRegisterControl>();
            containerRegistry.Register<IntegrateRegisterControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<IntegrateRegisterControl>>(new Log4gHelper<IntegrateRegisterControl>());
            containerRegistry.RegisterInstance<ILogger<IntegrateRegisterControlViewModel>>(new Log4gHelper<IntegrateRegisterControlViewModel>());

            containerRegistry.Register<RegisterPage>();
            containerRegistry.Register<RegisterPageViewModel>();
            containerRegistry.RegisterInstance<ILogger<RegisterPage>>(new Log4gHelper<RegisterPage>());
            containerRegistry.RegisterInstance<ILogger<RegisterPageViewModel>>(new Log4gHelper<RegisterPageViewModel>());

            containerRegistry.Register<AddAccompanyCardWindow>();
            containerRegistry.Register<AddAccompanyCardWindowViewModel>();
            containerRegistry.RegisterInstance<ILogger<AddAccompanyCardWindow>>(new Log4gHelper<AddAccompanyCardWindow>());
            containerRegistry.RegisterInstance<ILogger<AddAccompanyCardWindowViewModel>>(new Log4gHelper<AddAccompanyCardWindowViewModel>());


            containerRegistry.Register<AddAccompanyPhotoWindow>();
            containerRegistry.Register<AddAccompanyPhotoWindowViewModel>();
            containerRegistry.RegisterInstance<ILogger<AddAccompanyPhotoWindow>>(new Log4gHelper<AddAccompanyPhotoWindow>());
            containerRegistry.RegisterInstance<ILogger<AddAccompanyPhotoWindowViewModel>>(new Log4gHelper<AddAccompanyPhotoWindowViewModel>());

            containerRegistry.Register<IntegrateAccompanyVisitorControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<IntegrateAccompanyVisitorControlViewModel>>(new Log4gHelper<IntegrateAccompanyVisitorControlViewModel>());

            containerRegistry.Register<VisitorBlacklistViewModel>();
            containerRegistry.Register<IntegrateSuccessWindow>();
            //操作

            //依赖注入视图 

            //日记
            //var container = ((UnityContainerExtension)Container);
            //foreach (var item in container.Instance.Registrations)
            //{
            //    var logObj = Container.Resolve(item.MappedToType);
            //    containerRegistry.RegisterInstance(Log4gHelper.CreateType(logObj));
            //}

            InitData();
        }

    }
}

//public partial class App : Application
//{
//    private ILogger _logger;

//    public App()
//    {
//        InitializeComponent();

//        //全局异常
//        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
//        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
//        //TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

//        //更新数据库
//        DbUpdateHelper.InitDbAsync().Wait();
//    }

//    protected override void OnStartup(StartupEventArgs e)
//    {
//        base.OnStartup(e);


//        InjectService();
//    }

//    /// <summary>
//    /// 初始化ICO
//    /// </summary>
//    private void InjectService()
//    {
//        //服务注入
//        DependencyService.StartServices((services) =>
//        {
//            services.Configure<AppSettings>(DependencyService.Configuration.GetSection("AppSettings").Bind);

//            //// 配置本地数据库，设置绝对路径，避免Api生成目录在当前项目下与发布版本目录不一致 
//            //string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
//            //services.AddDbContext<SqliteContext>(options => options.UseSqlite(@"Data Source=" + dbPath, p => p.MigrationsAssembly("KT.Visitor.Interface")));
//            services.AddDbContext<SqliteContext>();
//            //公用组件
//            services.AddLogging(configure =>
//            {
//                configure.AddLog4Net();
//            });
//            //后台Api接口
//            services.AddBackendApis();

//            //Local Data
//            services.AddScoped<ILoginUserDataDao, LoginUserDataDao>();
//            services.AddScoped<ISystemConfigDataDao, SystemConfigDataDao>();
//            services.AddScoped<ILoginUserDataService, LoginUserDataService>();
//            services.AddScoped<ISystemConfigDataService, SystemConfigDataService>();

//            //Device
//            services.AddTransient<IReader, Reader>();
//            services.AddTransient<HD900>();
//            services.AddTransient<CVR100U>();
//            services.AddTransient<CVR100XG>();
//            services.AddTransient<THPR210>();
//            services.AddTransient<FS531>();
//            services.AddTransient<Development>();

//            //静态工具 
//            services.AddSingleton<ConfigHelper>();
//            services.AddSingleton<MainFrameHelper>();
//            services.AddSingleton<SmallTicketOperator>();
//            services.AddSingleton<ReaderFactory>();

//            //控件与视频  services.AddTransient<>();
//            services.AddTransient<FrontMainWindow>();
//            services.AddTransient<MainWindowViewModel>();
//            services.AddTransient<VisitorRegisterPageViewModel>();
//            services.AddTransient<VisitorRegisterPage>();
//            services.AddTransient<VisitorRecordListPageViewModel>();
//            services.AddTransient<VisitorRecordListPage>();
//            services.AddTransient<VisitorDetailPage>();
//            services.AddTransient<VisitorDynamicDetailControl>();
//            services.AddTransient<TakePictureControlViewModel>();
//            services.AddTransient<TakePictureControl>();
//            services.AddTransient<CompanyWarnControl>();
//            services.AddTransient<CompanyShowDetailControlViewModel>();
//            services.AddTransient<CompanyShowDetailControl>();
//            services.AddTransient<CompanySelectListControlViewModel>();
//            services.AddTransient<CompanySelectListControl>();

//            services.AddTransient<AssistantVisitorControlViewModel>();
//            services.AddTransient<AssistantVisitorControl>();
//            services.AddTransient<AccompanyVisitorControlViewModel>();
//            services.AddTransient<AccompanyVisitorControl>();
//            services.AddTransient<SystemLoginWindow>();
//            services.AddTransient<LoginWindowViewModel>();
//            services.AddTransient<LoginWindow>();
//            services.AddTransient<ConfigSetting>();
//            services.AddTransient<NotifyPage>();
//            services.AddTransient<NotifyPageViewModel>();
//            services.AddTransient<SystemSettingWindow>();
//            services.AddTransient<SystemSettingWindowViewModel>();
//            services.AddTransient<CompanyTreeCheckHelper>();
//            services.AddTransient<VistitorConfigHelper>();
//            services.AddTransient<SuccessPage>();
//            services.AddTransient<NavPageControlViewModel>();
//            services.AddTransient<NavPageControl>();
//            services.AddTransient<GenderControl>();
//            services.AddTransient<CameraTakePhotoWindow>();
//            services.AddTransient<VisitorBlacklist>();
//            services.AddTransient<PullBlackConfirmWindowViewModel>();
//            services.AddTransient<PullBlackConfirmWindow>();
//            services.AddTransient<AddBlacklistPageViewModel>();
//            services.AddTransient<AddBlacklistPage>();
//            services.AddTransient<ReadIdCardPage>();
//            services.AddTransient<InviteAuthActivePageViewModel>();
//            services.AddTransient<InviteAuthActivePage>();
//            services.AddTransient<IdentityAuthPage>();
//            services.AddTransient<IdentityAuthActivePageViewModel>();
//            services.AddTransient<IdentityAuthActivePage>();
//            services.AddTransient<AuthMsgPage>();
//            services.AddTransient<AppointAuthDetailControl>();
//            services.AddTransient<VisitorInfoViewModel>();
//            services.AddTransient<TreeCheckViewModel>();
//            services.AddTransient<NavPageViewModel>();
//            services.AddTransient<ItemsCheckViewModel>();
//            services.AddTransient<CompanyViewModel>();
//            services.AddTransient<ComboViewModel>();
//            services.AddTransient<CaptureImageViewModel>();
//            services.AddTransient<AuthorizeTimeLimitViewModel>();
//            services.AddTransient<QRCodeHelper>();
//            services.AddTransient<CompanyShowSelectControl>();
//            services.AddTransient<CompanyShowSelectControlViewModel>();
//            services.AddTransient<CompanySearchSelectControl>();
//            services.AddTransient<CompanySearchSelectControlViewModel>();
//            services.AddTransient<CompanySelectControl>();
//            services.AddTransient<CompanySelectControlViewModel>();
//            services.AddTransient<MainBarInfoViewModel>();
//            services.AddTransient<VisitorsControl>();
//            services.AddTransient<VisitorsControlViewModel>();

//            // 一体机
//            services.AddTransient<IntegrateMainWindow>();
//            services.AddTransient<IntegrateMainWindowViewModel>();
//            services.AddTransient<IntegrateLoginWindow>();
//            services.AddTransient<IntegrateSystemSettingWindow>();
//            services.AddTransient<IntegrateCompanyControl>();
//            services.AddTransient<IntegrateCompanyControlViewModel>();
//            services.AddTransient<IntegrateAccompanyVisitorsWindow>();
//            services.AddTransient<IntegrateAccompanyVisitorsWindowViewModel>();
//            services.AddTransient<IntegrateAuthModeControl>();
//            services.AddTransient<IntegrateCompanyShowDetailControl>();
//            services.AddTransient<IntegrateCompanyShowDetailControlViewModel>();
//            services.AddTransient<IntegrateVisitorControl>();
//            services.AddTransient<IntegrateVisitorControlViewModel>();
//            services.AddTransient<InterateVisitorRegisterControl>();
//            services.AddTransient<InterateVisitorRegisterControlViewModel>();
//            services.AddTransient<IntegrateCompanySelectListControl>();
//            services.AddTransient<IntegrateAuthModeControlViewModel>();
//            services.AddTransient<IntegrateAccompanyVisitorControl>();
//            services.AddTransient<IntegrateGenderControl>();
//            //操作


//            //依赖注入视图
//        });

//        //当前类日记记录
//        _logger = DependencyService.ServiceProvider.GetRequiredService<ILogger<App>>();

//        ////更新本地数据库
//        //var dbContext = DependencyService.ServiceProvider.GetRequiredService<SqliteContext>();
//        //dbContext.Database.Migrate();

//        _logger.LogInformation("Database Inited!");

//        //更新本地配置
//        var configHelper = DependencyService.ServiceProvider.GetRequiredService<ConfigHelper>();
//        configHelper.Refresh();

//        //启动主窗口
//        //this.FrontMainWindow = DependencyService.ServiceProvider.GetRequiredService<FrontMainWindow>();
//        this.FrontMainWindow = DependencyService.ServiceProvider.GetRequiredService<IntegrateLoginWindow>();
//        this.FrontMainWindow.Show();
//    }

//    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
//    {
//        _logger?.LogError("App_DispatcherUnhandledException Error:{0} ", JsonConvert.SerializeObject(e, JsonUtil.JsonSettings));
//        MessageWarnBox.Show(e.Exception.Message + e.Exception.InnerException?.Message);
//        e.Handled = true;
//    }
//    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
//    {
//        _logger?.LogError("CurrentDomain_UnhandledException Error:{0} ", JsonConvert.SerializeObject(e, JsonUtil.JsonSettings));
//        var ex = (e.ExceptionObject as Exception);
//        MessageWarnBox.Show(ex?.Message + ex.InnerException?.Message);
//    }
//    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
//    {
//        _logger?.LogError("App_DispatcherUnhandledException Error:{0} ", JsonConvert.SerializeObject(e, JsonUtil.JsonSettings));
//        MessageWarnBox.Show(e.Exception.Message + e.Exception.InnerException?.Message);
//        //将异常标识为已经观察到 
//        e.SetObserved();
//    }
//}
//}