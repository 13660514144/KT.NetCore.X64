using KT.Turnstile.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.IServices
{
    public interface ICardDeviceService
    {
        Task AddOrUpdateAsync(TurnstileUnitCardDeviceEntity entity);
        Task AddOrUpdateAsync(List<TurnstileUnitCardDeviceEntity> entities);
        Task DeleteAsync(string id, long editTime);
        Task<List<TurnstileUnitCardDeviceEntity>> GetAllAsync();
    }
}
