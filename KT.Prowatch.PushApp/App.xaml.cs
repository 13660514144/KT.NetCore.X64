using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Prowatch.Service.Base;
using KT.Prowatch.Service.Daos;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Services;
using KT.Proxy.BackendApi.Apis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Prowatch.PushApp
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
            new EventWaitHandle(false, EventResetMode.AutoReset, "KT.Prowatch.PushApp", out bool createNew);

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
            var exception = e.GetInner();
            _logger?.LogError($"App_DispatcherUnhandledException Error:{exception} ");
            e.Handled = true;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            var exception = ex.GetInner();
            _logger?.LogError($"CurrentDomain_UnhandledException Error:{exception} ");
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.GetInner();
            _logger?.LogError("TaskScheduler_UnobservedTaskException Error:{0} ", JsonConvert.SerializeObject(exception, JsonUtil.JsonSettings));
            e.SetObserved();
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

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppUserSettings").Get<AppUserSettings>();
            containerRegistry.RegisterInstance(appSettings);

            //containerRegistry.Register<DbContextOptions<ProwatchContext>>();

            containerRegistry.Register<ILoginUserDao, LoginUserDao>();
            containerRegistry.Register<IUserTokenDao, UserTokenDao>();
            containerRegistry.Register<IProwatchDao, ProwatchDao>();
            containerRegistry.Register<ILoginUserService, LoginUserService>();
            containerRegistry.Register<IUserTokenService, UserTokenService>();
            containerRegistry.Register<IProwatchService, ProwatchService>();
            containerRegistry.Register<IPushEventService, PushEventService>();

            containerRegistry.RegisterInstance<ILogger<LoginUserDao>>(new Log4gHelper<LoginUserDao>());
            containerRegistry.RegisterInstance<ILogger<UserTokenDao>>(new Log4gHelper<UserTokenDao>());
            containerRegistry.RegisterInstance<ILogger<ProwatchDao>>(new Log4gHelper<ProwatchDao>());
            containerRegistry.RegisterInstance<ILogger<LoginUserService>>(new Log4gHelper<LoginUserService>());
            containerRegistry.RegisterInstance<ILogger<UserTokenService>>(new Log4gHelper<UserTokenService>());
            containerRegistry.RegisterInstance<ILogger<ProwatchService>>(new Log4gHelper<ProwatchService>());
            containerRegistry.RegisterInstance<ILogger<PushEventService>>(new Log4gHelper<PushEventService>());

            //HttpProxy
            containerRegistry.RegisterSingleton<OpenApi>();
            containerRegistry.RegisterSingleton<InitHelper>();

            containerRegistry.RegisterInstance<ILogger<OpenApi>>(new Log4gHelper<OpenApi>());
            containerRegistry.RegisterInstance<ILogger<InitHelper>>(new Log4gHelper<InitHelper>());
            containerRegistry.RegisterInstance<ILogger<ReaderHelper>>(new Log4gHelper<ReaderHelper>());
            containerRegistry.RegisterInstance<ILogger<ScrollMessageViewModel>>(new Log4gHelper<ScrollMessageViewModel>());
        }
    }
}