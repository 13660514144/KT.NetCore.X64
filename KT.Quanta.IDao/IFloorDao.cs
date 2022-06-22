using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IFloorDao : IBaseDataDao<FloorEntity>
    {
        Task<FloorEntity> GetByIdAsync(string id);
        Task<List<FloorEntity>> GetAllAsync();
        Task<List<FloorEntity>> GetByEdificeIdAsync(string id);
        Task<List<FloorEntity>> GetWithEdificeByIdsAsync(List<string> destinationFloorIds);
        Task<List<FloorEntity>> GetByIdsAsync(List<string> ids);
    }
}
