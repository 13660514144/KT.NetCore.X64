using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class AddHolidayQuery
    {
        public MasterHoliday pMasterHoliday { get; set; }
        public int pHolID { get; set; }
        public int pStatus { get; set; }
    }
}
