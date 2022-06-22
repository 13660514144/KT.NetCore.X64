using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IHandleElevatorDeviceApi
    {
        Task<List<HandleElevatorDeviceModel>> GetAllAsync();
        Task<List<HandleElevatorDeviceModel>> GetByElevatorGroupIdAsync(string elevatorGroupId);
    }
}
