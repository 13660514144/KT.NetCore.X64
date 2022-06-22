using KT.WinPak.SDK.Entities;
using KT.WinPak.SDK.ISqlDaos;
using System.Collections.Generic;
using System.Linq;

namespace KT.WinPak.SDK.SqlDaos
{
    public class AccessLevelSqlDao : IAccessLevelSqlDao
    {
        private WINPAKPROContext _context;

        public AccessLevelSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<AccessLevelPlus> GetAll()
        {
            return _context.AccessLevelPlus.Where(x => x.Deleted == 0).ToList();
        }
    }
}
