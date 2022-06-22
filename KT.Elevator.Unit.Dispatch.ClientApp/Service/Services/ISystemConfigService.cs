using KT.Elevator.Unit.Dispatch.Entity.Entities;
using KT.Elevator.Unit.Dispatch.Entity.Enums;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.IServices
{
    public interface ISystemConfigService
    {
        Task AddOrUpdateAsync(UnitDispatchSystemConfigModel model);
        Task AddOrUpdateAsync(UnitDispatchSystemConfigEnum keyEnum, object value);
        Task AddOrUpdatesAsync(List<UnitDispatchSystemConfigEntity> entities);
        Task<UnitDispatchSystemConfigModel> GetAsync();
    }
}
