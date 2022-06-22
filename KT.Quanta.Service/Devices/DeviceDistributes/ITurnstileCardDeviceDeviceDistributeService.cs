using KT.Quanta.Service.Turnstile.Dtos;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public interface ITurnstileCardDeviceDeviceDistributeService
    {
        Task AddOrUpdateAsync(TurnstileCardDeviceModel model);
        Task DeleteAsync(string id, long time);
    }
}