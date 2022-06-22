
using HelperTools;
using KT.Common.Core.Utils;
using KT.Common.Netty.Common;
using KT.Common.Netty.Servers;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Device;
using KT.Device.Unit;
using KT.Device.Unit.CardReaders.Models;
using KT.Quanta.Unit.Common.Helpers;
using KT.Turnstile.Unit.ClientApp.Dao.Daos;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.ClientApp.Device;
using KT.Turnstile.Unit.ClientApp.Device.Common;
using KT.Turnstile.Unit.ClientApp.Device.Handlers;
using KT.Turnstile.Unit.ClientApp.Server;
using KT.Turnstile.Unit.ClientApp.Service.Helpers;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using KT.Turnstile.Unit.ClientApp.Service.Services;
using KT.Turnstile.Unit.ClientApp.Service.Updates;
using KT.Turnstile.Unit.ClientApp.Views;
using KT.Turnstile.Unit.ClientApp.WsScoket;
using KT.Turnstile.Unit.Entity.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

//using System.Web.Http.SelfHost;

namespace KT.Turnstile.Unit.ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static ILoggerFactory AppLoggerFactory =
       LoggerFactory.Create(buliidder =>
       {
           buliidder.AddLog4Net();
       });
        private ILogger _logger;
        
        public App()
        {
            //_logger = new Log4gHelper<App>();
            _logger = AppLoggerFactory.CreateLogger("Elevator Log");

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Turnstile.Unit.ClientApp", out bool createNew);

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
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            _logger?.LogError($"CurrentDomain_UnhandledException Error：{JsonConvert.SerializeObject(ex, JsonUtil.JsonPrintSettings)} ");
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger?.LogError($"TaskScheduler_UnobservedTaskException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");
            e.SetObserved();
        }

        private async void InitDataAsync()
        {
            //版本校验
            var _JobChkVer = Container.Resolve<JobChkVer>();
            _JobChkVer.Init();
            //更新数据库
            await DbUpdateHelper.InitDbAsync();
          
            //初始化读卡器设备
            await Container.InitializeDeviceUnitAsync();

            //更新配置
            var configHelper = Container.Resolve<ConfigHelper>();
            configHelper.Refresh();

            //开启读卡器数据接收
            var cardReceiveHanderInstance = Container.Resolve<CardReceiveHanderInstance>();
            await cardReceiveHanderInstance.StartAsync();

            //开启服务器连接
            var hubHelper = Container.Resolve<HubHelper>();
            hubHelper.StartAsync();

            //初始化Netty服务 
            var appSettings = Container.Resolve<AppSettings>();
            var quantaServerHost = Container.Resolve<IQuantaServerHost>();
            quantaServerHost.RunAsync(appSettings.Port + 1);

            //心跳
            var heartbeatHelper = Container.Resolve<HeartbeatHelper>();
            heartbeatHelper.StartAsync();

            //初始化读卡器设备
            var serialDeviceDeviceService = Container.Resolve<ISerialDeviceService>();
            serialDeviceDeviceService.InitAllCardDeviceAsync();

            //系统时间同步
            var systemTimeHelper = ContainerHelper.Resolve<SystemTimeHelper>();
            await systemTimeHelper.StartAsync(async () =>
            {
                var hubHelper = ContainerHelper.Resolve<HubHelper>();
                return await hubHelper.MasterHub.InvokeAsync<long>("GetUtcMillis");
            });

            //文件清除
            var cleanFileHelper = Container.Resolve<CleanFileHelper>();
            var cleanFileSettings = Container.Resolve<CleanFileSettings>();
            await cleanFileHelper.StartAsync(cleanFileSettings);
  
        }
   
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
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
            containerRegistry.RegisterInstance(Container);
            ContainerHelper.SetProvider(_logger, Container);

            //确定值的类
            containerRegistry.RegisterInstance(_logger);
            containerRegistry.RegisterInstance(AppLoggerFactory);
            //静态工具
            containerRegistry.RegisterSingleton<ConfigHelper>();
            containerRegistry.RegisterSingleton<HubHelper>();
            containerRegistry.RegisterSingleton<HeartbeatHelper>();

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var cardDeviceSettings = PrismDependency.GetConfiguration().GetSection("CardDeviceSettings").Get<CardDeviceSettings>();
            containerRegistry.RegisterInstance<CardDeviceSettings>(cardDeviceSettings);
            var elevatorDisplayDeviceSettings = PrismDependency.GetConfiguration()
                .GetSection("ElevatorDisplayDeviceSettings")
                .Get<ElevatorDisplayDeviceSettings>();
            containerRegistry.RegisterInstance(elevatorDisplayDeviceSettings);
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            //containerRegistry.Register<SqliteContext>();

            //泛型日记
            containerRegistry.RegisterInstance<ILogger<QuantaServerHost>>(new Log4gHelper<QuantaServerHost>());

            //本地数据
            containerRegistry.Register<ISystemConfigDao, SystemConfigDao>();
            containerRegistry.Register<ISystemConfigService, SystemConfigService>();
            containerRegistry.Register<ICardDeviceDao, CardDeviceDao>();
            containerRegistry.Register<ICardDeviceService, CardDeviceService>();
            containerRegistry.Register<IRightGroupDao, RightGroupDao>();
            containerRegistry.Register<IRightGroupService, RightGroupService>();
            containerRegistry.Register<IPassRightDao, PassRightDao>();
            containerRegistry.Register<IPassRightService, PassRightService>();
            containerRegistry.Register<IPassRecordService, PassRecordService>();
            containerRegistry.Register<IPassRecordDao, PassRecordDao>();
            containerRegistry.Register<IErrorService, ErrorService>();

            containerRegistry.RegisterSingleton<CardReceiveHanderInstance>();
            containerRegistry.Register<ISerialDeviceService, SerialDeviceService>();
            containerRegistry.RegisterSingleton<SocketDeviceList>();
            containerRegistry.RegisterSingleton<SocketDeviceFactory>();

            containerRegistry.RegisterSingleton<IDeviceList, DeviceList>();
            containerRegistry.Register<QuantaServerFrameEncoder>();
            containerRegistry.Register<QuantaServerFrameDecoder>();
            containerRegistry.Register<IQuantaServerHost, QuantaServerHost>();
            containerRegistry.Register<IQuantaServerHandler, QuantaServerHandler>();
            containerRegistry.Register<IQuantaNettyActionManager, QuantaNettyActionManager>();

            containerRegistry.RegisterInstance<ILogger<DeviceList>>(new Log4gHelper<DeviceList>());
            containerRegistry.RegisterInstance<ILogger<QuantaServerFrameEncoder>>(new Log4gHelper<QuantaServerFrameEncoder>());
            containerRegistry.RegisterInstance<ILogger<QuantaServerFrameDecoder>>(new Log4gHelper<QuantaServerFrameDecoder>());
            containerRegistry.RegisterInstance<ILogger<QuantaServerHost>>(new Log4gHelper<QuantaServerHost>());
            containerRegistry.RegisterInstance<ILogger<QuantaServerHandler>>(new Log4gHelper<QuantaServerHandler>());
            containerRegistry.RegisterInstance<ILogger<QuantaNettyActionManager>>(new Log4gHelper<QuantaNettyActionManager>());

            containerRegistry.RegisterSingleton<ElevatorDisplayDeviceClient>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());

            //初始化读卡器设备注入
            containerRegistry.RegisterDeviceUnitTypes();

            //2021-10-30  ws socket 数据通道

            //串口初始化注入 日立
            containerRegistry.RegisterSingleton<ScommModel>();
            containerRegistry.RegisterSingleton<Dispatch>(); //派梯方法
            containerRegistry.RegisterSingleton<JobChkVer>(); //版本检验
            containerRegistry.RegisterSingleton<WebSocket4net>(); //版本检验
            // 初始化数据
            InitDataAsync();
        }
    }
}
