using ArcFaceSDK;
using ArcFaceSDK.Mothed;
using ArcSoftFace.Entity;
using HelperTools;
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
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.IdReader.SDK;
using KT.Visitor.IntegrateApp.Helpers;
using KT.Visitor.IntegrateApp.Views;
using KT.Visitor.IntegrateApp.Views.Register;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
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
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//64
//using KT.Unit.FaceRecognition.Models;
//using KT.Visitor.Common.Tools.FacePro;
//64
namespace KT.Visitor.IntegrateApp
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
            new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Visitor.IntegrateApp", out bool createNew);

            if (!createNew)
            {
                MessageBox.Show("已经存在运行的应用程序，不能同时运行多个应用程序！");
                App.Current.Shutdown();
                Environment.Exit(0);
            }
            try
            {
                //去除文件夹和子文件的只读属性
                //去除文件夹的只读属性
                String strRunPath = System.Windows.Forms.Application.StartupPath;
                String file = strRunPath + @"\\Files\\Images\\Portraits";
                //System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
                //fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;

                //去除文件的只读属性
                //System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);

                DirectoryInfo dir = new DirectoryInfo(file);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }

            }
            catch (Exception ex) // 异常处理
            {
                Console.WriteLine(ex.Message.ToString());// 异常信息
            }
            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger?.LogError($"App_DispatcherUnhandledException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            var exception = e.Exception.GetInner();

            Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);

            //发布错误事件
            _eventAggregator.GetEvent<ExceptionEvent>().Publish(exception);

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

        private async Task InitDataAsync()
        {
            //JobChkVer _JobChkVer = new JobChkVer();
            //_JobChkVer.Init();
            //更新数据库
            await DbUpdateHelper.InitDbAsync();

            //更新配置
            var configHelper = Container.Resolve<ConfigHelper>();
            _ = configHelper.RefreshAsync();

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

            //MemoryCache
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            containerRegistry.RegisterInstance<IMemoryCache>(memoryCache);

            //重名内容 
            containerRegistry.Register<SystemSettingWindow>();

            //弹窗
            containerRegistry.RegisterDialog<ContentConfirmWindow, ContentConfirmWindowViewModel>();

            containerRegistry.Register<MessageWarnBox>();
            containerRegistry.RegisterSingleton<DialogHelper>();

            containerRegistry.Register<MessageWarnBox>();
            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var idReaderSettings = PrismDependency.GetConfiguration().GetSection("IdReaderSettings").Get<IdReaderSettings>();
            containerRegistry.RegisterInstance(idReaderSettings);
            //x86
            var arcFaceSettings = PrismDependency.GetConfiguration().GetSection("ArcFaceSettings").Get<ArcFaceSettings>();
            containerRegistry.RegisterInstance(arcFaceSettings);
            //x86
            //X64 for ever

            var faceRecognitionAppSettings = PrismDependency.GetConfiguration().GetSection("FaceRecognitionAppSettings").Get<FaceRecognitionAppSettings>();
            containerRegistry.RegisterInstance(faceRecognitionAppSettings);
            containerRegistry.RegisterSingleton<FaceEngine>();
            containerRegistry.RegisterSingleton<FaceCheckMethod>();
            /*containerRegistry.Register<IFaceService, FaceService>();

            containerRegistry.RegisterSingleton<FaceFactory>();
            containerRegistry.Register<IFaceProvider, FaceProvider>();*/

            //X64 for ever
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
            containerRegistry.RegisterInstance<ILogger<FS531X>>(new Log4gHelper<FS531X>());
            containerRegistry.RegisterInstance<ILogger<Development>>(new Log4gHelper<Development>());
            containerRegistry.RegisterInstance<ILogger<ConfigHelper>>(new Log4gHelper<ConfigHelper>());
            containerRegistry.RegisterInstance<ILogger<MainFrameHelper>>(new Log4gHelper<MainFrameHelper>());
            containerRegistry.RegisterInstance<ILogger<SmallTicketOperator>>(new Log4gHelper<SmallTicketOperator>());
            containerRegistry.RegisterInstance<ILogger<ReaderFactory>>(new Log4gHelper<ReaderFactory>());
            containerRegistry.RegisterInstance<ILogger<MainWindowViewModel>>(new Log4gHelper<MainWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<ShowTakePictureControlViewModel>>(new Log4gHelper<ShowTakePictureControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyWarnControl>>(new Log4gHelper<CompanyWarnControl>());
            containerRegistry.RegisterInstance<ILogger<SystemLoginWindow>>(new Log4gHelper<SystemLoginWindow>());
            containerRegistry.RegisterInstance<ILogger<LoginWindowViewModel>>(new Log4gHelper<LoginWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<LoginWindow>>(new Log4gHelper<LoginWindow>());
            //containerRegistry.RegisterInstance<ILogger<ConfigSetting>>(new Log4gHelper<ConfigSetting>());
            //containerRegistry.RegisterInstance<ILogger<NotifyPage>>(new Log4gHelper<NotifyPage>());
            //containerRegistry.RegisterInstance<ILogger<NotifyPageViewModel>>(new Log4gHelper<NotifyPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindow>>(new Log4gHelper<SystemSettingWindow>());
            containerRegistry.RegisterInstance<ILogger<SystemSettingWindowViewModel>>(new Log4gHelper<SystemSettingWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyTreeCheckHelper>>(new Log4gHelper<CompanyTreeCheckHelper>());
            containerRegistry.RegisterInstance<ILogger<VistitorConfigHelper>>(new Log4gHelper<VistitorConfigHelper>());
            containerRegistry.RegisterInstance<ILogger<NavPageControlViewModel>>(new Log4gHelper<NavPageControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<NavPageControl>>(new Log4gHelper<NavPageControl>());
            containerRegistry.RegisterInstance<ILogger<GenderControl>>(new Log4gHelper<GenderControl>());
            containerRegistry.RegisterInstance<ILogger<CameraTakePhotoWindow>>(new Log4gHelper<CameraTakePhotoWindow>());
            containerRegistry.RegisterInstance<ILogger<RegistVisitorViewModel>>(new Log4gHelper<RegistVisitorViewModel>());
            containerRegistry.RegisterInstance<ILogger<TreeCheckCompanyViewModel>>(new Log4gHelper<TreeCheckCompanyViewModel>());
            containerRegistry.RegisterInstance<ILogger<NavPageViewModel>>(new Log4gHelper<NavPageViewModel>());
            containerRegistry.RegisterInstance<ILogger<ItemsCheckViewModel>>(new Log4gHelper<ItemsCheckViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanyViewModel>>(new Log4gHelper<CompanyViewModel>());
            containerRegistry.RegisterInstance<ILogger<CaptureImageViewModel>>(new Log4gHelper<CaptureImageViewModel>());
            containerRegistry.RegisterInstance<ILogger<AuthorizeTimeLimitViewModel>>(new Log4gHelper<AuthorizeTimeLimitViewModel>());
            containerRegistry.RegisterInstance<ILogger<MainWindow>>(new Log4gHelper<MainWindow>());
            containerRegistry.RegisterInstance<ILogger<MainWindowViewModel>>(new Log4gHelper<MainWindowViewModel>());
            containerRegistry.RegisterInstance<ILogger<LoginWindow>>(new Log4gHelper<LoginWindow>());
            containerRegistry.RegisterInstance<ILogger<CompanyControl>>(new Log4gHelper<CompanyControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyControlViewModel>>(new Log4gHelper<CompanyControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorsWindow>>(new Log4gHelper<AccompanyVisitorsWindow>());
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorsWindowViewModel>>(new Log4gHelper<AccompanyVisitorsWindowViewModel>());
            //containerRegistry.RegisterInstance<ILogger<AuthModeControl>>(new Log4gHelper<AuthModeControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowDetailControl>>(new Log4gHelper<CompanyShowDetailControl>());
            containerRegistry.RegisterInstance<ILogger<CompanyShowDetailControlViewModel>>(new Log4gHelper<CompanyShowDetailControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorInputControl>>(new Log4gHelper<VisitorInputControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorInputControlViewModel>>(new Log4gHelper<VisitorInputControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<VisitorRegisterControl>>(new Log4gHelper<VisitorRegisterControl>());
            containerRegistry.RegisterInstance<ILogger<VisitorRegisterControlViewModel>>(new Log4gHelper<VisitorRegisterControlViewModel>());
            containerRegistry.RegisterInstance<ILogger<CompanySelectListControl>>(new Log4gHelper<CompanySelectListControl>());
            //containerRegistry.RegisterInstance<ILogger<AuthModeControlViewModel>>(new Log4gHelper<AuthModeControlViewModel>());
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
            //containerRegistry.Register<HD900>();
            //containerRegistry.Register<CVR100U>();
            //containerRegistry.Register<CVR100XG>();
            //containerRegistry.Register<THPR210>();
            //containerRegistry.Register<FS531>();
            //containerRegistry.Register<Development>();

            //静态工具 
            containerRegistry.RegisterSingleton<ConfigHelper>();
            containerRegistry.RegisterSingleton<MainFrameHelper>();
            containerRegistry.RegisterSingleton<ReaderFactory>();
            containerRegistry.RegisterSingleton<PrintSourceProvider>();
            //containerRegistry.Register<SmallTicketOperator>();
            //containerRegistry.Register<PrintHandler>();
            //containerRegistry.Register<VisitQrCodeRenderer>();

            ////控件与视频 
            //containerRegistry.Register<FrontMainWindow>();
            //containerRegistry.Register<MainWindowViewModel>();
            //containerRegistry.Register<VisitorRecordListPageViewModel>();
            //containerRegistry.Register<VisitorRecordListPage>();
            //containerRegistry.Register<VisitorDetailControl>();
            //containerRegistry.Register<VisitorDynamicDetailControl>();
            //containerRegistry.Register<ShowTakePictureControlViewModel>();
            //containerRegistry.Register<CompanyWarnControl>();

            //containerRegistry.Register<AssistantVisitorControlViewModel>();
            //containerRegistry.Register<AssistantVisitorControl>();
            //containerRegistry.Register<SystemLoginWindow>();
            //containerRegistry.Register<LoginWindowViewModel>();
            //containerRegistry.Register<LoginWindow>();
            //containerRegistry.Register<ConfigSetting>();
            //containerRegistry.Register<NotifyPage>();
            //containerRegistry.Register<NotifyPageViewModel>();
            //containerRegistry.Register<SystemSettingWindow>();
            //containerRegistry.Register<SystemSettingWindowViewModel>();
            //containerRegistry.Register<CompanyTreeCheckHelper>();
            //containerRegistry.Register<VistitorConfigHelper>();
            //containerRegistry.Register<NavPageControlViewModel>();
            //containerRegistry.Register<NavPageControl>();
            //containerRegistry.Register<GenderControl>();
            //containerRegistry.Register<CameraTakePhotoWindow>();
            //containerRegistry.Register<VisitorBlacklist>();
            //containerRegistry.Register<PullBlackConfirmWindowViewModel>();
            //containerRegistry.Register<PullBlackConfirmWindow>();
            //containerRegistry.Register<AddBlacklistPageViewModel>();
            //containerRegistry.Register<AddBlacklistControl>();
            //containerRegistry.Register<RegistVisitorViewModel>();
            //containerRegistry.Register<TreeCheckViewModel>();
            //containerRegistry.Register<NavPageViewModel>();
            //containerRegistry.Register<ItemsCheckViewModel>();
            //containerRegistry.Register<CompanyViewModel>();
            //containerRegistry.Register<CaptureImageViewModel>();
            //containerRegistry.Register<AuthorizeTimeLimitViewModel>();
            //containerRegistry.Register<MainBarInfoViewModel>();

            //// 一体机
            //containerRegistry.Register<MainWindow>();
            //containerRegistry.Register<MainWindowViewModel>();
            //containerRegistry.Register<LoginWindow>();
            //containerRegistry.Register<SystemSettingWindow>();
            //containerRegistry.Register<CompanyControl>();
            //containerRegistry.Register<CompanyControlViewModel>();
            //containerRegistry.Register<AccompanyVisitorsWindow>();
            //containerRegistry.Register<AccompanyVisitorsWindowViewModel>();
            //containerRegistry.Register<AuthModeControl>();
            //containerRegistry.Register<CompanyShowDetailControl>();
            //containerRegistry.Register<CompanyShowDetailControlViewModel>();
            //containerRegistry.Register<VisitorInputControl>();
            //containerRegistry.Register<VisitorInputControlViewModel>();
            //containerRegistry.Register<VisitorRegisterControl>();
            //containerRegistry.Register<VisitorRegisterControlViewModel>();
            //containerRegistry.Register<CompanySelectListControl>();
            //containerRegistry.Register<AuthModeControlViewModel>();
            //containerRegistry.Register<AccompanyVisitorControl>();
            //containerRegistry.Register<IdentityAuthControl>();
            //containerRegistry.Register<IdentityAuthControlViewModel>();
            //containerRegistry.Register<IdentityAuthActiveControl>();
            //containerRegistry.Register<IdentityAuthActiveControlViewModel>();
            //containerRegistry.Register<IdentityAuthSearchControl>();
            //containerRegistry.Register<IdentityAuthSearchControlViewModel>();
            //containerRegistry.Register<InviteAuthControl>();
            //containerRegistry.Register<InviteAuthControlViewModel>();
            //containerRegistry.Register<InviteAuthActiveControl>();
            //containerRegistry.Register<InviteAuthActiveControlViewModel>();
            //containerRegistry.Register<InviteAuthSearchControl>();
            //containerRegistry.Register<InviteAuthSearchControlViewModel>();
            //containerRegistry.Register<CompanySearchSelectControl>();
            //containerRegistry.Register<CompanySearchSelectControlViewModel>();

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
            containerRegistry.RegisterInstance<ILogger<RegisterHomeControl>>(new Log4gHelper<RegisterHomeControl>());
            containerRegistry.RegisterInstance<ILogger<RegisterHomeControlViewModel>>(new Log4gHelper<RegisterHomeControlViewModel>());

            //containerRegistry.Register<RegisterPage>();
            //containerRegistry.Register<RegisterPageViewModel>();
            //containerRegistry.RegisterInstance<ILogger<RegisterPage>>(new Log4gHelper<RegisterPage>());
            //containerRegistry.RegisterInstance<ILogger<RegisterPageViewModel>>(new Log4gHelper<RegisterPageViewModel>());

            //containerRegistry.Register<AddAccompanyCardWindow>();
            //containerRegistry.Register<AddAccompanyCardWindowViewModel>();
            containerRegistry.RegisterInstance<ILogger<AddAccompanyCardWindow>>(new Log4gHelper<AddAccompanyCardWindow>());
            containerRegistry.RegisterInstance<ILogger<AddAccompanyCardWindowViewModel>>(new Log4gHelper<AddAccompanyCardWindowViewModel>());


            //containerRegistry.Register<AddAccompanyPhotoWindow>();
            //containerRegistry.Register<AddAccompanyPhotoWindowViewModel>();
            containerRegistry.RegisterInstance<ILogger<AddAccompanyPhotoWindow>>(new Log4gHelper<AddAccompanyPhotoWindow>());
            containerRegistry.RegisterInstance<ILogger<AddAccompanyPhotoWindowViewModel>>(new Log4gHelper<AddAccompanyPhotoWindowViewModel>());

            containerRegistry.Register<AccompanyVisitorControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<AccompanyVisitorControlViewModel>>(new Log4gHelper<AccompanyVisitorControlViewModel>());

            //containerRegistry.Register<SuccessWindow>();

            //containerRegistry.Register<IdentityAuthDetailControl>();
            //containerRegistry.Register<IdentityAuthDetailControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<IdentityAuthDetailControl>>(new Log4gHelper<IdentityAuthDetailControl>());
            containerRegistry.RegisterInstance<ILogger<IdentityAuthDetailControlViewModel>>(new Log4gHelper<IdentityAuthDetailControlViewModel>());

            //containerRegistry.Register<InviteAuthDetailControl>();
            //containerRegistry.Register<InviteAuthDetailControlViewModel>();
            containerRegistry.RegisterInstance<ILogger<InviteAuthDetailControl>>(new Log4gHelper<InviteAuthDetailControl>());
            containerRegistry.RegisterInstance<ILogger<InviteAuthDetailControlViewModel>>(new Log4gHelper<InviteAuthDetailControlViewModel>());
            //操作

            //依赖注入视图 

            //日记
            //var container = ((UnityContainerExtension)Container);
            //foreach (var item in container.Instance.Registrations)
            //{
            //    var logObj = Container.Resolve(item.MappedToType);
            //    containerRegistry.RegisterInstance(Log4gHelper.CreateType(logObj));
            //}

            //X86 人证对比
            containerRegistry.RegisterSingleton<ArcIdSdkHelper>();
            //X86 人证对比

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());

            _ = InitDataAsync();
        }

    }
}
