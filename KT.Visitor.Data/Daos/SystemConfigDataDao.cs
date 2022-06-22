using KT.Common.Data.Daos;
using KT.Visitor.Data.Base;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.IDaos;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Daos
{
    public class SystemConfigDataDao : BaseDataDao<SystemConfigEntity>, ISystemConfigDataDao
    {
        private SqliteContext _context;
        public SystemConfigDataDao(SqliteContext context) : base(context)
        {
            _context = context;
        }

        public Task<SystemConfigEntity> SelectByKeyAsync(string key)
        {
            return base.SelectFirstByLambdaAsync(x => x.Key == key);
        }
    }
}
