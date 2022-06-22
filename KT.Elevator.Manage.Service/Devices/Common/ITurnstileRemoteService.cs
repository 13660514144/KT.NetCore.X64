using KT.Elevator.Manage.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    /// <summary>
    /// 闸机数据推送服务
    /// </summary>
    public interface ITurnstileRemoteService
    {
        Task AddOrUpdateCardDeviceAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time);
    }
}
