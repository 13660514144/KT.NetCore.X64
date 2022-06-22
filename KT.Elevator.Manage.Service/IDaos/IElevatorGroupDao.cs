using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IElevatorGroupDao : IBaseDataDao<ElevatorGroupEntity>
    {
        Task<ElevatorGroupEntity> EditAsync(ElevatorGroupEntity entity);
        Task<ElevatorGroupEntity> AddAsync(ElevatorGroupEntity entity);
        Task<ElevatorGroupEntity> GetByIdAsync(string id);
        Task<List<ElevatorGroupEntity>> GetAllAsync();
        Task<List<ElevatorGroupEntity>> GetAllWithElevatorInfosAsync();
        Task<ElevatorGroupEntity> GetWithElevatorInfosByIdAsync(string id);
    }
}
