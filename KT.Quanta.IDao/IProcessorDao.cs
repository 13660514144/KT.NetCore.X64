using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IProcessorDao : IBaseDataDao<ProcessorEntity>
    {
        Task<ProcessorEntity> AddAsync(ProcessorEntity entity);
        Task<ProcessorEntity> EditAsync(ProcessorEntity entity);
        Task<List<ProcessorEntity>> GetAllAsync();
        Task<ProcessorEntity> GetByIdAsync(string id);
        Task<List<ProcessorEntity>> GetWidthCardDevicesAsync();
        Task<ProcessorEntity> GetWithFloorsByIdAsync(string id);
        Task<ProcessorEntity> GetWithProcessorFloorsByIdAsync(string processorId);
        Task<List<ProcessorEntity>> GetByDeviceTypeAsync(string deviceType);
        Task<ProcessorEntity> GetWidthCardDevicesByIdAsync(string id);
    }
}
