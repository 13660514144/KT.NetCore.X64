using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.IServices
{
    public interface ICardDeviceService
    {
        Task AddOrUpdateAsync(UnitCardDeviceEntity entity);
        Task AddOrUpdateAsync(List<UnitCardDeviceEntity> entities);
        Task Delete(string id, long editTime);
        Task<List<UnitCardDeviceEntity>> GetAllAsync();
    }
}
