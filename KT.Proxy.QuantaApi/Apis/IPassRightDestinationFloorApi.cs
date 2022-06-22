using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IPassRightDestinationFloorApi
    {
        Task AddOrEditsAsync(List<PassRightDestinationFloorModel> models);
        Task DeleteBySignAndElevatorGroupId(PassRightDestinationFloorModel model);
        Task DeleteBySignAsync(PassRightModel model);
        Task<List<PassRightDestinationFloorModel>> GetWithDetailBySignAsync(string sign);
    }
}
