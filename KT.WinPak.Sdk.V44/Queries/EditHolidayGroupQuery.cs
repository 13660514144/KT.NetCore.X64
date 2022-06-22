using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class EditHolidayGroupQuery
    {
        public string bstrExistHolGrpName { get; set; }
        public HolidayGroup pHolidayGroup { get; set; }
        public object vHolidays { get; set; }
        public int pStatus { get; set; }
    }
}
