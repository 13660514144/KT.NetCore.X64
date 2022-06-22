using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Front.ToolApp.Views;
using KT.Front.WriteCard.Services;
using KT.Front.WriteCard.Util;
using KT.Proxy.BackendApi.Apis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Front.ToolApp
{
    //TODO 因为导入KT.Common.WpfApp项目时会报错，所以项目中暂时没有用到依赖注入其他组件
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger<App> _logger;
        public App()
        {
            _logger = new LogHelper<App>();

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        private async void InitDataAsync()
        {
            //文件清除
            var cleanFileHelper = Container.Resolve<CleanFileHelper>();
            var cleanFileSettings = Container.Resolve<CleanFileSettings>();
            await cleanFileHelper.StartAsync(cleanFileSettings);
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

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            //后台Api接口
            containerRegistry.Register<BackendApiBase>();
            //containerRegistry.Register<IEdificeApi, EdificeApi>();
            //containerRegistry.RegisterInstance<ILogger<EdificeApi>>(new Log4gHelper<EdificeApi>());
            //containerRegistry.Register<IElevatorGroupApi, ElevatorGroupApi>();
            //containerRegistry.RegisterInstance<ILogger<ElevatorGroupApi>>(new Log4gHelper<ElevatorGroupApi>());
            //containerRegistry.Register<IKoneApi, KoneApi>();
            //containerRegistry.RegisterInstance<ILogger<KoneApi>>(new Log4gHelper<KoneApi>());
            //containerRegistry.Register<IFloorApi, FloorApi>();
            //containerRegistry.RegisterInstance<ILogger<FloorApi>>(new Log4gHelper<FloorApi>());
            //containerRegistry.Register<IPassRightApi, PassRightApi>();
            //containerRegistry.RegisterInstance<ILogger<PassRightApi>>(new Log4gHelper<PassRightApi>());
            //containerRegistry.Register<IHandleElevatorDeviceApi, HandleElevatorDeviceApi>();
            //containerRegistry.RegisterInstance<ILogger<HandleElevatorDeviceApi>>(new Log4gHelper<HandleElevatorDeviceApi>());

            containerRegistry.Register<IElevatorAuthInfoApi, ElevatorAuthInfoApi>();
            containerRegistry.RegisterInstance<ILogger<ElevatorAuthInfoApi>>(new Log4gHelper<ElevatorAuthInfoApi>());

            containerRegistry.Register<IElevatorAuthInfoService, ElevatorAuthInfoService>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();
            containerRegistry.RegisterInstance<ILogger<CleanFileHelper>>(new Log4gHelper<CleanFileHelper>());

            // 初始化数据
            InitDataAsync();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger?.LogError($"App_DispatcherUnhandledException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            var exception = e.Exception.GetInner();
            MessageBox.Show(exception.Message);
            //Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);

            ////发布错误事件
            //_eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);

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
                MessageBox.Show(exception.Message);
            });
            //Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);

            //发布错误事件
            //_eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);
        }
        private async void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //异步处理错误消息
            await TaskSchedulerUnobservedTaskExceptionExecAsync(e);

            //将异常标识为已经观察到 
            e.SetObserved();
        }

        private async Task TaskSchedulerUnobservedTaskExceptionExecAsync(UnobservedTaskExceptionEventArgs e)
        {
            _logger?.LogError($"TaskScheduler_UnobservedTaskException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            var exception = e.Exception.GetInner();
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show(exception.Message);
                //Container.Resolve<OperateErrorPage>().ShowMessage(exception?.Message);
            });

            await Task.CompletedTask;
        }
    }
}
