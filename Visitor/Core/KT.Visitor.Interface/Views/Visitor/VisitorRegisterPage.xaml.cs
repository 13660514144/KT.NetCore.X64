using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.Core.Exceptions;
using KT.Proxy.WebApi.Backend.Apis;
using KT.Proxy.WebApi.Backend.Models;
using KT.Visitor.IdReader;
using KT.Visitor.IdReader.Common;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Tools.Printer.DocumentRenderer;
using KT.Visitor.Interface.Tools.Printer.Models;
using KT.Visitor.Interface.Tools.Printer.PrintOperator;
using KT.Visitor.Interface.ViewModels;
using KT.Visitor.Interface.Views.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using CommonUtils;

namespace KT.Visitor.Interface.Views.Visitor
{
    /// <summary>
    /// VisitorRegisterPage.xaml 的交互逻辑
    /// </summary>
    public partial class VisitorRegisterPage : Page
    {
        private VisitorRegisterPageViewModel _viewModel;
        private AppointmentApi _appointmentApi;
        private BlacklistApi _blacklistApi;
        private ConfigHelper _configHelper;
        private ReaderFactory _readerFactory;
        private ILogger<VisitorRegisterPage> _logger;
        private MainFrameHelper _mainFrameHelper;
        private SmallTicketOperator _smallTicketOperator;
        private IContainerProvider _containerProvider;
        private FrontBaseApi _frontBaseApi;

        //证件阅读器
        private IReader reader;
        //证件阅读器定时器,用于定时读取身份证件信息
        private Timer readerTimer;

        public VisitorRegisterPage(AppointmentApi appointmentApi,
            BlacklistApi blacklistApi,
            VisitorRegisterPageViewModel viewModel,
            ConfigHelper configHelper,
            ReaderFactory readerFactory,
            ILogger<VisitorRegisterPage> logger,
            MainFrameHelper mainFrameHelper,
            SmallTicketOperator smallTicketOperator,
            IContainerProvider containerProvider,
            FrontBaseApi frontBaseApi)
        {
            InitializeComponent();

            _appointmentApi = appointmentApi;
            _blacklistApi = blacklistApi;
            _viewModel = viewModel;
            _configHelper = configHelper;
            _readerFactory = readerFactory;
            _logger = logger;
            _mainFrameHelper = mainFrameHelper;
            _smallTicketOperator = smallTicketOperator;
            _containerProvider = containerProvider;

            DataContext = _viewModel;

            _viewModel.VisitorsControl.GetCompanyFunc += GetSelectedCompany;
        }

        public CompanyViewModel GetSelectedCompany()
        {
            return _viewModel.CompanySelectControl.ViewModel.CompanyShowDetailControl.ViewModel.Company;
        }

