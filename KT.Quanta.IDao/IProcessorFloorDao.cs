using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface IProcessorFloorDao : IBaseDataDao<ProcessorFloorEntity>
    {
        Task AddAsync(ProcessorFloorEntity entity);
        Task EditAsync(ProcessorFloorEntity entity);
        Task<ProcessorFloorEntity> GetWithFloorByProcessorIdAndSortIdAsync(string processorId, int sortId);
    }
}
