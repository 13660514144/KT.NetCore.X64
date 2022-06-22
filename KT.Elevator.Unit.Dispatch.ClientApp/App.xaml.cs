using KT.Common.Core.Utils;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Device;
using KT.Device.Unit;
using KT.Device.Unit.Devices;
using KT.Elevator.Unit.Dispatch.ClientApp.Dao.Daos;
using KT.Elevator.Unit.Dispatch.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Dispatch.ClientApp.Device.Handlers;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.IServices;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Network;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Services;
using KT.Elevator.Unit.Dispatch.ClientApp.Updates;
using KT.Elevator.Unit.Dispatch.ClientApp.Views;
using KT.Quanta.Unit.Common.Helpers;
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

namespace KT.Elevator.Unit.Dispatch.ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Prism.Unity.PrismApplication
    {
        public static ILoggerFactory AppLoggerFactory =
            LoggerFactory.Create(bulidder =>
            {
                bulidder.AddLog4Net();
            });

        private ILogger _logger;
        public App()
        {
            _logger = AppLoggerFactory.CreateLogger("Elevator Log");

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Elevator.Unit.Dispatch.ClientApp", out bool createNew);

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
            //更新数据库
            await DbUpdateHelper.InitDbAsync();

            //初始化读卡器设备
            await Container.InitializeDeviceUnitAsync();

            //更新配置
            var configHelper = Container.Resolve<ConfigHelper>();
            configHelper.Refresh();

            //开启读卡器数据接收
            var cardReceiveHanderInstance = Container.Resolve<HitachiDeviceInstance>();
            cardReceiveHanderInstance.StartAsync();

            //开启服务器连接
            var hubHelper = Container.Resolve<HubHelper>();
            hubHelper.StartAsync();

            //心跳
            var heartbeatHelper = Container.Resolve<HeartbeatHelper>();
            heartbeatHelper.StartAsync();

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

            //日记
            containerRegistry.RegisterInstance(_logger);
            containerRegistry.RegisterInstance(AppLoggerFactory);

            //静态工具
            containerRegistry.RegisterSingleton<ConfigHelper>();
            containerRegistry.RegisterSingleton<HubHelper>();
            containerRegistry.RegisterSingleton<HeartbeatHelper>();

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var hitachiSettings = PrismDependency.GetConfiguration().GetSection("HitachiSettings").Get<HitachiSettings>();
            containerRegistry.RegisterInstance(hitachiSettings);
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            //本地数据
            containerRegistry.Register<ISystemConfigDao, SystemConfigDao>();
            containerRegistry.Register<ISystemConfigService, SystemConfigService>();

            containerRegistry.RegisterSingleton<HitachiDeviceInstance>();
            containerRegistry.RegisterSingleton<IDeviceList, DeviceList>();
            containerRegistry.Register<INettyServerHost, NettyServerHost>();
            containerRegistry.Register<NettyServerHandler>();

            containerRegistry.RegisterSingleton<HitachiDeviceClient>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());

            //初始化读卡器设备注入
            containerRegistry.RegisterDeviceUnitTypes();

            // 初始化数据
            InitDataAsync();
        }
    }
}
