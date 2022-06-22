using KT.Common.Data.Daos;
using KT.Elevator.Manage.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IDaos
{
    public interface ICardDeviceDao : IBaseDataDao<CardDeviceEntity>
    {
        Task<CardDeviceEntity> DeleteReturnWidthProcessorAsync(string id);
        Task<List<CardDeviceEntity>> GetAllAsync();
        Task<CardDeviceEntity> GetByIdAsync(string id);
        Task<CardDeviceEntity> AddAsync(CardDeviceEntity entity);
        Task<CardDeviceEntity> EditAsync(CardDeviceEntity entity);
    }
}
