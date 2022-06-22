using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Enums;
using KT.Elevator.Unit.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.IServices
{
    public interface ISystemConfigService
    {
        Task AddOrUpdateAsync(UnitSystemConfigModel model);
        Task AddOrUpdateAsync(UnitSystemConfigEnum keyEnum, object value);
        Task AddOrUpdatesAsync(List<UnitSystemConfigEntity> entities);
        Task<UnitSystemConfigModel> GetAsync();
        Task<UnitSystemConfigModel> AddOrEditFromDeviceAsync(UnitHandleElevatorDeviceModel model, UnitSystemConfigModel localConfig);
    }
}
