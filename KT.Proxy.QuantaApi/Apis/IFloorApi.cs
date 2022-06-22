using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IFloorApi
    {
        Task<List<FloorModel>> EditDirectionsAsync(List<FloorModel> floors);
    }
}
