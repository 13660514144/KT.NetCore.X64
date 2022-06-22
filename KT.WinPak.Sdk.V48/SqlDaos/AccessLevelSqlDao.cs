using KT.WinPak.SDK.V48.Entities;
using KT.WinPak.SDK.V48.ISqlDaos;
using System.Collections.Generic;
using System.Linq;

namespace KT.WinPak.SDK.V48.SqlDaos
{
    public class AccessLevelSqlDao : IAccessLevelSqlDao
    {
        private WINPAKPROContext _context;

        public AccessLevelSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<AccessLevelPlu> GetAll()
        {
            return _context.AccessLevelPlus.Where(x => x.Deleted == 0).ToList();
        }
    }
}
