using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IEdificeDao : IBaseDataDao<EdificeEntity>
    {
        Task<EdificeEntity> AddWidthFloorAsync(EdificeEntity entity);
        Task<EdificeEntity> EditWidthFloorAsync(EdificeEntity entity);
        Task RemoveWithFloorByIdAsync(string id);
        Task<List<EdificeEntity>> GetAllWithFloorAsync();
        Task<EdificeEntity> GetWithFloorByIdAsync(string id);
        Task<List<EdificeEntity>> GeWithFloorByIdsAsync(List<string> ids);
    }
}
