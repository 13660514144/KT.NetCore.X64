using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    /// <summary>
    /// 分发数据
    /// 对符合业务的设备进行分发
    /// </summary>
    public interface IHandleElevatorDeviceDeviceDistributeService
    {
        Task AddOrUpdateAsync(UnitHandleElevatorDeviceModel model);
        Task DeleteAsync(string id);
    }
}
