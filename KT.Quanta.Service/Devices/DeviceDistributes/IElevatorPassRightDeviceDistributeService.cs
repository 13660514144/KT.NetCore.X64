using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    /// <summary>
    /// 远程分发数据服务
    /// 对符合的设备分发数据
    /// </summary>
    public interface IElevatorPassRightDeviceDistributeService
    {
        Task AddOrUpdateAsync(PassRightModel model, FaceInfoModel face = null, PassRightModel oldModel = null);
        Task DeleteAsync(PassRightModel model);
    }
}
