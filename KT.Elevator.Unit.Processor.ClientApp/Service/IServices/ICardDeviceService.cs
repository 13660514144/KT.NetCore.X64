using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface ICardDeviceService
    {
        Task AddOrUpdateAsync(UnitCardDeviceEntity entity);
        Task AddOrUpdateAsync(List<UnitCardDeviceEntity> entities);
        Task DeleteAsync(string id, long editTime);
        Task<List<UnitCardDeviceEntity>> GetAllAsync();
    }
}
