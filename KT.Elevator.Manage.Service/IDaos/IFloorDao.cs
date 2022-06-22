using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IFloorDao : IBaseDataDao<FloorEntity>
    {
        Task<FloorEntity> GetByIdAsync(string id);
        Task<List<FloorEntity>> GetAllAsync();
        Task<List<FloorEntity>> GetByEdificeIdAsync(string id);
    }
}
