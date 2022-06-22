using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class ElevatorGroupApi : BackendApiBase, IElevatorGroupApi
    {
        public ElevatorGroupApi(ILogger<ElevatorGroupApi> logger) : base(logger)
        {
        }

        public Task<List<ElevatorGroupModel>> GetAllAsync()
        {
            var results = base.GetAsync<List<ElevatorGroupModel>>("elevatorGroup/all");
            return results;
        }

        public Task<List<ElevatorGroupModel>> GetAllWithFloorAsync()
        {
            var results = base.GetAsync<List<ElevatorGroupModel>>("elevatorGroup/allWithFloors");
            return results;
        }
    }
}
