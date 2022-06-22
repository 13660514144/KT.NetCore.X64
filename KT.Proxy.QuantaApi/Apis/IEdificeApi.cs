using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IEdificeApi
    {
        Task<List<EdificeModel>> EditDirectionsAsync(List<EdificeModel> models);
        Task<List<EdificeModel>> GetAllWithFloorWhereHasRealFloorIdAsync();
    }
}
