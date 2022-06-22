using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IDistributeErrorDao : IBaseDataDao<DistributeErrorEntity>
    {
        Task DeleteAsync(List<DistributeErrorEntity> entities);
        Task<DistributeErrorEntity> AddAsync(DistributeErrorEntity entity);
        Task<DistributeErrorEntity> EditAsync(DistributeErrorEntity entity);
        Task<List<DistributeErrorEntity>> GetAllAsync();
        Task<DistributeErrorEntity> GetByIdAsync(string id);
    }
}