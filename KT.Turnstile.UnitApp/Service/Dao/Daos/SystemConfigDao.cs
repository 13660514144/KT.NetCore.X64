using KT.Common.Data.Daos;
using KT.Turnstile.Unit.ClientApp.Dao.Base;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Dao.Daos
{
    public class SystemConfigDao : BaseDataDao<TurnstileUnitSystemConfigEntity>, ISystemConfigDao
    {
        private TurnstileUnitContext _context;
        public SystemConfigDao(TurnstileUnitContext context) : base(context)
        {
            _context = context;
        }
 
    }
}
