using CommonUtils;
using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Exceptions;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.ViewModels;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.Visitor.Interface.Views.Integrate
{
    /// <summary>
    /// InterateVisitorRegisterControl.xaml 的交互逻辑
    /// </summary>
    public partial class InterateVisitorRegisterControl : UserControl
    {
        public InterateVisitorRegisterControlViewModel ViewModel;
        private ILogger<InterateVisitorRegisterControl> _logger;

        private AppointmentApi _appointmentApi;
        private BlacklistApi _blacklistApi;
        private SmallTicketOperator _smallTicketOperator;
        private IContainerProvider _containerProvider;
        private MainFrameHelper _mainFrameHelper;
        private ConfigHelper _configHelper;
        private IntegrateSuccessWindow _integrateSuccessWindow;
        private FrontBaseApi _frontBaseApi;

        public InterateVisitorRegisterControl(InterateVisitorRegisterControlViewModel viewModel,
            ILogger<InterateVisitorRegisterControl> logger,
            AppointmentApi appointmentApi,
            BlacklistApi blacklistApi,
            SmallTicketOperator smallTicketOperator,
            IContainerProvider containerProvider,
            MainFrameHelper mainFrameHelper,
            ConfigHelper configHelper,
            IntegrateSuccessWindow integrateSuccessWindow,
            FrontBaseApi frontBaseApi)
        {
            InitializeComponent();

            ViewModel = viewModel;
            _logger = logger;
            _appointmentApi = appointmentApi;
            _blacklistApi = blacklistApi;
            _smallTicketOperator = smallTicketOperator;
            _containerProvider = containerProvider;
            _mainFrameHelper = mainFrameHelper;
            _configHelper = configHelper;
            _integrateSuccessWindow = integrateSuccessWindow;
            _frontBaseApi = frontBaseApi;

            this.DataContext = ViewModel;

            ViewModel.IntegrateCompanyControl.SelectedCompanyClick += SelectedCompany;

            ViewModel.IntegrateAuthModeControl.CancleClick += SelectedCancle;
            ViewModel.IntegrateAuthModeControl.ConfirmClick += SelectedAuthMode;

            //获取公司，验证黑名单使用
            ViewModel.IntegrateVisitorControl.GetCompanyFunc += GetSelectedCompany;

            ViewModel.IntegrateVisitorControl.ConfirmClick += RegisterConfirm;
            ViewModel.IntegrateVisitorControl.CancelClick += CancelVisitor;
        }

        private void SelectedCancle(object sender, RoutedEventArgs e)
        {
            ViewModel.IntegrateCompanyControl.Visibility = Visibility.Visible;
            ViewModel.IntegrateAuthModeControl.Visibility = Visibility.Collapsed;
        }

        private void CancelVisitor(object sender, RoutedEventArgs e)
        {
            ViewModel.IntegrateAuthModeControl.Visibility = Visibility.Visible;
            ViewModel.IntegrateVisitorControl.Visibility = Visibility.Collapsed;
        }

        private void RegisterConfirm(object sender, RoutedEventArgs e)
        {
            var btnSubmit = (Button)sender;
            btnSubmit.IsEnabled = false;
            Task.Run(async () =>
            {
                try
                {
                    await SubmitAsync();
                }
                catch (Exception ex)
                {
                    var exception = ex.InnerException ?? ex;
                    _logger.LogError("提交访客登记数据错误：", ex);

                    Dispatcher.Invoke(() =>
                    {
                        MessageWarnBox.Show(exception.Message);
                    });
                }
                finally
                {
                    Dispatcher.Invoke(() =>
                    {
                        btnSubmit.IsEnabled = true;
                    });
                }
            });
        }

        private async Task SubmitAsync()
        {
            //非空校验
            string warnMsg = null;
            var mainVisitorViewModel = ViewModel.IntegrateVisitorControl.ViewModel.AccompanyVisitorControl.ViewModel;

            if (ViewModel.IntegrateCompanyControl.ViewModel.CompanyShowDetailControl.ViewModel.Company == null)
            {
                warnMsg = "请选择要访问的公司!" + "\r\n";
            }
            if (string.IsNullOrEmpty(mainVisitorViewModel.VisitorInfo.Name))
            {
                warnMsg += "请输入主访客姓名!" + "\r\n";
            }
            if (string.IsNullOrEmpty(mainVisitorViewModel.VisitorInfo.CertificateNumber))
            {
                warnMsg += "请输入主访客证件号码!" + "\r\n";
            }
            if (!string.IsNullOrEmpty(mainVisitorViewModel.VisitorInfo.Phone)
                && mainVisitorViewModel.VisitorInfo.Phone.Length != 11)
            {
                warnMsg += "手机号码格式有误!" + "\r\n";
            }
            //来访事由
            string _reason = ViewModel.IntegrateVisitorControl.ViewModel.VisitReasons.FirstOrDefault(x => x.IsChecked)?.Name;
            if (_reason == null)
            {
                warnMsg += "请选择来访事由!" + "\r\n";
            }
            if (!ViewModel.IntegrateAuthModeControl.ViewModel.TimeLimitVM.IsOne &&
                (!ViewModel.IntegrateAuthModeControl.ViewModel.TimeLimitVM.IsAuth ||
                ViewModel.IntegrateAuthModeControl.ViewModel.TimeLimitVM.Days <= 0))
            {
                warnMsg += "请选择授权时限!" + "\r\n";
            }
            List<string> authcategory = new List<string>();
            foreach (var item in ViewModel.IntegrateAuthModeControl.ViewModel.AuthTypes)
            {
                if (item.IsChecked)
                {
                    authcategory.Add(item.Id);
                }
            }
            if (authcategory.Count <= 0)
            {
                warnMsg += "请至少选择一种授权方式!" + "\r\n";
            }

            if (warnMsg != null)
            {
                throw CustomException.Run(warnMsg);
            }

            //创建接收数据的对象
            AppointmentModel mvisitor = new AppointmentModel();
            mvisitor.name = mainVisitorViewModel.VisitorInfo.Name;
            mvisitor.idNumber = mainVisitorViewModel.VisitorInfo.CertificateNumber;
            mvisitor.phone = mainVisitorViewModel.VisitorInfo.Phone;
            mvisitor.reason = _reason;
            mvisitor.icCard = mainVisitorViewModel.VisitorInfo.IcCardNumber;
            mvisitor.accessDate = ViewModel.IntegrateAuthModeControl.ViewModel.TimeLimitVM.Days;
            mvisitor.once = ViewModel.IntegrateAuthModeControl.ViewModel.TimeLimitVM.IsOne;
            mvisitor.authTypes = authcategory;
            mvisitor.gender = mainVisitorViewModel.VisitorInfo.Gender;

            //选择的公司赋值
            var company = ViewModel.IntegrateCompanyControl.ViewModel.CompanyShowDetailControl.ViewModel.Company;
            mvisitor.beVisitCompanyId = company.Id;
            mvisitor.beVisitCompanyName = company.Name;
            mvisitor.beVisitFloorId = company.FloorId;

            //验证黑名单   
            if (!string.IsNullOrEmpty(mvisitor.phone))
            {
                var isBlack = await _blacklistApi.IsBlackAsync(mvisitor.phone, string.Empty, company.FloorId, company.Id);
                if (isBlack)
                {
                    throw CustomException.Run("抱歉，手机号码疑似黑名单，不能登记！");
                }
            }

            //设置人脸头像，授权需拍照
            mvisitor.faceImg = mainVisitorViewModel.TakePictureControl.ViewModel.ImageUrl;
            bool isFace = mvisitor.authTypes.Contains(AuthModelEnum.FACE.Value);
            if (isFace && string.IsNullOrEmpty(mvisitor.faceImg))
            {
                throw CustomException.Run("抱歉，人脸授权请先拍照！");
            }

            //陪同访客
            var visitorTeams = new List<VisitorTeamModel>();
            var accompanies = ViewModel.IntegrateVisitorControl.ViewModel.IntegrateAccompanyVisitorsWindowViewModel?.GetVisitorTeams();
            if (accompanies != null && accompanies.FirstOrDefault() != null)
            {
                visitorTeams.AddRange(accompanies);
            }

            //校验陪同访客
            foreach (var item in visitorTeams)
            {
                //不校验主访客 
                if (string.IsNullOrEmpty(item.Name))
                {
                    throw CustomException.Run("请输入陪同访客姓名！");
                }
                if (string.IsNullOrEmpty(item.IdNumber))
                {
                    throw CustomException.Run($"请输入陪同访客身份证，访客：{item.Name}");
                }
                if (isFace && string.IsNullOrEmpty(item.FaceImg))
                {
                    throw CustomException.Run($"请拍照陪同访客照片，访客：{item.Name}");
                }
                var isBlack = await _blacklistApi.IsBlackAsync(item.Phone, item.IdNumber, company.FloorId, company.Id);
                if (isBlack)
                {
                    throw CustomException.Run($"抱歉，【{item.Name}】疑似黑名单用户，不能登记！");
                }
            }

            //加人加人脸的陪同访客不用校验
            var cardAccompanies = ViewModel.IntegrateVisitorControl.ViewModel.AddAccompanyCardWindowViewModel?.GetVisitorTeams();
            if (cardAccompanies != null && cardAccompanies.FirstOrDefault() != null)
            {
                visitorTeams.AddRange(cardAccompanies);
            }
            var photoAccompanies = ViewModel.IntegrateVisitorControl.ViewModel.AddAccompanyPhotoWindowViewModel?.GetVisitorTeams();
            if (photoAccompanies != null && photoAccompanies.FirstOrDefault() != null)
            {
                visitorTeams.AddRange(photoAccompanies);
            }

            mvisitor.retinues = visitorTeams;

            //后台提交预约登录
            var lst = await _appointmentApi.SubmitAppoiAsync(mvisitor);

            // todo:在次判断是否打印二维码
            if (mvisitor.authTypes.Contains(AuthModelEnum.QR.Value) && lst.Count > 0)
            {
                await Dispatcher.Invoke(async () =>
                {
                    //CommonHelper.PrintQR(lst); 
                    foreach (var item in lst)
                    {
                        //打印二维码//C764D517E93705E2B734F19D63C069718A615D6FA13E0A74564505566979A8922028D238747C5493347CDC96DFD9718F 
                        var imageInfo = await _frontBaseApi.GetQrAsync(item.Qr);
                        var bitmap = ImageConvert.BytesToBitmap(imageInfo.Bytes);

                        _smallTicketOperator.Init(PrintConfig.VISIT_QR_CODE_DOCUMENT_PATH, VisitQRCodePrintModel.FromVisitorResult(item, bitmap), new VisitQRCodeRenderer());
                        await _smallTicketOperator.StartPrintAsync();
                    }
                });
            }
            Dispatcher.Invoke(() =>
            {
                _integrateSuccessWindow.ShowDialog();
            });

            EndRegistedAction?.Invoke();
        }

        public Action EndRegistedAction;

        private CompanyViewModel GetSelectedCompany()
        {
            return ViewModel.IntegrateCompanyControl.ViewModel.CompanyShowDetailControl.ViewModel.Company;
        }

        private void SelectedAuthMode(object sender, RoutedEventArgs e)
        {
            var authModel = ViewModel.IntegrateAuthModeControl.ViewModel.AuthTypes.FirstOrDefault(x => x.IsChecked);
            if (authModel == null)
            {
                MessageWarnBox.Show("请选择一种授权方式！");
                return;
            }
            ViewModel.IntegrateAuthModeControl.Visibility = Visibility.Collapsed;
            ViewModel.IntegrateVisitorControl.Visibility = Visibility.Visible;

            ViewModel.IntegrateVisitorControl.ViewModel.AuthModel = authModel.Id;
        }

        private void SelectedCompany(object sender, RoutedEventArgs e)
        {
            if (!_configHelper.LocalConfig.IsRememberAuthModel)
            {
                ViewModel.IntegrateCompanyControl.Visibility = Visibility.Collapsed;
                ViewModel.IntegrateAuthModeControl.Visibility = Visibility.Visible;
            }
            else
            {
                ViewModel.IntegrateCompanyControl.Visibility = Visibility.Collapsed;
                ViewModel.IntegrateVisitorControl.Visibility = Visibility.Visible;
                ViewModel.IntegrateVisitorControl.ViewModel.AuthModel = _configHelper.LocalConfig.AuthModel;
            }
        }


    }
}
