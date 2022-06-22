using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IHandleElevatorDeviceDao : IBaseDataDao<HandleElevatorDeviceEntity>
    {
        Task<HandleElevatorDeviceEntity> AddAsync(HandleElevatorDeviceEntity entity);
        Task<HandleElevatorDeviceEntity> EditAsync(HandleElevatorDeviceEntity entity);
        Task<HandleElevatorDeviceEntity> GetByIdAsync(string id);
        Task<List<HandleElevatorDeviceEntity>> GetAllAsync();
        Task<HandleElevatorDeviceEntity> GetWithFloorsByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithFloorAndRelevanceFloorsByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithElevatorServerByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithFloorByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithServersAndFloorsByIdAsync(string id);
        Task<HandleElevatorDeviceEntity> GetWithElevatorGroupByIdAsync(string id);
    }
}
