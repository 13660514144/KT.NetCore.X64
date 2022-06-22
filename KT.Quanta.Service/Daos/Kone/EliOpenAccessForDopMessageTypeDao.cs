using KT.Common.Data.Daos;
using KT.Quanta.Entity.Entities;
using KT.Quanta.Entity.Kone;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class EliOpenAccessForDopMessageTypeDao
        : BaseDataDao<EliOpenAccessForDopMessageTypeEntity>, IEliOpenAccessForDopMessageTypeDao
    {
        private QuantaDbContext _context;
        public EliOpenAccessForDopMessageTypeDao(QuantaDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
