using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Visitor.Interface.Views.Register
{
    public class VisitorRegisterControlViewModel : BindableBase
    {
        private CompanyControl _companyControl;
        private CompanyShowDetailControl _companyShowDetailControl;
        private VisitorInputControl _visitorInputControl;

        private CompanyViewModel _company;
        private IRegion _contentRegion;

        private ILogger _logger;
        private IVisitorApi _visitorApi;
        private IBlacklistApi _blacklistApi;
        private ConfigHelper _configHelper;
        private DialogHelper _dialogHelper;
        private PrintHandler _printHandler;
        private IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;
        private IFunctionApi _functionApi;
        private ArcIdSdkHelper _arcIdCardHelper;

        public VisitorRegisterControlViewModel()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _arcIdCardHelper = ContainerHelper.Resolve<ArcIdSdkHelper>();

            _companyControl = ContainerHelper.Resolve<CompanyControl>();
            _companyShowDetailControl = ContainerHelper.Resolve<CompanyShowDetailControl>();
            _visitorInputControl = ContainerHelper.Resolve<VisitorInputControl>();

            //提交访客登记
            _eventAggregator.GetEvent<SubmitRegisteEvent>().Subscribe(SubmitRegiste);

            //公司选择完成
            _eventAggregator.GetEvent<CompanySelectedEvent>().Subscribe(CompanySelected);
            //公司选中，未确认
            _eventAggregator.GetEvent<CompanyCheckedEvent>().Subscribe(CompanyChecked);
            //取消授权
            _eventAggregator.GetEvent<CancelRegisteEvent>().Subscribe(CancelSubmitRegiste);
            //授权完成
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(RegistedSuccess);
        }

        private void RegistedSuccess()
        {
            _contentRegion.Activate(_companyControl);
        }

        private async void SubmitRegiste(SubmitParameterModel submitParameter)
        {
            await SubmitAsync(submitParameter);
        }

        internal void ViewLoadedInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _contentRegion = _regionManager.Regions[RegionNameHelper.VisitorRegisterContentRegion];

            _contentRegion.Add(_companyControl);
            _contentRegion.Add(_companyShowDetailControl);
            _contentRegion.Add(_visitorInputControl);

            _contentRegion.Activate(_companyControl);
        }

        /// <summary>
        /// 取消授权
        /// </summary>
        private void CancelSubmitRegiste()
        {
            _contentRegion.Activate(_companyControl);
        }

        /// <summary>
        /// 公司选中，未确认
        /// </summary>
        /// <param name="company"></param>
        private void CompanyChecked(CompanyViewModel company)
        {
            if (company.Opening)
            {
                //需要确认选择的公司
                _companyShowDetailControl.ViewModel.Company = company;
                _contentRegion.Activate(_companyShowDetailControl);
                return;
            }

            //不需要确认直接赋值并下一步
            _company = company;
            EndSelectedCompany();
        }

        private void CompanySelected(CompanyViewModel company)
        {
            _company = company;

            if (_company == null)
            {
                _contentRegion.Activate(_companyControl);
            }
            else
            {
                EndSelectedCompany();
            }
        }

        private void EndSelectedCompany()
        {
            //异步 直接跳转到访客输入
            _contentRegion.Activate(_visitorInputControl);
        }

        private async Task SubmitAsync(SubmitParameterModel submitParameter)
        {
            _logger.LogInformation("提交预约4");
            //非空校验
            string warnMsg = null;

            if (_company == null)
            {
                warnMsg = "请选择要访问的公司!" + "\r\n";
            }
            if (string.IsNullOrEmpty(submitParameter.VisitorInfo.Name))
            {
                warnMsg += "请输入主访客姓名!" + "\r\n";
            }
            if (string.IsNullOrEmpty(submitParameter.VisitorInfo.CertificateNumber))
            {
                warnMsg += "请输入主访客证件号码!" + "\r\n";
            }
            if (!string.IsNullOrEmpty(submitParameter.VisitorInfo.Phone)
                && submitParameter.VisitorInfo.Phone.Length != 11)
            {
                warnMsg += "手机号码格式有误!" + "\r\n";
            }
            //来访事由 
            if (submitParameter.VisiteReason == null)
            {
                warnMsg += "请选择来访事由!" + "\r\n";
            }

            if (warnMsg != null)
            {
                throw CustomException.Run(warnMsg);
            }
            _logger.LogInformation("提交预约5");

            //创建接收数据的对象
            AppointmentModel mvisitor = new AppointmentModel();
            //人员基本数据
            mvisitor.Name = submitParameter.VisitorInfo.Name;
            mvisitor.IdNumber = submitParameter.VisitorInfo.CertificateNumber;
            mvisitor.IdType = submitParameter.VisitorInfo.CertificateType;
            mvisitor.Phone = submitParameter.VisitorInfo.Phone;
            mvisitor.Reason = submitParameter.VisiteReason;
            mvisitor.IcCard = submitParameter.VisitorInfo.IcCard;
            mvisitor.Gender = submitParameter.VisitorInfo.Gender;
            //授权类型
            if (_visitorInputControl.R2.IsChecked == true)
            {
                mvisitor.AuthType = "FACE";
                File.WriteAllText("Radio.ini", "type:R2", Encoding.UTF8);
            }
            else if (_visitorInputControl.R3.IsChecked == true)
            {
                mvisitor.AuthType = "QR";
                File.WriteAllText("Radio.ini", "type:R3", Encoding.UTF8);
            }
            else if (_visitorInputControl.R4.IsChecked == true)
            {
                mvisitor.AuthType = "IC";
                File.WriteAllText("Radio.ini", "type:R4", Encoding.UTF8);
            }
            else
            {
                File.WriteAllText("Radio.ini", "type:R1", Encoding.UTF8);
            }

            if (_visitorInputControl.Auth1.IsChecked == true)
            {
                File.WriteAllText("Auth.ini", "Auth1:", Encoding.UTF8);
            }
            else if (_visitorInputControl.Auth2.IsChecked == true)
            {
                File.WriteAllText("Auth.ini", $@"Auth2:{_visitorInputControl.Auth3.Text.Trim()}", Encoding.UTF8);
            }
            //时限
            mvisitor.AccessDate = submitParameter.AuthorizeTimeLimit.Days;
            mvisitor.Once = submitParameter.AuthorizeTimeLimit.IsOne;

            //选择的公司赋值 
            mvisitor.BeVisitCompanyId = _company.Id;
            mvisitor.BeVisitCompanyName = _company.Name;
            mvisitor.BeVisitFloorId = _company.FloorId;
            mvisitor.Unit = _company.Unit;

            _logger.LogInformation("提交预约6");

            //验证黑名单   
            if (!string.IsNullOrEmpty(mvisitor.Phone))
            {
                var checkBack = new CheckBlacklistModel();
                checkBack.Phone = mvisitor.Phone;
                checkBack.IdNumber = string.Empty;
                checkBack.CompanyId = _company.Id;
                checkBack.FloorId = _company.FloorId;

                var isBlack = await _blacklistApi.IsBlackAsync(checkBack);
                if (isBlack)
                {
                    throw CustomException.Run("手机号码疑似黑名单，不能登记！");
                }
            }

            _logger.LogInformation("提交预约7");

            //设置人脸头像，开启人证比对需拍照
            mvisitor.FaceImg = submitParameter.ImageUrl;
            var configParms = await _functionApi.GetConfigParmsAsync();
            if (configParms?.OpenVisitorCheck == true && string.IsNullOrEmpty(mvisitor.FaceImg))
            {
                throw CustomException.Run("开启人证比对需要拍照！");
            }

            _logger.LogInformation("提交预约8");

            //陪同访客
            mvisitor.Retinues = new List<VisitorInfoModel>();
            if (submitParameter.Accompanies != null && submitParameter.Accompanies.FirstOrDefault() != null)
            {
                mvisitor.Retinues.AddRange(submitParameter.Accompanies);
            }

            _logger.LogInformation("提交预约9");

            //校验陪同访客
            foreach (var item in mvisitor.Retinues)
            {
                //不校验主访客 
                if (string.IsNullOrEmpty(item.Name))
                {
                    throw CustomException.Run("请输入陪同访客姓名！");
                }
                if (string.IsNullOrEmpty(item.IdNumber))
                {
                    throw CustomException.Run($"请输入陪同访客【{item.Name}】身份件证！");
                }
                if (configParms?.OpenVisitorCheck == true && string.IsNullOrEmpty(item.FaceImg))
                {
                    throw CustomException.Run("开启人证比对需要拍照！");
                }

                var checkBack = new CheckBlacklistModel();
                checkBack.Phone = item.Phone;
                checkBack.IdNumber = item.IdNumber;
                checkBack.CompanyId = _company.Id;
                checkBack.FloorId = _company.FloorId;

                var isBlack = await _blacklistApi.IsBlackAsync(checkBack);
                if (isBlack)
                {
                    throw CustomException.Run($"陪同访客【{item.Name}】疑似黑名单用户，不能登记！");
                }
            }

            _logger.LogInformation("提交预约10");

            //加人手机号的陪同访客不用校验 
            if (submitParameter.PhotoAccompanies != null && submitParameter.PhotoAccompanies.FirstOrDefault() != null)
            {
                mvisitor.Retinues.AddRange(submitParameter.PhotoAccompanies);
            }
            //加人数的陪同访客不用校验 
            else if (submitParameter.NumAccompanies != null && submitParameter.NumAccompanies.FirstOrDefault() != null)
            {
                mvisitor.Retinues.AddRange(submitParameter.NumAccompanies);
            }

            _logger.LogInformation("提交预约11");
            _logger.LogInformation($"访客登记初始数据：{JsonConvert.SerializeObject(submitParameter, JsonUtil.JsonPrintSettings)} ");

            //后台提交预约登录
            var addRresult = await _visitorApi.AddAsync(mvisitor);

            List<RegisterResultModel> registerResults;
            Application.Current.Dispatcher.Invoke(() =>
            {
                //已经存在访客预约记录，取消后才能重复登记
                if (addRresult.Code == 50001)
                {
                    _logger.LogInformation("提交预约12");

                    var warnWindow = ContainerHelper.Resolve<MultiRegisterWarnWindow>();
                    warnWindow.ViewModel.Appointment = mvisitor;
                    warnWindow.ViewModel.VisitorInfo = JsonConvert.DeserializeObject<VisitorInfoModel>(JsonConvert.SerializeObject(addRresult.Data, JsonUtil.JsonSettings), JsonUtil.JsonSettings);

                    var warnResult = _dialogHelper.ShowDialog(warnWindow);

                    //关闭窗口取消操作 
                    if (!warnResult.HasValue || !warnResult.Value)
                    {
                        //取消授权
                        if (warnWindow.ViewModel.IsCancel)
                        {
                            //授权完成 
                            _contentRegion.Activate(_companyControl);
                            _eventAggregator.GetEvent<RegistedSuccessEvent>().Publish();
                            return;
                        }
                        else
                        {
                            //保留在授权页面
                            _eventAggregator.GetEvent<CancelSubmitEvent>().Publish();
                            return;
                        }
                    }

                    registerResults = warnWindow.ViewModel.RegisterResults;
                }
                else
                {
                    registerResults = JsonConvert.DeserializeObject<List<RegisterResultModel>>(JsonConvert.SerializeObject(addRresult.Data, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
                }

                _logger.LogInformation("提交预约13");

                // 异步 打印二维码 
                //if (_visitorInputControl.R3.IsChecked == true)
                //{
                    _printHandler.StartPrintAsync(registerResults, mvisitor.Once);
                //}

                var integrateSuccessWindow = ContainerHelper.Resolve<SuccessWindow>();
                integrateSuccessWindow.ViewModel.Init(registerResults);
                _dialogHelper.ShowDialog(integrateSuccessWindow);

                //授权完成   
                _contentRegion.Activate(_companyControl);
                _eventAggregator.GetEvent<RegistedSuccessEvent>().Publish();

                _logger.LogInformation("提交预约14");
            });
        }
    }
}
