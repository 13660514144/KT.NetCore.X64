using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class PassRightDestinationFloorApi : BackendApiBase, IPassRightDestinationFloorApi
    {
        public PassRightDestinationFloorApi(ILogger<PassRightDestinationFloorApi> logger)
            : base(logger)
        {
        }
 
        public async Task<List<PassRightDestinationFloorModel>> GetWithDetailBySignAsync(string sign)
        {
            return await base.GetAsync<List<PassRightDestinationFloorModel>>($"PassRightDestinationFloor/GetWithDetailBySign?sign={sign}");
        }

        public async Task AddOrEditsAsync(List<PassRightDestinationFloorModel> models)
        {
            await base.PostAsync($"PassRightDestinationFloor/AddOrEdits", models);
        }

        public async Task DeleteBySignAsync(PassRightModel model)
        {
            await base.PostAsync($"PassRightDestinationFloor/DeleteBySign", model);
        }

        public async Task DeleteBySignAndElevatorGroupId(PassRightDestinationFloorModel model)
        {
            await base.PostAsync($"PassRightDestinationFloor/DeleteBySignAndElevatorGroupId", model);
        }
    }
}
