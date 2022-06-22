using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.ISqlDaos
{
    public interface ITimeZoneSqlDao
    {
        List<Entities.TimeZone> GetAll();
    }
}
