using AutoMapper;
using KT.Common.Core.Utils;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Device;
using KT.Device.Unit;
using KT.Device.Unit.CardReaders.Models;
using KT.Elevator.Unit.Processor.ClientApp.Dao.Daos;
using KT.Elevator.Unit.Processor.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Processor.ClientApp.Device.Handlers;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer;
using KT.Elevator.Unit.Processor.ClientApp.Service.Services;
using KT.Elevator.Unit.Processor.ClientApp.Updates;
using KT.Elevator.Unit.Processor.ClientApp.Views;
using KT.Proxy.BackendApi.Apis;
using KT.Quanta.Unit.Common.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Elevator.Unit.Processor.ClientApp
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
            //new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Elevator.Unit.Processor.ClientApp", out bool createNew);

            //if (!createNew)
            //{
            //    MessageBox.Show("已经存在运行的应用程序，不能同时运行多个应用程序！");
            //    App.Current.Shutdown();
            //    Environment.Exit(0);
            //}

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
            var cardReceiveHanderInstance = Container.Resolve<CardReceiveHanderInstance>();
            await cardReceiveHanderInstance.StartAsync();

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
            var cardDeviceSettings = PrismDependency.GetConfiguration().GetSection("CardDeviceSettings").Get<CardDeviceSettings>();
            containerRegistry.RegisterInstance<CardDeviceSettings>(cardDeviceSettings);
            var inputDeviceSettings = PrismDependency.GetConfiguration().GetSection("InputDeviceSettings").Get<InputDeviceSettings>();
            containerRegistry.RegisterInstance(inputDeviceSettings);
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            //本地数据
            containerRegistry.Register<ISystemConfigDao, SystemConfigDao>();
            containerRegistry.Register<ISystemConfigService, SystemConfigService>();
            containerRegistry.Register<ICardDeviceDao, CardDeviceDao>();
            containerRegistry.Register<IFloorDao, FloorDao>();
            containerRegistry.Register<IPassRightDao, PassRightDao>();
            containerRegistry.Register<IPassRecordDao, PassRecordDao>();
            containerRegistry.Register<IHandleElevatorDeviceDao, HandleElevatorDeviceDao>();
            containerRegistry.Register<ICardDeviceService, CardDeviceService>();
            containerRegistry.Register<IPassRightService, PassRightService>();
            containerRegistry.Register<IPassRecordService, PassRecordService>();
            containerRegistry.Register<IErrorService, ErrorService>();
            containerRegistry.Register<IFloorService, FloorService>();
            containerRegistry.Register<IHandleElevatorDeviceService, HandleElevatorDeviceService>();
            containerRegistry.Register<IElevatorGroupDao, ElevatorGroupDao>();
            containerRegistry.Register<IElevatorGroupService, ElevatorGroupService>();
            containerRegistry.Register<KoalaFaceApi>();
            containerRegistry.RegisterInstance<ILogger<KoalaFaceApi>>(new Log4gHelper<KoalaFaceApi>());

            containerRegistry.RegisterSingleton<CardReceiveHanderInstance>();
            containerRegistry.RegisterSingleton<IDeviceList, DeviceList>();
            containerRegistry.Register<ISerialDeviceService, SerialDeviceService>();
            containerRegistry.Register<INettyServerHost, NettyServerHost>();
            containerRegistry.Register<NettyServerHandler>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());

            //AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<UnitFloorEntity, UnitProcessorFloorEntity>()
                //   .ForMember(dest => dest.FloorId, opt => opt.MapFrom(src => src.Id));
                //cfg.CreateMap<UnitProcessorFloorEntity, UnitFloorEntity>()
                //   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FloorId));
            });

            // only during development, validate your mappings; remove it before release
            configuration.AssertConfigurationIsValid();
            // use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
            var mapper = configuration.CreateMapper();
            containerRegistry.RegisterInstance(mapper);

            //初始化读卡器设备注入
            containerRegistry.RegisterDeviceUnitTypes();

            // 初始化数据
            InitDataAsync();
        }
    }
}
