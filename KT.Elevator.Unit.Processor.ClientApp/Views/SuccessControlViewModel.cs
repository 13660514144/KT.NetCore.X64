using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Quanta.Common.Models;
using Prism.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Elevator.Unit.Processor.ClientApp.Views
{
    public class SuccessControlViewModel : BindableBase
    {
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

        public SuccessControlViewModel()
        {
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _passRecordService = ContainerHelper.Resolve<IPassRecordService>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();

            _eventAggregator.GetEvent<CardInputedEvent>().Subscribe(CardInputedAsync);
            _eventAggregator.GetEvent<HandledElevatorEvent>().Subscribe(HandledElevatorAsync);
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Subscribe(HandledElevatorSuccess);
            _eventAggregator.GetEvent<HandleElevatorStatusEvent>().Subscribe(HandleElevatorStatus);
            _eventAggregator.GetEvent<HandledElevatorErrorEvent>().Subscribe(HandledElevatorError);
        }

        private async void CardInputedAsync(UnitHandleElevatorModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                FloorName = string.Empty;
            });

            await Task.CompletedTask;
        }
        private List<string> _destinationFloorIds;
        private async void HandledElevatorAsync(UnitHandleElevatorModel handleElevator)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _destinationFloorIds = handleElevator.DestinationFloorIds;
                MessageTitle = "正在派梯";
                FloorName = handleElevator.DestinationFloorName;
                Message = string.Empty;
                ElevatorName = string.Empty;
                if (string.IsNullOrEmpty(handleElevator.DestinationFloorIds?.FirstOrDefault()))
                {
                    MessageTitle = "派梯失败";
                    Message = "抱歉，您的楼层权限未录入！";
                }
            });

            PassRecordAsync(handleElevator);

            await Task.CompletedTask;
        }

        private async void PassRecordAsync(UnitHandleElevatorModel handleElevator)
        {
            var passTime = DateTimeUtil.UtcNowMillis();
            //如果上条记录未上传，直接上传记录，上传通行事件
            if (LastHandleElevator != null && (LastHandleElevator.PassTime + (_appSettings.HandleResultOutSecondTime * 1000)) > passTime)
            {
                //异步 上传通行事件
                LastHandleElevator.DeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
                LastHandleElevator.DeviceType = _appSettings.DeviceType;
                _passRecordService.PushAsync(LastHandleElevator);
            }

            //更新最后通行记录
            handleElevator.PassTime = passTime;
            LastHandleElevator = handleElevator;

            await Task.CompletedTask;
        }
        /// <summary>
        /// 派梯数据回显
        /// </summary>
        /// <param name="handledElevatorSuccess">结果</param>
        private async void HandleElevatorStatus(HandleElevatorDisplayModel handledElevatorSuccess)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (string.IsNullOrEmpty(handledElevatorSuccess.DestinationFloorName)
                && !string.IsNullOrEmpty(_destinationFloorIds?.FirstOrDefault()))
                {
                    MessageTitle = "请选择目的楼层";
                }
                else
                {
                    MessageTitle = "派梯结束";
                    FloorName = handledElevatorSuccess.DestinationFloorName;
                }
            });

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
                if (string.IsNullOrEmpty(handledElevatorSuccess.DestinationFloorName)
                && string.IsNullOrEmpty(handledElevatorSuccess.ElevatorName))
                {
                    MessageTitle = "派梯失败";
                    Message = "未择目的楼层或电梯未派梯";
                }
                else
                {
                    MessageTitle = "派梯完成";
                    FloorName = handledElevatorSuccess.DestinationFloorName;
                    ElevatorName = handledElevatorSuccess.ElevatorName;
                }
            });

            //记录为空，不上传事件
            if (LastHandleElevator == null)
            {
                return;
            }

            //异步 上传通行事件
            LastHandleElevator.DeviceId = _configHelper.LocalConfig.HandleElevatorDeviceId;
            LastHandleElevator.DeviceType = _appSettings.DeviceType;
            LastHandleElevator.DestinationFloorName = handledElevatorSuccess.DestinationFloorName;
            _passRecordService.PushAsync(LastHandleElevator);

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
            if (string.IsNullOrEmpty(_destinationFloorIds?.FirstOrDefault()))
            {
                return;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageTitle = "派梯结束";
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

        public string ElevatorName
        {
            get => _elevatorName;
            set
            {
                SetProperty(ref _elevatorName, value);
            }
        }
    }
}
