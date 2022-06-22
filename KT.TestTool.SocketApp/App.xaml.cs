using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.TestTool.SocketApp.Views;
using KT.Visitor.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KT.TestTool.SocketApp
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
                Application.Current.Shutdown();
                Environment.Exit(0);
            }

            base.OnStartup(e);
        }


        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.GetInner();
            _logger?.LogError($"App_DispatcherUnhandledException Error:{exception} ");

            //Container.Resolve<OperateErrorPage>().ShowMessage(exception.Message); 
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            var exception = ex.GetInner();
            _logger?.LogError($"CurrentDomain_UnhandledException Error:{exception} ");
            // Container.Resolve<OperateErrorPage>().ShowMessage(ex.InnerException?.Message ?? ex?.Message);
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //异步处理错误消息
            var exception = e.GetInner();
            _logger?.LogError($"TaskScheduler_UnobservedTaskException Error:{exception} ");

            //将异常标识为已经观察到 
            e.SetObserved();
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

        private async void InitDataAsync()
        {

        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var logger = new Log4gHelper();

            containerRegistry.RegisterInstance(Container);

            ContainerHelper.SetProvider(logger, Container);

            //确定值的类
            containerRegistry.RegisterInstance<ILogger>(logger);

            containerRegistry.RegisterSingleton<DialogHelper>();

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var socketSettings = PrismDependency.GetConfiguration().GetSection("SocketSettings").Get<SocketSettings>();
            containerRegistry.RegisterInstance(socketSettings);

            containerRegistry.RegisterInstance<ILogger<UdpControl>>(new Log4gHelper<UdpControl>());
            containerRegistry.RegisterInstance<ILogger<TcpControl>>(new Log4gHelper<TcpControl>());
            containerRegistry.RegisterInstance<ILogger<ScrollMessageViewModel>>(new Log4gHelper<ScrollMessageViewModel>());

            InitDataAsync();
        }

    }
}
