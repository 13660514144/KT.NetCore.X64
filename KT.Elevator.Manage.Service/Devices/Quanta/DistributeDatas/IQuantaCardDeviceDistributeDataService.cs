using KT.Elevator.Manage.Service.Models;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaCardDeviceDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time);
    }
}
