using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class FloorApi : BackendApiBase, IFloorApi
    {
        public FloorApi(ILogger<FloorApi> logger) : base(logger)
        {
        }

        public async Task<List<FloorModel>> EditDirectionsAsync(List<FloorModel> floors)
        {
            var results = await base.PostAsync<List<FloorModel>>("floor/editDirections", floors);

            if (results?.FirstOrDefault() == null)
            {
                return results;
            }

            return results;
        }
    }
}
