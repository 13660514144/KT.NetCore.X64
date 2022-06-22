using KT.Common.Data.Daos;
using KT.Turnstile.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Dao.IDaos
{
    public interface IRightGroupDao : IBaseDataDao<TurnstileUnitRightGroupEntity>
    {
        Task<TurnstileUnitRightGroupEntity> GetWithDetailsByIdAsync(string id);
        Task<TurnstileUnitRightGroupDetailEntity> GetDetailByCardDeviceIdAndGroupIdAsync(string cardDeviceId, string rightGroupId); 
    }
}
