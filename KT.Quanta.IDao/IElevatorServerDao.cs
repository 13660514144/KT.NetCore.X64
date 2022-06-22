using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IElevatorServerDao : IBaseDataDao<ElevatorServerEntity>
    {
        Task<ElevatorServerEntity> AddAsync(ElevatorServerEntity entity);
        Task<ElevatorServerEntity> EditAsync(ElevatorServerEntity entity);
        Task<ElevatorServerEntity> GetByIdAsync(string id);
        Task<List<ElevatorServerEntity>> GetAllAsync();
    }
}
