using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaTurnstileRightGroupDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, TurnstileCardDeviceRightGroupModel model);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, string id, long time);
    }
}