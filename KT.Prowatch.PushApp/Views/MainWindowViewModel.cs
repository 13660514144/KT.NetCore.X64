using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Prowatch.PushApp;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KangTa.Prowatch.PushEventApp
{
    public class MainWindowViewModel : BindableBase
    {
        public ICommand ClearWarnCommand { get; private set; }
        public ICommand BackPushCommand { get; private set; }
        public ICommand FrontPushCommand { get; private set; }

        private ScrollMessageViewModel _scrollMessage;
        private bool _isBack = true;

        //private ILoginUserService _loginUserService;
        private OpenApi _openApi;
        private InitHelper _initHelper;
        private ILogger _logger;
        private ReaderHelper _readerHelper;
        private AppUserSettings _appSettings;

        public MainWindowViewModel()
        {
            //_loginUserService = ContainerHelper.Resolve<ILoginUserService>();
            _openApi = ContainerHelper.Resolve<OpenApi>();
            _initHelper = ContainerHelper.Resolve<InitHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _readerHelper = ContainerHelper.Resolve<ReaderHelper>();
            _appSettings = ContainerHelper.Resolve<AppUserSettings>();

            ScrollMessage = ContainerHelper.Resolve<ScrollMessageViewModel>();

            ClearWarnCommand = new DelegateCommand(ClearWarn);
            BackPushCommand = new DelegateCommand(BackPush);
            FrontPushCommand = new DelegateCommand(FrontPush);

            Init();
        }

        public void Init()
        {
            _initHelper.Init(_appSettings);

            //开启上传事件
            StartMonitor();
        }

        private void ClearWarn()
        {
            ScrollMessage.Clear();
        }

        private void StartMonitor()
        {
            ApiHelper.PWApi.realEventHandler = new RealEventHandler(this.RealEvent);
            bool bRet = ApiHelper.PWApi.StartRecvRealEvent();
            if (!bRet)
            {
                ScrollMessage.InsertTop("开启监听事件失败！");
                return;
            }
            ScrollMessage.InsertTop("开启监听事件成功！");
        }

        private void RealEvent(sPA_Event paEvent)
        {
            //异步上传数据
            RealEventAsync(paEvent);
        }

        private async Task RealEventAsync(sPA_Event paEvent)
        {
            EventData eventData = paEvent.ToModel();

            _logger.LogInformation($"事件接收原始数据：cardNo:{eventData.CardNo} data:{JsonConvert.SerializeObject(eventData, JsonUtil.JsonSettings)} ");
            if (!IsBack)
            {
                ScrollMessage.InsertTop($"事件接收原始数据：cardNo:{eventData.CardNo} data:{JsonConvert.SerializeObject(eventData, JsonUtil.JsonSettings)} ");
            }

            try
            {
                // 上传数据
                await PushDataAsync(eventData);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"上传事件错误：cardNo:{eventData.CardNo} ex:{ex} ");
                ScrollMessage.InsertTop($"上传事件错误：cardNo:{eventData.CardNo} message:{ex.GetInner().Message} ");
            }
        }

        private async Task PushDataAsync(EventData eventData)
        {
            //只上传刷卡授权事件
            if (!_appSettings.PushStatus.Contains(eventData.EvntDesc))
            {
                return;
            }

            //获取读卡器Id
            var equipmentId = _readerHelper.GetReaderIdByLocation(eventData.Location);

            //错误数据
            if (string.IsNullOrEmpty(equipmentId)
                || string.IsNullOrEmpty(eventData.CardNo))
            {
                throw CustomException.Run($"卡号或设备id为空：cardNo:{eventData.CardNo} equipmentId:{equipmentId} ");
            }

            PushPassRecordModel model = new PushPassRecordModel();
            model.File = null;
            model.EquipmentId = equipmentId;
            model.Extra = string.Empty;
            model.EquipmentType = "ACCESS_READER";
            model.AccessType = "IC_CARD";
            model.AccessToken = eventData.CardNo;
            model.AccessDate = eventData.EvntDate;
            model.Remark = eventData.EvntDesc;

            //上传数据
            await _openApi.PushRecord(_appSettings.ServerAddress, model);

            //界面显示 
            if (!IsBack)
            {
                ScrollMessage.InsertTop($"上传数据：cardNo:{eventData.CardNo} data:{JsonConvert.SerializeObject(model, JsonUtil.JsonSettings)}");
            }
        }

        private void BackPush()
        {
            IsBack = true;
        }

        private void FrontPush()
        {
            IsBack = false;
        }


        public ScrollMessageViewModel ScrollMessage
        {
            get
            {
                return _scrollMessage;
            }

            set
            {
                SetProperty(ref _scrollMessage, value);
            }
        }

        public bool IsBack
        {
            get
            {
                return _isBack;
            }

            set
            {
                SetProperty(ref _isBack, value);
            }
        }
    }
}
