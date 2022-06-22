using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Service.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 电梯服务数据推送
    /// </summary>
    public interface IElevatorDataRemoteService
    {
        Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model);
        Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time);
        Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model);
        Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id);
        Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face, PassRightModel oldModel );
        Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model);
        Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice);
    }
}
