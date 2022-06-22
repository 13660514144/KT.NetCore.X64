using AutoMapper.Configuration.Conventions;
using KT.Elevator.Manage.Service.Devices.Hikvision;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    /// <summary>
    /// 电梯服务数据推送
    /// </summary>
    public interface IElevatorRemoteService
    {
        Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model);
        Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time);
        Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model);
        Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id);
        Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face);
        Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model);
        Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice);
    }
}
