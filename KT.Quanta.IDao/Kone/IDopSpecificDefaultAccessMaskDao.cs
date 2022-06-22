using KT.Common.Data.Daos;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IDopSpecificDefaultAccessMaskDao : IBaseDataDao<DopSpecificDefaultAccessMaskEntity>
    {
        Task<List<DopSpecificDefaultAccessMaskEntity>> GetWithFloorsByGroupIdAsync(string elevatorGroupId);
        Task<DopSpecificDefaultAccessMaskEntity> GetWithFloorsByIdAsync(string id);
        Task<List<DopSpecificDefaultAccessMaskEntity>> GetAllAsync();
        Task<List<DopSpecificDefaultAccessMaskEntity>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync();
    }
}
