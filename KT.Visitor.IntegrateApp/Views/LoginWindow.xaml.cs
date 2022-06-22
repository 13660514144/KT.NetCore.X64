using KT.Common.WpfApp.Helpers;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
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

namespace KT.Visitor.IntegrateApp.Views
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : WindowX
    {
        private LoginWindowViewModel _viewModel;
        private DialogHelper _dialogHelper;
        private AppSettings _appSettings;

        public LoginWindow()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<LoginWindowViewModel>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();

            _ = InitAsync();

            this.Loaded += LoginWindow_Loaded;
            this.KeyDown += LoginWindow_KeyDown;
        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Environment.Exit(0);
            }
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置全屏
            WindowHelper.ReleaseFullWindow(this, _appSettings.IsFullScreen);
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
            Dispatcher.Invoke(() =>
            {
                txt_pwd.Password = password;
            });
        }

        public void LoginEnd(string userName)
        {
            //清除登录界面
            _dialogHelper.RemoveFirst();
            var mainWindow = ContainerHelper.Resolve<MainWindow>();
            mainWindow.ViewModel.TopPanel.UserName = userName;
            _dialogHelper.Show(mainWindow);
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
