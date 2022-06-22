using KT.Common.Data.Daos;
using KT.Quanta.Entity.Kone;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IDopGlobalDefaultAccessMaskDao : IBaseDataDao<DopGlobalDefaultAccessMaskEntity>
    {
        Task<List<DopGlobalDefaultAccessMaskEntity>> GetWithFloorsByGroupIdAsync(string elevatorGroupId);
        Task<DopGlobalDefaultAccessMaskEntity> GetWithFloorsByIdAsync(string id);
        Task<List<DopGlobalDefaultAccessMaskEntity>> GetAllAsync(); 
        Task<List<DopGlobalDefaultAccessMaskEntity>> GetAllDopGlobalMasksWithElevatorGroupAsync();
    }
}
