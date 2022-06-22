using KT.WinPak.SDK.V48.Entities;
using KT.WinPak.SDK.V48.ISqlDaos;
using System.Collections.Generic;
using System.Linq;

namespace KT.WinPak.SDK.V48.SqlDaos
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
            return _context.TimeZones.Where(x => x.Deleted == 0).ToList();
        }
    }
}
