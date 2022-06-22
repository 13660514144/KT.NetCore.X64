using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaCardDeviceDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, CardDeviceModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time);
    }
}
