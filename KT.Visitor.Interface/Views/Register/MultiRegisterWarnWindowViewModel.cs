using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Enums;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Data.Enums;
using KT.Visitor.Interface.Events;
using Prism.Events;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Common.Core.Exceptions;

namespace KT.Visitor.Interface.Views.Register
{
    public class MultiRegisterWarnWindowViewModel : BindableBase
    {
        public AppointmentModel Appointment { get; set; }
        public List<RegisterResultModel> RegisterResults { get; set; }

        //private ICommand _cancelCommand;
        //public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));

        //private ICommand _confirmCommand;
        //public ICommand ConfirmCommand => _confirmCommand ?? (_confirmCommand = new DelegateCommand(Confirm));

        private VisitorInfoModel _visitorInfo;
        private bool _isCancel;
        private bool _isSubmiting;
        
        private IVisitorApi _visitorApi;
        private IEventAggregator _eventAggregator;
        public MultiRegisterWarnWindowViewModel()
        {
            _visitorApi = ContainerHelper.Resolve<IVisitorApi>();

            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(RegistedSuccess);
        }

        private void RegistedSuccess()
        {
            IsSubmiting = false;
            _eventAggregator.GetEvent<ExceptionEvent>().Unsubscribe(Error);
        }

        private void Error(Exception obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsSubmiting = false;
            });
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

        public bool IsCancel
        {
            get
            {
                return _isCancel;
            }

            set
            {
                _isCancel = value;
            }
        }

        public bool IsSubmiting
        {
            get => _isSubmiting;
            set
            {
                SetProperty(ref _isSubmiting, value);
            }
        }

        public async Task ReappointAsync()
        {
            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(Error);
            IsSubmiting = true;
            RestResponse S;
            /*if (VisitorInfo.VisitorStatus == VisitStatusEnum.VISITING.Value
              || VisitorInfo.VisitorStatus == VisitStatusEnum.UNVISIT.Value
              || VisitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_VISIT.Value
              || VisitorInfo.VisitorStatus == VisitStatusEnum.OUTTIME_AUDIT.Value)
            {
                //await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.FINISH);
                S=  _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.FINISH);
            }
            else
            {
                //await _visitorApi.OperateAsync(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
                S = _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
            }
            */
            //prowatch中可能删除卡存在延迟
            //await Task.Delay(1000);
            //同步等待
            JObject O = new JObject();
            //S = _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);

            S = _visitorApi.OperateWait(VisitorInfo.Id, VisitOperateTypeEnum.CANCEL);
            RegisterResults = await _visitorApi.AddDirectAsync(Appointment);
            /*if ((int)O["code"] == 200)
            {
                RegisterResults = await _visitorApi.AddDirectAsync(Appointment);
            }
            else
            {
                throw CustomException.Run("预约不成功，请尝试重新预约!!!");
            }*/
        }        
    }
}
