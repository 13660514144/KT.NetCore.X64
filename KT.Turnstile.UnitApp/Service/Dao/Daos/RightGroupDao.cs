using KT.Common.Data.Daos;
using KT.Turnstile.Unit.ClientApp.Dao.Base;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Dao.Daos
{
    public class RightGroupDao : BaseDataDao<TurnstileUnitRightGroupEntity>, IRightGroupDao
    {
        private TurnstileUnitContext _context;
        public RightGroupDao(TurnstileUnitContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TurnstileUnitRightGroupDetailEntity> GetDetailByCardDeviceIdAndGroupIdAsync(string cardDeviceId, string rightGroupId)
        {
            var entity = await _context.RightGroupDetails
                .Include(x=>x.CardDevice)
                .Include(x=>x.RightGroup)
                .FirstOrDefaultAsync(x => x.CardDevice.Id == cardDeviceId && x.RightGroup.Id == rightGroupId);

            return entity;
        }

        public async Task<TurnstileUnitRightGroupEntity> GetWithDetailsByIdAsync(string id)
        {
            var result = await _context.RightGroups
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
