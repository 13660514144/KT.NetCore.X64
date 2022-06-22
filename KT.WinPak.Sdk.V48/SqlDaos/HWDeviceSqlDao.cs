using KT.WinPak.SDK.V48.Entities;
using KT.WinPak.SDK.V48.ISqlDaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.WinPak.SDK.V48.SqlDaos
{
    public class DeviceSqlDao : IHWDeviceSqlDao
    {
        private WINPAKPROContext _context;

        public DeviceSqlDao(WINPAKPROContext context)
        {
            _context = context;
        }

        public List<HwindependentDevice> GetReaders()
        {
            return _context.HwindependentDevices.Where(x => x.Deleted == 0
                && x.DeviceType == 50
            ).ToList();
        }
    }
}
