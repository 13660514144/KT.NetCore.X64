using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IProcessorDao : IBaseDataDao<ProcessorEntity>
    {
        Task<ProcessorEntity> AddAsync(ProcessorEntity entity);
        Task<ProcessorEntity> EditAsync(ProcessorEntity entity);
        Task<List<ProcessorEntity>> GetAllAsync();
        Task<ProcessorEntity> GetByIdAsync(string id);
        Task<List<ProcessorEntity>> GetWidthCardDevicesAsync();
        Task<ProcessorEntity> GetWithFloorsByIdAsync(string id);
    }
}
