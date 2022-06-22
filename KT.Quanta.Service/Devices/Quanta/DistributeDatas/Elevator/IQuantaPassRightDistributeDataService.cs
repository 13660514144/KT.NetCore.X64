using KT.Quanta.Service.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaPassRightDistributeDataService
    {
        Task AddOrUpdateAsync(RemoteDeviceModel remoteDevice, PassRightModel model, FaceInfoModel face = null);
        Task DeleteAsync(RemoteDeviceModel remoteDevice, PassRightModel model);
    }
}
