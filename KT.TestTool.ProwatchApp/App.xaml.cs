using Prism.Ioc;
using KT.TestTool.ProwatchApp.Views;
using System.Windows;

namespace KT.TestTool.ProwatchApp
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

        }
    }
}
