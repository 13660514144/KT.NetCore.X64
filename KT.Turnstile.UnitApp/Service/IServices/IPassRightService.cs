using KT.Common.Data.Models;
using KT.Turnstile.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.IServices
{
    public interface IPassRightService
    {
        Task AddOrUpdateAsync(TurnstileUnitPassRightEntity entity);
        Task AddOrUpdateAsync(List<TurnstileUnitPassRightEntity> entities);
        Task Delete(string id, long editedTime);
        Task<TurnstileUnitPassRightEntity> GetByCardNubmerAndCardDeviceId(string cardNumber, string id);
        Task<PageData<TurnstileUnitPassRightEntity>> GetPageWithDetailAsync(int page, int size);
        Task Deletesync(TurnstileUnitPassRightEntity item);
    }
}
