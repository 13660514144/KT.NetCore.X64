using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    public class HandleElevatorDeviceDeviceDistributeService : IHandleElevatorDeviceDeviceDistributeService
    {
        private RemoteDeviceList _remoteDeviceList;
        private DistributeHelper _distributeHelper;
        public HandleElevatorDeviceDeviceDistributeService(RemoteDeviceList remoteDeviceList,
            DistributeHelper distributeHelper)
        {
            _remoteDeviceList = remoteDeviceList;
            _distributeHelper = distributeHelper;
        }

        public async Task AddOrUpdateAsync(UnitHandleElevatorDeviceModel model)
        {
            //获取分发设备 
            await _remoteDeviceList.ExecByWhereAsync(x =>
            {
                return x.RemoteDeviceInfo.DeviceId != model.Id
                  || x.RemoteDeviceInfo.DeviceType != DeviceTypeEnum.ELEVATOR_SECONDARY.Value;
            },
            async (remoteDevice) =>
            {
                await remoteDevice.AddOrUpdateHandleElevatorDeviceAsync(model);
            });
        }

        public async Task DeleteAsync(string id)
        {
            //获取分发设备 
            await _remoteDeviceList.ExecByWhereAsync(x =>
            {
                return x.RemoteDeviceInfo.DeviceId != id
                  || x.RemoteDeviceInfo.DeviceType != DeviceTypeEnum.ELEVATOR_SECONDARY.Value;
            },
            async (remoteDevice) =>
            {
                await remoteDevice.DeleteHandleElevatorDeviceAsync(id);
            });
        }

        
    }
}
