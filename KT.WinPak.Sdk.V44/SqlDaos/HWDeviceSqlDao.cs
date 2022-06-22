using KT.WinPak.SDK.Entities;
using KT.WinPak.SDK.ISqlDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.SqlDaos
{
    public class DeviceSqlDao : IHWDeviceSqlDao
    {
        private WINPAKPROContext _context;

        public DeviceSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<HwindependentDevices> GetReaders()
        {
            return _context.HwindependentDevices.Where(x => x.Deleted == 0
                && x.DeviceType == 50
            ).ToList();
        }
    }
}
