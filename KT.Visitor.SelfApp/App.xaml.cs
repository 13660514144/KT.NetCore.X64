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
using KT.Visitor.Common.Tools.Printer.DocumentRenderer;
using KT.Visitor.Common.Tools.Printer.Helpers;
using KT.Visitor.Common.Tools.Printer.PrintOperator;
using KT.Visitor.Data.Daos;
using KT.Visitor.Data.IDaos;
using KT.Visitor.Data.IServices;
using KT.Visitor.Data.Services;
using KT.Visitor.Data.Updates;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.SelfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger<App> _logger;
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
            new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Visitor.SelfApp", out bool createNew);

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

            if (exception is CustomException)
            {
                var ex = (CustomException)exception;
                Container.Resolve<OperateErrorPage>().ShowMessage(ex.Message, ex.Title);
            }
            else
            {
                Container.Resolve<OperateErrorPage>().ShowMessage(exception.Message);
            }
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            _logger?.LogError($"CurrentDomain_UnhandledException Error：{JsonConvert.SerializeObject(ex, JsonUtil.JsonPrintSettings)} ");

            var exception = ex.GetInner();
            Dispatcher.Invoke(() =>
            {
                Container.Resolve<OperateErrorPage>().ShowMessage(ex.InnerException?.Message ?? ex?.Message);
            });
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
                    Container.Resolve<OperateErrorPage>().ShowMessage(exception?.Message);
                });
            });
        }

        private async Task InitDataAsync()
        {
            var _JobChkVer= Container.Resolve<JobChkVer>();
            _JobChkVer.Init();
            //更新数据库
            await DbUpdateHelper.InitDbAsync();

            //更新配置
            var configHelper = Container.Resolve<ConfigHelper>();
            await configHelper.RefreshAsync();

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
            var window = Container.Resolve<MainWindow>();
            var dialogHelper = Container.Resolve<DialogHelper>();
            dialogHelper.AddFirst(window);
            return window;
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

            containerRegistry.RegisterSingleton<DialogHelper>();

            //公用组件
            var pageSeting = PrismDependency.GetConfiguration().GetSection("PageShow").Get<PageShow>();
            containerRegistry.RegisterInstance(pageSeting);
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var idReaderSettings = PrismDependency.GetConfiguration().GetSection("IdReaderSettings").Get<IdReaderSettings>();
            containerRegistry.RegisterInstance(idReaderSettings);
            var selfAppSettings = PrismDependency.GetConfiguration().GetSection("SelfAppSettings").Get<SelfAppSettings>();
            containerRegistry.RegisterInstance(selfAppSettings);
            //X86
            //var arcFaceSettings = PrismDependency.GetConfiguration().GetSection("ArcFaceSettings").Get<ArcFaceSettings>();
            //containerRegistry.RegisterInstance(arcFaceSettings);
            //X86
            //X64 for ever
            
            var faceRecognitionAppSettings = PrismDependency.GetConfiguration().GetSection("FaceRecognitionAppSettings").Get<FaceRecognitionAppSettings>();
            containerRegistry.RegisterInstance(faceRecognitionAppSettings);
            containerRegistry.RegisterSingleton<FaceEngine>();
            containerRegistry.RegisterSingleton<FaceCheckMethod>();
            
            //X64 for ever
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            //containerRegistry.Register<SqliteContext>();

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

            //后台Api接口
            containerRegistry.Register<OpenApi>();
            containerRegistry.Register<BackendApiBase>();
            containerRegistry.Register<IBlacklistApi, SelfBlacklistApi>();
            containerRegistry.Register<ICompanyApi, SelfCompanyApi>();
            containerRegistry.Register<IFunctionApi, SelfFunctionApi>();
            containerRegistry.Register<IVisitorApi, SelfVisitorApi>();
            containerRegistry.Register<IUploadImgApi, UploadImgApi>();

            //后台Api接口日记
            containerRegistry.RegisterInstance<ILogger<SelfVisitorApi>>(new Log4gHelper<SelfVisitorApi>());
            containerRegistry.RegisterInstance<ILogger<BackendApiBase>>(new Log4gHelper<BackendApiBase>());
            containerRegistry.RegisterInstance<ILogger<SelfCompanyApi>>(new Log4gHelper<SelfCompanyApi>());
            containerRegistry.RegisterInstance<ILogger<OpenApi>>(new Log4gHelper<OpenApi>());
            containerRegistry.RegisterInstance<ILogger<UploadImgApi>>(new Log4gHelper<UploadImgApi>());
            containerRegistry.RegisterInstance<ILogger<SelfBlacklistApi>>(new Log4gHelper<SelfBlacklistApi>());
            containerRegistry.RegisterInstance<ILogger<SelfFunctionApi>>(new Log4gHelper<SelfFunctionApi>());

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
            containerRegistry.RegisterSingleton<ReaderFactory>();
            containerRegistry.RegisterSingleton<PrintSourceProvider>();
            containerRegistry.Register<SmallTicketOperator>();
            containerRegistry.Register<PrintHandler>();
            containerRegistry.Register<VisitQrCodeRenderer>();
            containerRegistry.RegisterSingleton<ArcIdSdkHelper>();
            containerRegistry.RegisterSingleton<OnlineHeatHelper>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());
            containerRegistry.RegisterSingleton<JobChkVer>();
            _ = InitDataAsync();
        }
    }
}