        private void Btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Btn_Submit.IsEnabled = false;
            Task.Run(async () =>
            {
                try
                {
                    await SubmitAsync();
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (ex.InnerException != null)
                        {
                            MessageWarnBox.Show(ex.InnerException.Message);
                        }
                        else
                        {
                            MessageWarnBox.Show(ex.Message);
                            _logger.LogInformation("提交数据错误：", ex);
                        }
                    });
                }
                finally
                {
                    Dispatcher.Invoke(() =>
                    {
                        Btn_Submit.IsEnabled = true;
                    });
                }
            });
        }

        private async Task SubmitAsync()
        {
            //非空校验
            string warnMsg = null;
            var mainVisitorViewModel = _viewModel.VisitorsControl.ViewModel.VisitorControls.FirstOrDefault().ViewModel;

            if (this._viewModel.CompanySelectControl.ViewModel.CompanyShowDetailControl.ViewModel.Company == null)
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
            string _reason = null;
            foreach (var item in _viewModel.VisitReasons)
            {
                if (item.IsChecked)
                {
                    _reason = item.Name;
                    break;
                }
            }
            if (_reason == null)
            {
                warnMsg += "请选择来访事由!" + "\r\n";
            }
            if (!_viewModel.TimeLimitVM.IsOne && (!_viewModel.TimeLimitVM.IsAuth || _viewModel.TimeLimitVM.Days <= 0))
            {
                warnMsg += "请选择授权时限!" + "\r\n";
            }
            List<string> authcategory = new List<string>();
            foreach (var item in _viewModel.AuthTypes)
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
            mvisitor.accessDate = _viewModel.TimeLimitVM.Days;
            mvisitor.once = _viewModel.TimeLimitVM.IsOne;
            mvisitor.authTypes = authcategory;
            mvisitor.gender = mainVisitorViewModel.VisitorInfo.Gender;

            //选择的公司赋值
            var company = this._viewModel.CompanySelectControl.ViewModel.CompanyShowDetailControl.ViewModel.Company;
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
            var ls = new List<VisitorTeamModel>();
            foreach (var item in _viewModel.VisitorsControl.ViewModel.VisitorControls)
            {
                //不校验主访客
                if (item.ViewModel.IsMain)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(item.ViewModel.VisitorInfo.Name))
                {
                    warnMsg += "请输入陪同访客姓名!" + "\r\n";
                }
                if (string.IsNullOrEmpty(item.ViewModel.VisitorInfo.CertificateNumber))
                {
                    warnMsg += "请输入陪同访客身份证!" + "\r\n";
                }
                if (isFace && string.IsNullOrEmpty(item.ViewModel.TakePictureControl.ViewModel.ImageUrl))
                {
                    warnMsg += "请拍照陪同访客照片!" + "\r\n";
                }
                var isBlack = await _blacklistApi.IsBlackAsync(item.ViewModel.VisitorInfo.Name, item.ViewModel.VisitorInfo.CertificateNumber, company.FloorId, company.Id);
                if (isBlack)
                {
                    throw CustomException.Run("抱歉，【" + item.Name + "】疑似黑名单用户，不能登记！");
                }
                //设置陪同访客信息
                var visitor = new VisitorTeamModel
                {
                    IcCard = item.ViewModel.VisitorInfo.IcCardNumber,
                    IdNumber = item.ViewModel.VisitorInfo.CertificateNumber,
                    Name = item.ViewModel.VisitorInfo.Name,
                    FaceImg = item.ViewModel.TakePictureControl.ViewModel.ImageUrl,
                    Gender = item.ViewModel.VisitorInfo.Gender
                };
                ls.Add(visitor);
            }
            mvisitor.retinues = ls;

            //后台提交预约登录
            var lst = await _appointmentApi.SubmitAppoiAsync(mvisitor);

            await Dispatcher.Invoke(async () =>
             {
                 // todo:在次判断是否打印二维码
                 if (mvisitor.authTypes.Contains(AuthModelEnum.QR.Value) && lst.Count > 0)
                 {
                     //CommonHelper.PrintQR(lst); 
                     foreach (var item in lst)
                     {
                         //打印二维码
                         var imageInfo = await _frontBaseApi.GetQrAsync(item.Qr);
                         var bitmap = ImageConvert.BytesToBitmap(imageInfo.Bytes);

                         _smallTicketOperator.Init(PrintConfig.VISIT_QR_CODE_DOCUMENT_PATH, VisitQRCodePrintModel.FromVisitorResult(item, bitmap), new VisitQRCodeRenderer());
                         await _smallTicketOperator.StartPrintAsync();
                     }
                 }
                 var ui = _containerProvider.Resolve<SuccessPage>();
                 _mainFrameHelper.Link(ui, false, ui.PageKey);
             });
        }

        private void Btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            readerTimer.Start();

            //currVistors = null;
            _viewModel.TimeLimitVM.IsOne = true;

            //清理公司
            _viewModel.CompanySelectControl.ResetClear();
            //重新加载操作数据
            _ = _viewModel.RefreshDataAsync();

            _viewModel.Init();
        }
    }
}
