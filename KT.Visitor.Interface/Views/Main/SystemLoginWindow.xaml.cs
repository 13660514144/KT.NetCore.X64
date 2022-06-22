using KT.Visitor.Data.IServices;
using Panuon.UI.Silver;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.Interface.Views.User
{
    /// <summary>
    /// SystemLoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SystemLoginWindow : WindowX
    {
        private ILoginUserDataService _loginUserDataService;

        public SystemLoginWindow(ILoginUserDataService loginUserDataService)
        {
            InitializeComponent();

            _loginUserDataService = loginUserDataService;
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            _ = LoginAsync();
        }

        private async Task LoginAsync()
        {
            //检测输入字符
            if (string.IsNullOrEmpty(Pb_Password.Password))
            {
                Sp_Error.Visibility = Visibility.Visible;
                return;
            }
            //验证管理员密码
            var isExists = await _loginUserDataService.IsExistsSystemUserAsync(Pb_Password.Password);
            if (!isExists)
            {
                Sp_Error.Visibility = Visibility.Visible;
                return;
            }
            this.DialogResult = true;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Pb_Password_TextChanged(object sender, RoutedEventArgs e)
        {
            Sp_Error.Visibility = Visibility.Hidden;
        }
    }
}
