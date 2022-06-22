using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Enums;
using KT.Visitor.Common.Helpers;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using KT.Visitor.SelfApp.Views.Auth;
using KT.Visitor.SelfApp.Views.Common;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KT.Visitor.SelfApp.Views.Auth
{
    /// <summary>
    /// InviteAuthPage.xaml 的交互逻辑
    /// </summary>
    public partial class InviteAuthPage : Page
    {
        private InviteAuthPageViewModel _viewModel;
        private MainFrameHelper _mainFrameHelper;
        private IVisitorApi _visitorApi;
        private ILogger _logger;
        private PrintHandler _printHandler;
        private string StaticQr;        
        public InviteAuthPage()
        {
            InitializeComponent();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _viewModel = ContainerHelper.Resolve<InviteAuthPageViewModel>();
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
            query.AskCode = txt_input_number.Text;
            
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
           
            
            var page = ContainerHelper.Resolve<InviteReadIdCardPage>();
            await page.InitAsync(records);            
            _mainFrameHelper.Link(page, false);
            
            //Appoint(string.Empty,records);//直接登记，远东版本
        }
        
        #region  直接登记，远东版本
        /*
        private void Appoint(string imageUrl, List<VisitorInfoModel> records)
        {            
            var appoint = new AuthInfoModel();
            foreach (var item in records)
            {
                var visitor = new AuthVisitorModel();
                visitor.Id = item.Id;
                visitor.Ic = item.IcCard;
                visitor.FaceImg = string.Empty;
                appoint.Visitors.Add(visitor);
            }

            MaskTipBox.Run(appoint, RunSubmitAsync, SuccessSubmit, ErrorSubmit);
        }
        private async Task<List<RegisterResultModel>> RunSubmitAsync(AuthInfoModel appoint)
        {
            //后台提交预约登录
            var results = await _visitorApi.AuthAsync(appoint);            
            return results;
        }
        private void SuccessSubmit(List<RegisterResultModel> results, AuthInfoModel appoint)
        {
            Dispatcher.Invoke(() =>
            {
                //跳转到成功页面
                var successPage = ContainerHelper.Resolve<OperateSucessPage>();
                successPage.ViewModel.Init(results);
                successPage.LoadedAction += () =>
                {
                    // 异步 打印二维码 
                    _printHandler.StartPrintAsync(results, false);
                };
                _mainFrameHelper.Link(successPage, false);
            });
        }
        private void ErrorSubmit(Exception ex)
        {
            _logger.LogError($"预约失败：{ex} ");
            var exception = ex.GetInner();
            Dispatcher.Invoke(() =>
            {
                var UI = ContainerHelper.Resolve<OperateErrorPage>();
                UI.ViewModel.ErrorMsg = $"预约失败，请重新操作：{exception.Message}";
                UI.ViewModel.Title = "操作提示";
                _mainFrameHelper.Link(UI, false);
            });
        }
        */
        #endregion
        
        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var UI = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
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
