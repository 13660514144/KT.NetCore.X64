using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Visitor.Controls;
using Microsoft.Extensions.DependencyInjection;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateMainWindow : WindowX
    {
        private IntegrateMainWindowViewModel _viewModel;
        private IContainerProvider _containerProvider;

        public IntegrateMainWindow(IntegrateMainWindowViewModel viewModel,
            IContainerProvider containerProvider)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _containerProvider = containerProvider;

            //初始化要加载数据，先登录再加载
            this.DataContext = _viewModel;

            this.KeyDown += WIndex_KeyDown;
        }

        private void WIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Environment.Exit(0);
            }
        }

        private void Btn_ExitLogin_Click(object sender, RoutedEventArgs e)
        {
            var integrateLoginWindow = _containerProvider.Resolve<IntegrateLoginWindow>();
            integrateLoginWindow.Show();
            this.Close();
        }
    }
}
