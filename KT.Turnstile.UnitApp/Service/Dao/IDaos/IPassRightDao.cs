using KT.Common.Data.Daos;
using KT.Common.Data.Models;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Dao.IDaos
{
    public interface IPassRightDao : IBaseDataDao<TurnstileUnitPassRightEntity>
    {
        Task<TurnstileUnitPassRightEntity> GetByCardNubmerAndCardDeviceId(string cardNumber, string cardDeviceId);
        Task<PageData<TurnstileUnitPassRightEntity>> GetPageWithDetailsAsync(int page, int size);
        Task<TurnstileUnitPassRightEntity> GetWithDetailsByIdAsync(string id);
    }
}
