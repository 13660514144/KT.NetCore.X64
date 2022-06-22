using KT.Visitor.Interface.Views.Setting;
using KT.Visitor.Interface.Views.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// IntegrateLoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IntegrateLoginWindow : WindowX
    {
        private LoginWindowViewModel _viewModel;
        private ILogger<IntegrateLoginWindow> _logger;
        private IContainerProvider _containerProvider;

        public IntegrateLoginWindow(LoginWindowViewModel viewModel,
            ILogger<IntegrateLoginWindow> logger,
            IContainerProvider containerProvider)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _logger = logger;
            _containerProvider = containerProvider;

            _ = InitAsync();
        }

        public async Task InitAsync()
        {
            //设置viewModel执行界面操作
            await _viewModel.InitAsync(SetPassword, GetPassword, LoginEnd);

            this.DataContext = _viewModel;
            //var text = new  TextBox();
            //text.Focus();
        }

        private string GetPassword()
        {
            return txt_pwd.Password;
        }

        private void SetPassword(string password)
        {
            Dispatcher.Invoke(() =>
            {
                txt_pwd.Password = password;
            });
        }

        public void LoginEnd()
        {
            var integrateMainWindow = _containerProvider.Resolve<IntegrateMainWindow>();
            integrateMainWindow.Show();
            this.Close();
        }

        private void But_Setting_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Setting();
        }

        private void But_Closed_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void txt_pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.PasswordError = null;
        }
    }
}
