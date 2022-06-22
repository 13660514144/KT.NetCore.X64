using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Quanta.Common.Models;
using Prism.Events;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Logging;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Services;

namespace KT.Elevator.Unit.Secondary.ClientApp.Views
{
    public class SuccessControlViewModel : BindableBase, IRegionMemberLifetime
    {
        public bool KeepAlive
        {
            get { return false; }
        }

        //最后派梯楼层名称
        public static UnitHandleElevatorModel LastHandleElevator { get; set; }

        private string _floorName;
        private string _elevatorName;
        private string _messageTitle;
        private string _message;

        private IEventAggregator _eventAggregator;
        private IPassRecordService _passRecordService;
        private AppSettings _appSettings;
        private ConfigHelper _configHelper;
        private ILogger _Log;
        private SpeekVoice _SpeekVoice;
        public SuccessControlViewModel()
        {
            _SpeekVoice = ContainerHelper.Resolve<SpeekVoice>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _passRecordService = ContainerHelper.Resolve<IPassRecordService>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();

            _eventAggregator.GetEvent<CardInputedEvent>().Subscribe(CardInputedAsync);
            _eventAggregator.GetEvent<HandledElevatorEvent>().Subscribe(HandledElevatorAsync);
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<HandledElevatorErrorEvent>().Subscribe(HandledElevatorError);

            _Log = ContainerHelper.Resolve<ILogger>();
        }

        private async void CardInputedAsync(UnitHandleElevatorModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FloorName =  string.Empty;
                ElevatorName = string.Empty;
                MessageTitle = string.Empty;
                Message = "请等待电梯！";
                
            });
            
            await Task.CompletedTask;
        }

        private async void HandledElevatorAsync(UnitHandleElevatorModel handleElevator)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FloorName = handleElevator.DestinationFloorName;
            });

            PassRecordAsync(handleElevator);
            
            await Task.CompletedTask;
        }

        private async void PassRecordAsync(UnitHandleElevatorModel handleElevator)
        {
            var passTime = DateTimeUtil.UtcNowMillis();
            handleElevator.PassTime = passTime;
            _Log.LogInformation($"Send Pass record ={JsonConvert.SerializeObject(handleElevator)}");
            try
            {
                Task.Run(async () =>
                {
                    _passRecordService.PushAsync(handleElevator); // 派梯成功上传
                });
            }
            catch (Exception ex)
            { 
            }
            
            //如果上条记录未上传，直接上传记录，上传通行事件            
            /*if (LastHandleElevator != null && (LastHandleElevator.PassTime + (_appSettings.HandleResultOutSecondTime * 1000)) > passTime)
            {            
                //异步 上传通行事件
                LastHandleElevator.DeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
                LastHandleElevator.DeviceType = _appSettings.DeviceType;

                _passRecordService.PushAsync(LastHandleElevator);
            }*/

            //更新最后通行记录
            //handleElevator.PassTime = passTime;
           LastHandleElevator = handleElevator;

            await Task.CompletedTask;
        }
        
        /// <summary>
        /// 派梯数据回显
        /// </summary>
        /// <param name="handledElevatorSuccess">结果</param>
        private async void HandledElevatorSuccess(HandleElevatorDisplayModel handledElevatorSuccess)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(handledElevatorSuccess.DestinationFloorName))
                {
                    FloorName = handledElevatorSuccess.DestinationFloorName;
                }
                ElevatorName = handledElevatorSuccess.ElevatorName;
                _SpeekVoice.ToSpeek($"请乘坐{ElevatorName}梯");
            });

            //记录为空，不上传事件
            /*
            if (LastHandleElevator == null)
            {
                return;
            }

            //异步 上传通行事件
            LastHandleElevator.DeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
            LastHandleElevator.DeviceType = _appSettings.DeviceType;
            LastHandleElevator.DestinationFloorName = handledElevatorSuccess.DestinationFloorName;
            _passRecordService.PushAsync(LastHandleElevator); //先上传派梯
            */
            LastHandleElevator = null;

            //_eventAggregator.GetEvent<EndHandledElevatorEvent>().Publish();
            
            await Task.CompletedTask;
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="obj"></param>
        private void HandledElevatorError(HandleElevatorErrorModel handleElevatorError)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Message = handleElevatorError.Message;
            });
        }

        public string FloorName
        {
            get
            {
                return _floorName;
            }

            set
            {
                SetProperty(ref _floorName, value);
            }
        }

        public string ElevatorName
        {
            get
            {
                return _elevatorName;
            }

            set
            {
                SetProperty(ref _elevatorName, value);
            }
        }

        public string MessageTitle
        {
            get => _messageTitle;
            set
            {
                SetProperty(ref _messageTitle, value);
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                SetProperty(ref _message, value);
            }
        }
    }
}
