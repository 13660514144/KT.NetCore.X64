using KT.Common.Data.Daos;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;

namespace KT.Quanta.Service.Daos
{
    public class SerialConfigDao : BaseDataDao<SerialConfigEntity>, ISerialConfigDao
    {
        public SerialConfigDao(QuantaDbContext context) : base(context)
        {
        }
    }
}
