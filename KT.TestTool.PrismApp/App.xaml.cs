using KT.Common.WpfApp.Helpers;
using KT.Tests.PrismApp.IServices;
using KT.Tests.PrismApp.Services;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System.Windows;

namespace KT.Tests.PrismApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IUserService, UserService>();

            var logger = new Log4gHelper();
            containerRegistry.RegisterInstance<ILogger>(logger);
        }
    }
}
