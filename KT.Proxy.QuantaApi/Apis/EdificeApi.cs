using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class EdificeApi : BackendApiBase, IEdificeApi
    {
        public EdificeApi(ILogger<EdificeApi> logger) : base(logger)
        {
        }

        public async Task<List<EdificeModel>> GetAllWithFloorWhereHasRealFloorIdAsync()
        {
            var results = await base.GetAsync<List<EdificeModel>>("edifice/allWithFloor");
            if (results?.FirstOrDefault() == null)
            {
                return results;
            }
            return results;
        }

        public async Task<List<EdificeModel>> EditDirectionsAsync(List<EdificeModel> models)
        {
            var results = await base.PostAsync<List<EdificeModel>>("edifice/EditDirections", models);
            if (results?.FirstOrDefault() == null)
            {
                return results;
            }
            return results;
        }
    }
}
