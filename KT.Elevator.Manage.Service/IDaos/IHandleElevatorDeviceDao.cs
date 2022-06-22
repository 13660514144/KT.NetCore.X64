using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IHandleElevatorDeviceDao : IBaseDataDao<HandleElevatorDeviceEntity>
    {
        Task<HandleElevatorDeviceEntity> AddAsync(HandleElevatorDeviceEntity entity);
        Task<HandleElevatorDeviceEntity> EditAsync(HandleElevatorDeviceEntity entity);
        Task<HandleElevatorDeviceEntity> GetByIdAsync(string id);
        Task<List<HandleElevatorDeviceEntity>> GetAllAsync();
        Task<HandleElevatorDeviceEntity> GetWithFloorsByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithElevatorServerByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithFloorByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithServersAndFloorsByIdAsync(string id);
    }
}
