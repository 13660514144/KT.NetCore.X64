using KT.Common.Data.Models;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class PassRightAccessibleFloorApi : BackendApiBase, IPassRightAccessibleFloorApi
    {
        public PassRightAccessibleFloorApi(ILogger<PassRightAccessibleFloorApi> logger)
            : base(logger)
        {
        }
 
        public async Task<List<PassRightAccessibleFloorModel>> GetWithDetailBySignAsync(string sign)
        {
            return await base.GetAsync<List<PassRightAccessibleFloorModel>>($"PassRightAccessibleFloor/GetWithDetailBySign?sign={sign}");
        }

        public async Task AddOrEditsAsync(List<PassRightAccessibleFloorModel> models)
        {
            await base.PostAsync($"PassRightAccessibleFloor/AddOrEdits", models);
        }

        public async Task DeleteBySignAsync(PassRightModel model)
        {
            await base.PostAsync($"PassRightAccessibleFloor/DeleteBySign", model);
        }

        public async Task DeleteBySignAndElevatorGroupId(PassRightAccessibleFloorModel model)
        {
            await base.PostAsync($"PassRightAccessibleFloor/DeleteBySignAndElevatorGroupId", model);
        }
    }
}
