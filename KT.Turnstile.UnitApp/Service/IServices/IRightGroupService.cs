using KT.Turnstile.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.IServices
{
    public interface IRightGroupService
    {
        Task AddOrUpdateAsync(TurnstileUnitRightGroupEntity entity);
        Task AddOrUpdateAsync(List<TurnstileUnitRightGroupEntity> entities);
        Task DeleteAsync(string id, long editTime);
        Task<List<TurnstileUnitRightGroupEntity>> GetAll();
    }
}
