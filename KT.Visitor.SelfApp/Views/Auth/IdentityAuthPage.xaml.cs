using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Enums;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using KT.Visitor.SelfApp.Views.Auth;
using Prism.Ioc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.SelfApp.Views.Auth
{
    /// <summary>
    /// IdentityAuthPage.xaml 的交互逻辑
    /// </summary>
    public partial class IdentityAuthPage : Page
    {
        private IdentityAuthPageViewModel _viewModel;
        private MainFrameHelper _mainFrameHelper;
        private IVisitorApi _visitorApi;

        public IdentityAuthPage()
        {
            InitializeComponent();

            _viewModel = ContainerHelper.Resolve<IdentityAuthPageViewModel>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();

            uc_kb.EndAction = CheckData;

            this.DataContext = _viewModel;
        }

        private void CheckData()
        {
            _ = CheckDataAsync();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            _ = CheckDataAsync();
        }

        private async Task CheckDataAsync()
        {
            if (string.IsNullOrEmpty(txt_input_number.Text))
            {
                _viewModel.PhoneVM.IsError = Visibility.Visible;
                _viewModel.PhoneVM.ErrorMsg = "请输入要校验的号码";
                return;
            }

            var query = new AuthCheckModel();
            query.Phone = txt_input_number.Text;
            
            var records = await _visitorApi.CheckAsync(query);

            if (records == null || records.FirstOrDefault() == null)
            {
                var errorPage = ContainerHelper.Resolve<OperateErrorPage>();
                errorPage.ViewModel.Title = "身份验证";
                errorPage.ViewModel.ErrorMsg = "没有找到邀约记录！";
                _mainFrameHelper.Link(errorPage, false);
                return;

                //throw CustomException.Run("没有找到预约记录！");
            }

            var page = ContainerHelper.Resolve<IdentityReadIdCardPage>();
            page.Init(records);
            _mainFrameHelper.Link(page, false);
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var page = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(page);
        }

        private void BtnlastSetp_Click(object sender, RoutedEventArgs e)
        {
            _mainFrameHelper.PreHistory(this);
        }

        private void PUTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            uc_kb.Visibility = Visibility.Visible;
        }
    }
}
