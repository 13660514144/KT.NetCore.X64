using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public class ElevatorCardDeviceDeviceDistributeService : IElevatorCardDeviceDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public ElevatorCardDeviceDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task AddOrUpdateAsync(CardDeviceModel model)
        {
            //获取分发设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(model.Processor.Id);
            if (remoteDevice?.ElevatorDataRemoteService != null)
            {
                var isDistribute = await DistributeWhere(remoteDevice);
                if (isDistribute)
                {
                    await remoteDevice.ElevatorDataRemoteService?.AddOrUpdateCardDeviceAsync(remoteDevice, model);
                }
            }
        }

        private Task<bool> DistributeWhere(IRemoteDevice remoteDevice)
        {
            if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value
                || remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value
                || remoteDevice.RemoteDeviceInfo.DeviceType==DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_3600P.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel==BrandModelEnum.QIACS_DLS81.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel==BrandModelEnum.HITACHI_ELE_3600.Value)
                {
                    return Task.FromResult(true);
                }
            }
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                {
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        public async Task DeleteAsync(string remoteDeviceId, string id, long time)
        {
            //获取分发设备
            var remoteDevice = await _remoteDeviceList.GetByIdAsync(remoteDeviceId);
            if (remoteDevice?.ElevatorDataRemoteService != null)
            {
                var isDistribute = await DistributeWhere(remoteDevice);
                if (isDistribute)
                {
                    await remoteDevice.ElevatorDataRemoteService?.DeleteCardDeviceAsync(remoteDevice, id, time);
                }
            }
        }
    }
}
