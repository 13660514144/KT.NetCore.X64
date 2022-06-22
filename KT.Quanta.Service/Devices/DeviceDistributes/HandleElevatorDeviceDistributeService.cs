using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Schindler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    /// <summary>
    /// 派梯分发
    /// </summary>
    public class HandleElevatorDeviceDistributeService : IHandleElevatorDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public HandleElevatorDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task HandleAsync(string elevatorGroupId, DistributeHandleElevatorModel distributeHandle)
        {
            //获取分发设备            
            await _remoteDeviceList.AsyncExecuteAsync(
                 (remoteDevice) =>
                 {
                     return DistributeWhere(elevatorGroupId, remoteDevice);
                 },
                async (remoteDevice) =>
                {
                    await remoteDevice.HandleElevatorRemoteService.DirectCallAsync(remoteDevice, distributeHandle);
                });
        }

        private bool DistributeWhere(string elevatorGroupId, IRemoteDevice remoteDevice)
        {
            if (remoteDevice.RemoteDeviceInfo.DeviceId != elevatorGroupId)
            {
                return false;
            }
            //传递传递电梯组
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.SCHINDLER_PORT.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_DLS81.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel==BrandModelEnum.HITACHI_ELE_3600.Value)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task MultiFloorHandleAsync(string elevatorGroupId, DistributeMultiFloorHandleElevatorModel distributeHandle)
        {
            //获取分发设备
            await _remoteDeviceList.AsyncExecuteAsync(
                 (remoteDevice) =>
                 {
                     return DistributeMultiFloorHandleWhere(elevatorGroupId, remoteDevice);
                 },
                async (remoteDevice) =>
                {
                    await remoteDevice.HandleElevatorRemoteService.MultiFloorCallAsync(remoteDevice, distributeHandle);
                });
        }

        private bool DistributeMultiFloorHandleWhere(string elevatorGroupId, IRemoteDevice remoteDevice)
        {
            if (remoteDevice.RemoteDeviceInfo.DeviceId != elevatorGroupId)
            {
                return false;
            }
            //传递传递电梯组
            else if (remoteDevice.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value)
            {
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_DFRS.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.MITSUBISHI_ELIP.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.KONE_DCS.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.SCHINDLER_PORT.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.QSCS_DLS81.Value
                 || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HITACHI_ELE_3600.Value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
