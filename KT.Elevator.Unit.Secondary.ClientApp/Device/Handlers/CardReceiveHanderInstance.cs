using HelperTools;
using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Device;
using KT.Device.Unit.CardReaders.Events;
using KT.Device.Unit.CardReaders.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Views.Controls;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using System.Data;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Device.Handlers
{
    /// <summary>
    /// 接收到卡号操作
    /// </summary>
    public class CardReceiveHanderInstance
    {
        private ILogger _logger;
        private AppSettings _appSettings;
        private IContainerProvider _containerProvider;
        private HubHelper _hubHelper;
        private IDeviceList _deviceList;
        private IEventAggregator _eventAggregator;
        private WarnTipControl _warnTipControl;
        public CardReceiveHanderInstance()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _containerProvider = ContainerHelper.Resolve<IContainerProvider>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _deviceList = ContainerHelper.Resolve<IDeviceList>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

        }
        public Task StartAsync()
        {
            _eventAggregator.GetEvent<CardDeviceAnalyzedEvent>().Subscribe(CardDeviceAnalyzedAsync);

            return Task.CompletedTask;
        }

        private async void CardDeviceAnalyzedAsync(CardDeviceAnalyzedModel data)
        {
            //var device = await _deviceList.GetAsync(data.CardReceive.Address);
            //if (device == null)
            //{
            //    _logger.LogError($"未找到接收的设备。");
            //    return;
            //}
            //await ReceivedAsync((UnitCardDeviceEntity)device, data.CardReceive);
            _logger.LogInformation($"\r\n==>>刷卡原始数据：{JsonConvert.SerializeObject(data)}");
            if (data.CardReceive.IsCheckDate)
            {
                _logger.LogInformation("DateTimeCheck==================================");
                var now = DateTimeUtil.UtcNowSeconds();
                _logger.LogInformation($"DateTimeCheck: now {now} ==================================");
                if (data.CardReceive.StartTime.HasValue && data.CardReceive.StartTime > now)
                {
                    //_logger.LogWarning("二维码起用时间未到：qrDate:{0} now:{1} ", data.CardReceive.StartTime, now);
                    _eventAggregator.GetEvent<WarnTipEvent>().Publish("二维码起用时间未到！");
                    _eventAggregator.GetEvent<NotRightEvent>().Publish();
                    return;
                }
                if (data.CardReceive.EndTime.HasValue && data.CardReceive.EndTime < now)
                {
                    //_logger.LogWarning("二维码过期：qrEndDate:{0} now:{1} ", data.CardReceive.EndTime, now);
                    _eventAggregator.GetEvent<WarnTipEvent>().Publish("二维码过期！");
                    _eventAggregator.GetEvent<NotRightEvent>().Publish();
                    return;
                }
                _logger.LogInformation("DateTimeCheck: end ==================================");
            }
            /*在线获取权限，不走本地*/
            if (data.CardDeviceInfo is UnitCardDeviceEntity unitCardDevice)
            {
                var handleElevator = new UnitHandleElevatorModel();
                handleElevator.DeviceType = unitCardDevice.DeviceType;
                handleElevator.AccessType = data.CardReceive.AccessType;
                handleElevator.HandleElevatorDeviceId = unitCardDevice.HandleDeviceId;
                handleElevator.DeviceId = unitCardDevice.Id;
                handleElevator.Sign = data.CardReceive.CardNumber;

                //通行卡权限
                handleElevator.HandleElevatorRight = new UnitHandleElevatorRightModel();
                handleElevator.HandleElevatorRight.PassRightSign = data.CardReceive.CardNumber;

                _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleElevator);
            }
            else
            {
                _logger.LogError($"接收数据类型错误：type:{data.CardDeviceInfo.GetType().FullName} need:{typeof(UnitCardDeviceEntity).FullName} ");
            }
            

            /*在线获取权限，不走本地*/
            /*
            if (data.CardDeviceInfo is UnitCardDeviceEntity unitCardDevice)
            {
                await ReceivedAsync(unitCardDevice, data.CardReceive);
            }
            else
            {
                _logger.LogError($"接收数据类型错误：type:{data.CardDeviceInfo.GetType().FullName} need:{typeof(UnitCardDeviceEntity).FullName} ");
            }
            */
        }

        public Task ReceivedAsync(UnitCardDeviceEntity cardDevice, CardReceiveModel data)
        {
            if (cardDevice == null)
            {
                _logger.LogError("通行读卡器设备为空！");

                return Task.CompletedTask;
            }

            _logger.LogInformation("刷卡通行：cardNumber:{0} protName:{1} deviceId:{3} data:{4} ",
                 JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings),
                 cardDevice.PortName,
                 cardDevice.Id,
                 JsonConvert.SerializeObject(cardDevice, JsonUtil.JsonPrintSettings));

            _logger.LogInformation("==================================");

            //较验二维码时间
            if (data.IsCheckDate)
            {
                _logger.LogInformation("DateTimeCheck==================================");
                var now = DateTimeUtil.UtcNowSeconds();
                _logger.LogInformation($"DateTimeCheck: now {now} ==================================");
                if (data.StartTime.HasValue && data.StartTime > now)
                {
                    _logger.LogWarning("二维码起用时间未到：qrDate:{0} now:{1} ", data.StartTime, now);

                    return Task.CompletedTask;
                }
                if (data.EndTime.HasValue && data.EndTime < now)
                {
                    _logger.LogWarning("二维码过期：qrEndDate:{0} now:{1} ", data.EndTime, now);

                    return Task.CompletedTask;
                }
                _logger.LogInformation("DateTimeCheck: end ==================================");
            }

            _logger.LogInformation("刷卡通行：cardNumber:{0} cardDeviceId:{1} ", data.CardNumber, cardDevice.Id);

            var handleElevator = new UnitHandleElevatorModel();
            handleElevator.DeviceType = cardDevice.DeviceType;
            handleElevator.AccessType = data.AccessType;
            handleElevator.HandleElevatorDeviceId = cardDevice.HandleDeviceId;
            handleElevator.DeviceId = cardDevice.Id;
            handleElevator.Sign = data.CardNumber;

            //通行卡权限
            handleElevator.HandleElevatorRight = new UnitHandleElevatorRightModel();
            handleElevator.HandleElevatorRight.PassRightSign = data.CardNumber;

            _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleElevator);

            return Task.CompletedTask;
        }
    }
}
