using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    public class PassRightDeviceDistributeService : IPassRightDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        public PassRightDeviceDistributeService(RemoteDeviceList remoteDeviceList)
        {
            _remoteDeviceList = remoteDeviceList;
        }

        public async Task AddOrUpdateAsync(PassRightModel model, FaceInfoModel face)
        {
            //获取分发设备
            await _remoteDeviceList.ExecAllAsync(async (remoteDevice) =>
            {
                await remoteDevice.AddOrUpdatePassRightAsync(model, face);
            });
        }

        public async Task DeleteAsync(PassRightModel model)
        {
            //获取分发设备
            await _remoteDeviceList.ExecAllAsync(async (remoteDevice) =>
            {
                await remoteDevice.DeletePassRightAsync(model);
            });
        }
    }
}
