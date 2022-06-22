using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class PassRecordDao : BaseDataDao<PassRecordEntity>, IPassRecordDao
    {
        public PassRecordDao(QuantaDbContext context) : base(context)
        {
        }

    }
}
