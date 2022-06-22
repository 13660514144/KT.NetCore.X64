using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IDaos
{
    public interface ICardDeviceDao : IBaseDataDao<CardDeviceEntity>
    {
        Task<CardDeviceEntity> DeleteReturnWidthProcessorAsync(string id);
        Task<List<CardDeviceEntity>> GetAllAsync();
        Task<CardDeviceEntity> GetByIdAsync(string id);
        Task<CardDeviceEntity> AddAsync(CardDeviceEntity entity);
        Task<CardDeviceEntity> EditAsync(CardDeviceEntity entity);
        Task<List<CardDeviceEntity>> GetByProcessorIdAsync(string processorId);
        Task<List<CardDeviceEntity>> GetByDeviceTypeAsync(string deviceType);
        Task<List<CardDeviceEntity>> GetByInAndNotAsync(List<string> inIds, List<string> notIds);
        Task<List<CardDeviceEntity>> GetByInAsync(List<string> inIds);
        Task<List<CardDeviceEntity>> GetByNotAsync(List<string> notIds);
        Task<CardDeviceEntity> GetWithProcessorByIdAsync(string cardDeviceId);
    }
}
