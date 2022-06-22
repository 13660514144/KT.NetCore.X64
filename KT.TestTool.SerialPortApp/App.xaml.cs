using Prism.Ioc;
using KT.TestTool.SerialPortApp.Views;
using System.Windows;

namespace KT.TestTool.SerialPortApp
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
