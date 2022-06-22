using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.IServices;
using KT.Visitor.Interface.Views.Setting;
using KT.Visitor.Interface.Views.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.Interface
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : WindowX
    {
        private LoginWindowViewModel _viewModel;
        private ILogger<LoginWindow> _logger;
        private IContainerProvider _containerProvider;

        public LoginWindow(LoginWindowViewModel viewModel,
            ILogger<LoginWindow> logger,
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
        }

        private string GetPassword()
        {
            return txt_pwd.Password;
        }

        private void SetPassword(string password)
        {
            Dispatcher.InvokeAsync(() =>
            {
                txt_pwd.Password = password;
            });
        }

        public void LoginEnd()
        {
            var integrateMainWindow = _containerProvider.Resolve<FrontMainWindow>();
            integrateMainWindow.Show();
            this.Close();
        }

        private void But_Setting_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Setting();
        }

        private void But_Closed_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void txt_pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.PasswordError = null;
        }
    }
}
