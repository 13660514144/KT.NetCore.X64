using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaTurnstileCardDeviceDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, TurnstileCardDeviceModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time);
    }
}
