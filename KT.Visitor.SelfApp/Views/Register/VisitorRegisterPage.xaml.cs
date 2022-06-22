using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
using KT.Visitor.Data.Enums;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using KT.Visitor.SelfApp.Views.Auth;
using KT.Visitor.SelfApp.Views.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KT.Visitor.SelfApp.Register
{
    /// <summary>
    /// VisitorRegisterPage.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorRegisterPage : Page
    {
        public VisitorRegisterPageViewModel ViewModel { get; set; }

        /// <summary>
        /// 初始化是否成功
        /// </summary>
        public bool IsInit = true;

        private MainFrameHelper _mainFrameHelper;
        private IFunctionApi _functionApi;

        private IVisitorApi _visitorApi;
        private PrintHandler _printHandler;
        private AppSettings _appSettings;
        private ILogger _logger;

        public VisitorRegisterPage(MainFrameHelper mainFrameHelper,
            IFunctionApi functionApi,
            AppSettings appSettings)
        {
            InitializeComponent();

            _mainFrameHelper = mainFrameHelper;
            _functionApi = functionApi;
            _appSettings = appSettings;

            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _logger = ContainerHelper.Resolve<ILogger>();

            ViewModel = new VisitorRegisterPageViewModel();
            this.DataContext = ViewModel;

            uc_keybrod.EndAction = EndInputPhone;

            this.Loaded += RegisterPage_Loaded;
        }

        private void EndInputPhone()
        {
            uc_keybrod.Visibility = Visibility.Collapsed;

            pop_NumberInput.IsOpen = false;
        }

        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            st_reason.Visibility = Visibility.Collapsed;
        }

        private void Uc_keybrod_ValueVerify(object sender, EventArgs e)
        {
            ViewModel.PhoneVM.IsError = Visibility.Hidden;
            ViewModel.PhoneVM.ErrorMsg = "";
            Appointor.Phone = txt_mobile.Text;
        }

        #region 成员变量
        public System.Drawing.Image headImg { get; set; }
        public System.Drawing.Image img_pz { get; set; }

        public string faceImg { get; set; }
        public string vistorname { get; set; }
        public string idnumber { get; set; }
        public AppointmentModel Appointor { get; set; }
        public int authCategory = 0;


        public CompanyModel Company { get; set; }

        #endregion
        private void Uc_keybrod_BtnClick(object sender, EventArgs e)
        {
            txt_mobile.Text = uc_keybrod.TxtNumber;
        }

        private void Txt_mobile_GotFocus(object sender, RoutedEventArgs e)
        {
            uc_keybrod.Visibility = Visibility.Visible;

            pop_NumberInput.IsOpen = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            uc_keybrod.TxtNumber = "";
            txt_mobile.Text = "";
            pop_NumberInput.IsOpen = false;
        }

        private void Btn_next_Click(object sender, RoutedEventArgs e)
        {
            //重新校验数据 ，防止直接点下一步
            if (string.IsNullOrEmpty(txt_mobile.Text))
            {
                ViewModel.PhoneVM.IsError = Visibility.Visible;
                ViewModel.PhoneVM.ErrorMsg = "手机号码不能为空";
                return;
            }
            //匹配手机号规则
            if (!RegularUtil.MatchRegular(txt_mobile.Text,_appSettings.PhoneRegular))
            {
                ViewModel.PhoneVM.IsError = Visibility.Visible;
                ViewModel.PhoneVM.ErrorMsg = "手机号码格式不正确";
                return;
            }
            var reason = ViewModel.VisitReasons.FirstOrDefault(x => x.IsChecked);
            if (reason == null)
            {
                st_reason.Visibility = Visibility.Visible;
                return;
            }

            Appointor.Phone = txt_mobile.Text;
            Appointor.Reason = reason.Id;

            Appointor.VisitorFrom = VisitorFromEnum.SELF_HELP.Value;

            //向服务器提交预约数据
            SubmitAppoi(Appointor);
        }

        private void BtnLastSetp_Click(object sender, RoutedEventArgs e)
        {
            var UI = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
        }

        private void Img_exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var UI = ContainerHelper.Resolve<HomePage>();
            _mainFrameHelper.Link(UI);
        }

        private void RegisterPage_Loaded(object sender, RoutedEventArgs e)
        {
            init_input();
        }

        private void init_input()
        {
            uc_keybrod.TxtNumber = "";
            txt_mobile.Text = "";
            this.faceImg = "";
            ViewModel.PhoneVM.IsError = Visibility.Hidden;
            ViewModel.PhoneVM.ErrorMsg = "";
        }

        private void SubmitAppoi(AppointmentModel appoint)
        {
            MaskTipBox.Run(appoint, RunSubmitAsync, SuccessSubmit, ErrorSubmit);
        }
        /// <summary>
        /// 取消后重新预约
        /// </summary>
        /// <param name="ex"></param>
        private void ErrorSubmit(Exception ex)
        {
            _logger.LogError($"预约失败：{ex} ");
            var exception = ex.GetInner();
            var UI = ContainerHelper.Resolve<OperateErrorPage>();
            //UI.ViewModel.ErrorMsg = $"预约失败，请重新操作：{exception.Message}";
            UI.ViewModel.ErrorMsg = $"预约不成功，请稍后重新预约：";
            UI.ViewModel.Title = "操作提示"; 
            _mainFrameHelper.Link(UI, false);
        }

        private void SuccessSubmit(DataResponse<object> result, AppointmentModel appoint)
        {
            //已经存在预约，取消后再继续预约
            if (result.Code == 50001)
            {
                var warnPage = ContainerHelper.Resolve<MultiRegisterWarnPage>();
                warnPage.ViewModel.Appointment = appoint;
                warnPage.ViewModel.VisitorInfo = JsonConvert.DeserializeObject<VisitorInfoModel>(JsonConvert.SerializeObject(result.Data, JsonUtil.JsonSettings), JsonUtil.JsonSettings);

                _mainFrameHelper.Link(warnPage);
                return;
            }
            //预约成功
            var registerResults = JsonConvert.DeserializeObject<List<RegisterResultModel>>(JsonConvert.SerializeObject(result.Data, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
            //生成二维码并打印
            var successPage = ContainerHelper.Resolve<OperateSucessPage>();
            successPage.ViewModel.Init(registerResults);
            successPage.LoadedAction += () =>
            {
                // 异步 打印二维码 
                _printHandler.StartPrintAsync(registerResults, appoint.Once);
            };
            _mainFrameHelper.Link(successPage);
        }

        private async Task<DataResponse<object>> RunSubmitAsync(AppointmentModel appoint)
        {
            //后台提交预约登录
            var results = await _visitorApi.AddAsync(appoint);
            return results;
        }

        private void txt_mobile_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            uc_keybrod.Visibility = Visibility.Visible;

            pop_NumberInput.IsOpen = true;
        }
    }
}
