using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class SystemConfigDao : BaseDataDao<SystemConfigEntity>, ISystemConfigDao
    {
        public SystemConfigDao(QuantaDbContext context) : base(context)
        {
        }
    }
}
