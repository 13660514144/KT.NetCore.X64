using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IPassRightAccessibleFloorApi
    {
        Task AddOrEditsAsync(List<PassRightAccessibleFloorModel> models);
        Task DeleteBySignAndElevatorGroupId(PassRightAccessibleFloorModel model);
        Task DeleteBySignAsync(PassRightModel model);
        Task<List<PassRightAccessibleFloorModel>> GetWithDetailBySignAsync(string sign);
    }
}
