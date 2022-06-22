using KT.Common.Core.Exceptions;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Data.Enums;
using KT.Visitor.SelfApp.Helpers;
using KT.Visitor.SelfApp.Public;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.SelfApp.Views.Common
{
    public class MultiRegisterWarnPageViewModel : BindableBase
    {
        public AppointmentModel Appointment { get; set; }

        private VisitorInfoModel _visitorInfo;
        private string _title;
        private string _errorMsg;

        private PrintHandler _printHandler;
        private MainFrameHelper _mainFrameHelper;
        private IVisitorApi _visitorApi;
        private ILogger _logger;

        public MultiRegisterWarnPageViewModel()
        {
            Title = "温馨提示";

            _printHandler = ContainerHelper.Resolve<PrintHandler>();
            _mainFrameHelper = ContainerHelper.Resolve<MainFrameHelper>();
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        public void ResubmitAppoint()
        {
            MaskTipBox.Run(RunSubmitAsync, SuccessSubmit, ErrorSubmit);
        }

        private void ErrorSubmit(Exception ex)
        {
            _logger.LogError($"预约失败：{ex} ");
             var exception = ex.GetInner();
            var UI = ContainerHelper.Resolve<OperateErrorPage>();
            //UI.ViewModel.ErrorMsg = $"预约失败，请重新操作：{exception.Message}";
            UI.ViewModel.ErrorMsg = $"预约失败，请取消当前预约，重新预约";
            UI.ViewModel.Title = "操作提示";
            _mainFrameHelper.Link(UI, false);
        }

        private void SuccessSubmit(List<RegisterResultModel> results)
        {
            // 跳转到成功页面
            var successPage = ContainerHelper.Resolve<OperateSucessPage>();
            successPage.ViewModel.Init(results);

            // 打印二维码
            successPage.LoadedAction += () =>
            {
                // 异步 打印二维码 
                _printHandler.StartPrintAsync(results, Appointment.Once);
            };
            _mainFrameHelper.Link(successPage);
        }

        private async Task<List<RegisterResultModel>> RunSubmitAsync()
        {
            RestResponse S;
            //后台提交预约登录  
            
            if (VisitorInfo.VisitorStatus == VisitStatusEnum.VISITING.Value
              || VisitorInfo.VisitorStatus == VisitStatusEnum.UNVISIT.Value
              || VisitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_VISIT.Value
              || VisitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_AUDIT.Value)
            {
                //await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.FINISH);
                S= _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.FINISH);
            }
            else
            {
                //await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
                S= _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
            }

            //prowatch中可能删除卡存在延迟
            //await Task.Delay(1000);
            _logger.LogInformation($"Content=[{S.Content}]");
            JObject O = JObject.Parse(S.Content);
            //S = _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
            //var results = await _visitorApi.AddDirectAsync(Appointment);
            //return results;
            if ((int)O["code"] == 200)
            {
                var results = await _visitorApi.AddDirectAsync(Appointment);
                return results;
            }
            else
            {
                var errorPage = ContainerHelper.Resolve<OperateErrorPage>();
                errorPage.ViewModel.Title = "温馨提示";
                errorPage.ViewModel.ErrorMsg = "预约不成功，请尝试重新预约!!!！";
                _mainFrameHelper.Link(errorPage, false);
                return null;
                //throw CustomException.Run("预约不成功，请尝试重新预约!!!");
            }
        }

        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }

            set
            {
                SetProperty(ref _errorMsg, value);
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                SetProperty(ref _title, value);
            }
        }

        public VisitorInfoModel VisitorInfo
        {
            get
            {
                return _visitorInfo;
            }

            set
            {
                SetProperty(ref _visitorInfo, value);
            }
        }
    }
}


//public void ResubmitAppoint()
//{
//    var tipBox = new MaskTipBox();
//    tipBox.Show();
//    var startTime = DateTime.Now;
//    Task.Run(async () =>
//    {
//        try
//        {
//            //后台提交预约登录  
//            if (VisitorInfo.VisitorStatus == VisitStatusEnum.VISITING.Value
//              || VisitorInfo.VisitorStatus == VisitStatusEnum.UNVISIT.Value
//              || VisitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_VISIT.Value
//              || VisitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_AUDIT.Value)
//            {
//                await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.FINISH);
//            }
//            else
//            {
//                await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
//            }

//            //prowatch中可能删除卡存在延迟
//            await Task.Delay(1000);

//            var results = await _visitorApi.AddDirectAsync(Appointment);

//            Application.Current.Dispatcher.Invoke(() =>
//            {
//                CloseTipBox(startTime, tipBox, () =>
//                {
//                    // 跳转到成功页面
//                    var successPage = ContainerHelper.Resolve<OperateSucessPage>();
//                    successPage.ViewModel.Init(results);

//                    // 打印二维码
//                    successPage.LoadedAction += () =>
//                    {
//                        _printHandler.StartPrintAsync(results, Appointment.Once);
//                    };
//                    _mainFrameHelper.Link(successPage);
//                });
//            });
//        }
//        catch (Exception ex)
//        {
//            Application.Current.Dispatcher.Invoke(() =>
//            {
//                CloseTipBox(startTime, tipBox, () =>
//                {
//                    string mesg = string.Empty;
//                    if (ex.InnerException != null)
//                    {
//                        mesg = ex.InnerException.Message;
//                    }
//                    else
//                    {
//                        mesg = ex.Message;
//                    }
//                    var UI = ContainerHelper.Resolve<OperateErrorPage>();
//                    UI.ViewModel.ErrorMsg = $"预约失败，请重新操作：{mesg}";
//                    UI.ViewModel.Title = "操作提示";
//                    _mainFrameHelper.Link(UI, false);
//                });
//            });
//        }
//    });
//}

//private void CloseTipBox(DateTime startTime, MaskTipBox tipBox, Action endAction)
//{
//    var times = (DateTime.Now - startTime).TotalSeconds;
//    if (times < 3)
//    {
//        Task.Run(() =>
//        {
//            Thread.Sleep(Convert.ToInt32((3 - times) * 1000));
//            Application.Current.Dispatcher.Invoke(() =>
//            {
//                if (tipBox != null && tipBox.IsActive)
//                {
//                    tipBox.Close();
//                    endAction?.Invoke();
//                }
//            });
//        });
//    }
//    else
//    {
//        if (tipBox != null && tipBox.IsActive)
//        {
//            tipBox.Close();
//            endAction?.Invoke();
//        }
//    }
//}
