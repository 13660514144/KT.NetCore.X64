using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    /// <summary>
    /// 远程设备
    /// </summary>
    public interface IRemoteDevice
    {
        /// <summary>
        /// 远程设备数据
        /// </summary>
        RemoteDeviceModel RemoteDeviceInfo { get; }

        /// <summary>
        /// 登录客户端对象
        /// </summary>
        object LoginUserClient { get; }

        /// <summary>
        /// 包含的远程服务合集
        /// 远程设备分发到包含的服务，需在分发方法中调用
        /// </summary>
        RemoteServiceModel RemoteService { get; }

        void Close();
        void Heartbeat(string key);
        void Init(RemoteDeviceModel remoteDevice);

        /// <summary>
        /// 查检是否连接，如果未连接则连接
        /// </summary>
        /// <returns>最络连接状态</returns>
        Task<bool> CheckAndLinkAsync();
        Task AddOrUpdateCardDeviceAsync(CardDeviceModel model);
        Task DeleteCardDeviceAsync(string id, long time);
        Task AddOrUpdateHandleElevatorDeviceAsync(UnitHandleElevatorDeviceModel model);
        Task DeleteHandleElevatorDeviceAsync(string id);
        Task AddOrUpdatePassRightAsync(PassRightModel model, FaceInfoModel face);
        Task DeletePassRightAsync(PassRightModel model);
        Task<int> GetOutputNumAsync();
    }
}
