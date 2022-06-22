using KT.Elevator.Manage.Service.Models;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaPassRightDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, PassRightModel model, FaceInfoModel face = null);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, PassRightModel model);
    }
}
