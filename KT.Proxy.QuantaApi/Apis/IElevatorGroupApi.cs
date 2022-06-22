using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IElevatorGroupApi
    {
        Task<List<ElevatorGroupModel>> GetAllWithFloorAsync();
        Task<List<ElevatorGroupModel>> GetAllAsync();
    }
}
