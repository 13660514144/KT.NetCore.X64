using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaTurnstilePassRightDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, TurnstilePassRightModel model, FaceInfoModel face);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, TurnstilePassRightModel model);
    }
}