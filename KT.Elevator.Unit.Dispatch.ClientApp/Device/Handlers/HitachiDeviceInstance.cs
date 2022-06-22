using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Device.Unit.Devices;
using KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Device.Handlers
{
    /// <summary>
    /// 接收到卡号操作
    /// </summary>
    public class HitachiDeviceInstance
    {
        //最后派梯楼层名称
        private UnitDispatchHandleElevatorModel _lastHandleElevator;

        private ILogger _logger;
        private AppSettings _appSettings;
        private HitachiSettings _hitachiSettings;
        private IEventAggregator _eventAggregator;
        private HubHelper _hubHelper;

        private HitachiDeviceClient _hitachiDeviceClient;
        private HitachiSendHandler _hitachiSendHandler;

        public HitachiDeviceInstance()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _hitachiSettings = ContainerHelper.Resolve<HitachiSettings>();
            _hitachiDeviceClient = ContainerHelper.Resolve<HitachiDeviceClient>();
            _hitachiSendHandler = ContainerHelper.Resolve<HitachiSendHandler>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
        }

        public async void StartAsync()
        {
            await _hitachiDeviceClient.UpdateAsync(_hitachiSettings);

            _logger.LogInformation($"{_hitachiSettings.PortName} 串口启动成功：{JsonConvert.SerializeObject(_hitachiSettings, JsonUtil.JsonPrintSettings)} ");

            _eventAggregator.GetEvent<AutoHandleElevatorEvent>().Subscribe(AutoHandleElevator);
            _eventAggregator.GetEvent<ManualHandleElevatorEvent>().Subscribe(ManualHandleElevator);
            _eventAggregator.GetEvent<HitachiConfirmSendEvent>().Subscribe(HitachiClientReceive);
            _eventAggregator.GetEvent<HandleElevatorReceiveEvent>().Subscribe(HandleElevatorReceive);

            _eventAggregator.GetEvent<HandledElevatorEvent>().Subscribe(HandledElevator);
        }

        private async void HandleElevatorReceive(HitachiReceiveModel obj)
        {
            if (_lastHandleElevator == null)
            {
                _logger.LogError($"最后派梯数据为空！");
                return;
            }
            var now = DateTimeUtil.UtcNowMillis();
            if ((_lastHandleElevator.PassTime + (_appSettings.SuccessReturnSecondTime * 1000)) < now)
            {
                _logger.LogError($"最后派梯数据结果超时：passTime:{_lastHandleElevator.PassTime} now:{now} ");
                return;
            }

            //返回派梯结果
            var model = new UnitDispatchReceiveHandleElevatorModel();
            model.MessageId = _lastHandleElevator.SendData.MessageId;
            model.CardNumber = _lastHandleElevator.SendData.CardNumber;
            model.DistinationFloorId = _lastHandleElevator.SendData.AutoRealFloorId;
            model.ElevatorName = obj.ElevatorName;
            await _hubHelper.MasterHub.InvokeAsync("HitachiCallResponse", model);

            _logger.LogInformation($"派梯结果返回成功：data:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");
        }

        private void HandledElevator(UnitDispatchHandleElevatorModel obj)
        {
            //更新最后通行记录
            obj.PassTime = DateTimeUtil.UtcNowMillis();
            _lastHandleElevator = obj;
        }

        private void HitachiClientReceive(HitachiReceiveModel obj)
        {
            _hitachiSendHandler.ConfirmSendAsync(obj);
        }

        private void ManualHandleElevator(UnitDispatchSendHandleElevatorModel obj)
        {
            _hitachiSendHandler.DispatchSendAsync(obj);
        }

        private void AutoHandleElevator(UnitDispatchSendHandleElevatorModel obj)
        {
            _hitachiSendHandler.DispatchSendAsync(obj);
        }
    }
}
