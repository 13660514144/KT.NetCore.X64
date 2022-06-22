using KT.Common.WpfApp.Helpers;
using KT.Visitor.Data.IServices;
using KT.Visitor.SelfApp.Helpers;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.SelfApp.Views.Setting
{
    /// <summary>
    /// SettingPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingPage : Page
    {
        private SettingPageViewModel _viewModel;
        private MainFrameHelper _mainFrameHelper;
        private ILoginUserDataService _loginUserDataService;

        public SettingPage()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<SettingPageViewModel>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _loginUserDataService = ContainerHelper.Resolve<ILoginUserDataService>();

            this.Loaded += SettingPage_Loaded;

            uc_kb.EndAction = EndInputPassword;
        }

        private void EndInputPassword()
        {
            _ = NextOperateAsync();
        }

        private void SettingPage_Loaded(object sender, RoutedEventArgs e)
        {
            pageIndex = 1;
            sp_Login.Visibility = Visibility.Visible;
            sp_Setting.Visibility = Visibility.Collapsed;
            sp_Success.Visibility = Visibility.Collapsed;
            btn_NextPage.Content = "登录";

            this.DataContext = _viewModel;
        }

        //操作步骤 1、登录 2、修改配置 3、操作成功
        private int pageIndex = 1;
        private void btn_Pre_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex == 1)
            {
                _mainFrameHelper.PreHistory(this);
            }
            else if (pageIndex == 2)
            {
                _mainFrameHelper.PreHistory(this);
            }
            else if (pageIndex == 3)
            {
                sp_Setting.Visibility = Visibility.Visible;
                sp_Success.Visibility = Visibility.Collapsed;
                pageIndex = 2;
            }
        }

        private void btn_NextPage_Click(object sender, RoutedEventArgs e)
        {
            _ = NextOperateAsync();
        }

        private async Task NextOperateAsync()
        {
            if (pageIndex == 1)
            {
                var isExistsUser = await _loginUserDataService.IsExistsSystemUserAsync(_viewModel.PasswordVM?.Password);
                if (!isExistsUser)
                {
                    _viewModel.PasswordVM.ErrorMsg = "密码错误！";
                    _viewModel.PasswordVM.IsError = Visibility.Visible;
                    return;
                }
                sp_Login.Visibility = Visibility.Collapsed;
                sp_Setting.Visibility = Visibility.Visible;

                btn_NextPage.Content = "保存";

                pageIndex = 2;
            }
            else if (pageIndex == 2)
            {
                //保存设置
                await _viewModel.SaveSettingAsync();

                sp_Setting.Visibility = Visibility.Collapsed;
                sp_Success.Visibility = Visibility.Visible;

                btn_Pre.Visibility = Visibility.Hidden;
                btn_NextPage.Content = "完成";

                pageIndex = 3;
            }
            else if (pageIndex == 3)
            {
                var UI = ContainerHelper.Resolve<HomePage>();
                _mainFrameHelper.Link(UI);
            }
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
