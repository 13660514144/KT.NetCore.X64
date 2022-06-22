using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.Kone;
using KT.Elevator.Manage.Service.Devices.Kone.Models;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.IServices;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public class QuantaDisplayDistributeDataService:IQuantaDisplayDistributeDataService
    {
        private IHubContext<DistributeHub> _distributeHub;
        private RemoteDeviceList _remoteDeviceList;
        private ILogger<QuantaDisplayDistributeDataService> _logger;
        private IFloorService _floorService;
        private IServiceScopeFactory _serviceScopeFactory;
        private KoneHandleDeviceList _koneHandleDeviceList;
        private IElevatorInfoService _elevatorInfoService;

        public QuantaDisplayDistributeDataService(IHubContext<DistributeHub> distributeHub,
            RemoteDeviceList processorDeviceList,
            ILogger<QuantaDisplayDistributeDataService> logger,
            IFloorService floorService,
            IServiceScopeFactory serviceScopeFactory,
            KoneHandleDeviceList koneHandleDeviceList,
            IElevatorInfoService elevatorInfoService)
        {
            _distributeHub = distributeHub;
            _remoteDeviceList = processorDeviceList;
            _logger = logger;
            _floorService = floorService;
            _serviceScopeFactory = serviceScopeFactory;
            _koneHandleDeviceList = koneHandleDeviceList;
            _elevatorInfoService = elevatorInfoService;
        }

        public async Task HandleElevatorSuccess(MsgPaddle paddle)
        {
            _logger.LogInformation($"派梯结果回调： paddle:{ JsonConvert.SerializeObject(paddle, JsonUtil.JsonSettings)} ");

            var remoteDevice = await _remoteDeviceList.GetByTerminalIdAsync(Convert.ToUInt16(paddle.terminal_Id));

            //楼层真实id可能重复，从派梯设备中查找楼层
            var device = _koneHandleDeviceList.GetById(remoteDevice.RemoteDeviceInfo.DeviceId);
            var destinationId = paddle?.destination_floor.ToString();
            var floors = device.HandleElevatorDevice.ElevatorGroup.Floors.Where(x => x.RealFloorId == destinationId).ToList();

            //较验派梯楼层，防止配置出错
            if (floors == null || floors.FirstOrDefault() == null)
            {
                throw CustomException.Run($"派梯结果找不到楼层：destinationId{destinationId} ");
            }
            if (floors.Count > 1)
            {
                throw CustomException.Run($"派梯结果找不到楼层：destinationId{destinationId} ");
            }
            if (paddle?.serving_elevator_id == null)
            {
                throw CustomException.Run($"派梯结果电梯为空：destinationId{destinationId} ");
            }
            if (remoteDevice?.RemoteDeviceInfo == null || !remoteDevice.RemoteDeviceInfo.IsOnline)
            {
                throw CustomException.Run($"派梯设备不在线或不存在：remoteDevice:{(remoteDevice.RemoteDeviceInfo == null ? string.Empty : JsonConvert.SerializeObject(remoteDevice.RemoteDeviceInfo, JsonUtil.JsonSettings))} ");
            }

            //不同类型不同传送方式
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value
                || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
            {
                var result = new HandledElevatorSuccessModel();
                result.DestinationFloorName = floors.FirstOrDefault().Name;

                //获取电梯信息
                var elevatorRealId = paddle.serving_elevator_id.ToString();
                var elevatorInfo = await _elevatorInfoService.GetByRealIdAsync(elevatorRealId);
                result.ElevatorId = elevatorInfo?.Id;
                result.ElevatorName = string.IsNullOrEmpty(elevatorInfo.Name) ? elevatorRealId : elevatorInfo.Name;

                await _distributeHub.Clients.Client(remoteDevice.RemoteDeviceInfo.ConnectionId).SendAsync("HandleElevatorSuccess", result);

                _logger.LogInformation($"派梯结果回调成功：result:{ JsonConvert.SerializeObject(paddle, JsonUtil.JsonSettings)} ");
            }

        }
    }
}
