using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.DeviceDistributes
{
    /// <summary>
    /// 远程分发数据服务
    /// 对符合的设备分发数据
    /// </summary>
    public interface IPassRightDeviceDistributeService
    {
        Task AddOrUpdateAsync(PassRightModel model, FaceInfoModel face = null);
        Task DeleteAsync(PassRightModel model);
    }
}
