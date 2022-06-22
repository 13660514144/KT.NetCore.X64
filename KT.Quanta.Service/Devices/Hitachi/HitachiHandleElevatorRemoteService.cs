using KT.Common.Core.Utils;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Hitachi.Models;
using KT.Quanta.Service.Devices.Hitachi.SendDatas;
using KT.Quanta.Service.Devices.Quanta.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hitachi
{
    public class HitachiHandleElevatorRemoteService : IHandleElevatorRemoteService
    {
        private ILogger<HitachiHandleElevatorRemoteService> _logger;
        private IHitachiHandleElevatorSequenceList _hitachiHandleElevatorSequenceList;
        private IHitachiHandleElevatorDistributeDataService _hitachiHandleElevatorDistributeDataService;
        private readonly RemoteDeviceList _remoteDeviceList;

        public HitachiHandleElevatorRemoteService(ILogger<HitachiHandleElevatorRemoteService> logger,
            IHitachiHandleElevatorSequenceList hitachiHandleElevatorSequenceList,
            IHitachiHandleElevatorDistributeDataService hitachiHandleElevatorDistributeDataService,
            RemoteDeviceList remoteDeviceList)
        {
            _logger = logger;
            _hitachiHandleElevatorSequenceList = hitachiHandleElevatorSequenceList;
            _hitachiHandleElevatorDistributeDataService = hitachiHandleElevatorDistributeDataService;
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task DirectCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            var model = new UnitDispatchSendHandleElevatorModel();
            model.MessageId = distributeHandle.MessageId;
            model.CardNumber = Convert.ToUInt32(distributeHandle.CardNumber);
            model.AutoRealFloorId = distributeHandle.DestinationFloor.ToString();

            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                        {
                            return true;
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    await _hitachiHandleElevatorDistributeDataService.DirectCallAsync(remoteDevice.RemoteDeviceInfo, model);
                });

            //存储对列数据，返回结果时根据相应id查询出信息
            var sequence = new HitachiHandleElevatorSequenceModel();
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.DeviceId;
            sequence.FloorName = distributeHandle.DestinationFloorName;
            _hitachiHandleElevatorSequenceList.Add(model.MessageId, sequence);

            _logger.LogInformation($"结束日立派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");
        }

        public async Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            var model = new UnitDispatchSendHandleElevatorModel();
            model.MessageId = distributeHandle.MessageId;
            model.CardNumber = Convert.ToUInt32(distributeHandle.CardNumber);
            model.ManualRealFloorIds = distributeHandle.DestinationFloors;

            await _remoteDeviceList.ExecuteAsync(
                (remoteDevice) =>
                {
                    if (remoteDevice.RemoteDeviceInfo.ParentId == elevatorGroupRemoteDevice.RemoteDeviceInfo.DeviceId)
                    {
                        if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                        {
                            return true;
                        }
                    }
                    return false;
                },
                async (remoteDevice) =>
                {
                    await _hitachiHandleElevatorDistributeDataService.MultiFloorCallAsync(remoteDevice.RemoteDeviceInfo, model);
                });

            //存储对列数据，返回结果时根据相应id查询出信息
            var sequence = new HitachiHandleElevatorSequenceModel();
            sequence.MessageId = distributeHandle.MessageId;
            sequence.HandleElevatorDeviceId = distributeHandle.HandleElevatorDeviceId;
            _hitachiHandleElevatorSequenceList.Add(model.MessageId, sequence);

            _logger.LogInformation($"结束派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");
        }

        public Task RightCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle)
        {
            throw new NotImplementedException();
        }
    }
}
