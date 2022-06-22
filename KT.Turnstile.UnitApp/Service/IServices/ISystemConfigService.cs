using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.Entity.Enums;
using KT.Turnstile.Unit.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.IServices
{
    public interface ISystemConfigService
    {
        Task AddOrUpdateAsync(UnitSystemConfigModel model);
        Task AddOrUpdateAsync(SystemConfigEnum keyEnum, object value);
        Task AddOrUpdatesAsync(List<TurnstileUnitSystemConfigEntity> entities);
        Task<UnitSystemConfigModel> GetAsync();
    }
}
