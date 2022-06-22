using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface IProcessorFloorDao : IBaseDataDao<ProcessorFloorEntity>
    {
        Task AddAsync(ProcessorFloorEntity entity);
        Task EditAsync(ProcessorFloorEntity entity);
        Task<ProcessorFloorEntity> GetWithFloorByProcessorIdAndSortIdAsync(string processorId, int sortId);
    }
}
