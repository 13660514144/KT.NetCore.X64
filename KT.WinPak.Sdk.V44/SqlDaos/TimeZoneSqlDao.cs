using KT.WinPak.SDK.Entities;
using KT.WinPak.SDK.ISqlDaos;
using System.Collections.Generic;
using System.Linq;

namespace KT.WinPak.SDK.SqlDaos
{
    public class TimeZoneSqlDao : ITimeZoneSqlDao
    {
        private WINPAKPROContext _context;

        public TimeZoneSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<TimeZone> GetAll()
        {
            return _context.TimeZone.Where(x => x.Deleted == 0).ToList();
        }
    }
}
