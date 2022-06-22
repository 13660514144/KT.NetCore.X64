using Prism.Ioc;
using KT.TestTool.EmguFace.Views;
using System.Windows;
using Prism.Mvvm;
using System.Globalization;
using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace KT.TestTool.EmguFace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static ILoggerFactory AppLoggerFactory =
       LoggerFactory.Create(bulidder =>
       {
           bulidder.AddLog4Net();
       });

        private ILogger _logger;

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

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

    }
}
