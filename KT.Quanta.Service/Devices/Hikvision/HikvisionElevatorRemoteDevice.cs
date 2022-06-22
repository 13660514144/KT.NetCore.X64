using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public class HikvisionElevatorRemoteDevice : RemoteDevice, IRemoteDevice
    {
        private CommunicateDeviceList _communicateDeviceList;
        private HikvisionElevatorDeviceExecuteQueue _hikvisionDeviceExecuteQueue;
        private readonly ILogger<HikvisionElevatorRemoteDevice> _logger;

        public HikvisionElevatorRemoteDevice(CommunicateDeviceList communicateDeviceList,
            HikvisionElevatorDeviceExecuteQueue hikvisionDeviceExecuteQueue,
           ILogger<HikvisionElevatorRemoteDevice> logger)
        {
            _communicateDeviceList = communicateDeviceList;
            _hikvisionDeviceExecuteQueue = hikvisionDeviceExecuteQueue;
            _logger = logger;
        }

        public override async Task InitAsync(RemoteDeviceModel remoteDeviceInfo)
        {
            RemoteDeviceInfo = remoteDeviceInfo;

            _hikvisionDeviceExecuteQueue.DeviceKey = $"{RemoteDeviceInfo.DeviceType}:{RemoteDeviceInfo.BrandModel}";
            _hikvisionDeviceExecuteQueue.ExecuteActionAsync = ExecuteActionAsync;

            CommunicateDevices = new List<ICommunicateDevice>();
            for (int i = 0; i < RemoteDeviceInfo.CommunicateDeviceInfos.Count; i++)
            {
                var communicateDevice = await _communicateDeviceList.AddOrEditByAddressAndUserAsync(RemoteDeviceInfo.CommunicateDeviceInfos[i], remoteDeviceInfo);
                CommunicateDevices.Add(communicateDevice);

                RemoteDeviceInfo.CommunicateDeviceInfos[i] = communicateDevice.CommunicateDeviceInfo;
            }
        }

        private async Task ExecuteActionAsync(HikvisionDeviceExecuteQueueModel model)
        {
            if (model.DistributeType == HikvisionDeviceExecuteDistributeTypeEnum.AddOrUpdatePassRight.Value)
            {
                var service = (HikvisionElevatorDataRemoteService)ElevatorDataRemoteService;
                await service.ExecuteAddOrUpdatePassRightAsync(this, model);

                _logger.LogInformation($"{RemoteDeviceInfo.DeviceType}:{RemoteDeviceInfo.BrandModel} 海康梯控结束下发设置卡信息：{((HikvisionAddOrUpdatePassRightQuery<PassRightModel>)model.Data).Model.Print()} ");
            }
            else if (model.DistributeType == HikvisionDeviceExecuteDistributeTypeEnum.DeletePassRight.Value)
            {
                var service = (HikvisionElevatorDataRemoteService)ElevatorDataRemoteService;
                await service.ExecuteDeletePassRightAsync(this, model);

                _logger.LogInformation($"{RemoteDeviceInfo.DeviceType}:{RemoteDeviceInfo.BrandModel} 海康梯控结束下发删除卡信息：{((PassRightModel)model.Data).Print()} ");
            }
            else
            {
                _logger.LogError($"{RemoteDeviceInfo.DeviceType}:{RemoteDeviceInfo.BrandModel} 海康梯控找不到下发权限类型：type:{model.DistributeType}");
            }
        }

        public void AddExecuteAsync(HikvisionDeviceExecuteQueueModel model)
        {
            _hikvisionDeviceExecuteQueue.Add(model);
        }

        public async Task<int> GetOutputNumAsync()
        {
            return await ElevatorDataRemoteService.GetOutputNumAsync(this);
        }
    }
}
