using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IEdificeDao : IBaseDataDao<EdificeEntity>
    {
        Task<EdificeEntity> AddWidthFloorAsync(EdificeEntity entity);
        Task<EdificeEntity> EditWidthFloorAsync(EdificeEntity entity);
        Task RemoveWithFloorByIdAsync(string id);
        Task<List<EdificeEntity>> GetAllWithFloorAsync();
        Task<EdificeEntity> GetWithFloorByIdAsync(string id);
    }
}
