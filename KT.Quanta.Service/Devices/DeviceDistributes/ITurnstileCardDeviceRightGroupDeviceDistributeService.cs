using KT.Quanta.Service.Turnstile.Dtos;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public interface ITurnstileCardDeviceRightGroupDeviceDistributeService
    {
        Task AddOrUpdateAsync(TurnstileCardDeviceRightGroupModel model);
        Task DeleteAsync(TurnstileCardDeviceRightGroupModel model);
    }
}