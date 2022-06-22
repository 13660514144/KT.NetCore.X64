using KT.SmartTool.WriteCardApp.Views;
using KT.Front.WriteCard.Util;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace KT.SmartTool.WriteCardApp
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //var logger = new Log4gHelper();

            //containerRegistry.RegisterInstance(Container);

            //ContainerHelper.SetProvider(logger, Container);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var exception = e.Exception;
            _logger?.LogError($"App_DispatcherUnhandledException Error:{exception} ");
            MessageBox.Show(e.Exception.Message);
            ////Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);

            ////发布错误事件
            ////_eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);

            ////标识事件已提交
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            _logger?.LogError($"CurrentDomain_UnhandledException Error:{ex} ");
            MessageBox.Show(ex.Message);
            //Container.Resolve<MessageWarnBox>().ShowMessage(exception.Message);

            //发布错误事件
            //_eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //异步处理错误消息
            TaskSchedulerUnobservedTaskExceptionExecAsync(e);

            //将异常标识为已经观察到 
            e.SetObserved();
        }

        private Task TaskSchedulerUnobservedTaskExceptionExecAsync(UnobservedTaskExceptionEventArgs e)
        {
            return Task.Run(() =>
            {
                var exception = e.Exception.InnerException;
                _logger?.LogError($"TaskScheduler_UnobservedTaskException Error:{exception} ");

                //Dispatcher.Invoke(() =>
                //{
                //    Container.Resolve<OperateErrorPage>().ShowMessage(exception?.Message);
                //});
            });
        }
    }
}
