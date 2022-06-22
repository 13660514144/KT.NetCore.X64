using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.IServices;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Views.Setting;
using KT.Visitor.Interface.Views.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.FrontApp.Views
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : WindowX
    {
        private LoginWindowViewModel _viewModel;
        private ILogger _logger;
        private DialogHelper _dialogHelper;

        public LoginWindow()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<LoginWindowViewModel>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();

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

        public void LoginEnd(string userName)
        {
            //清除登录界面
            _dialogHelper.RemoveFirst();
            var integrateMainWindow = ContainerHelper.Resolve<MainWindow>();
            _dialogHelper.Show(integrateMainWindow);
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
