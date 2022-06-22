using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Queries
{
    public class EditHolidayQuery
    {
        public string ExHolName { get; set; }
        public MasterHoliday pMasterHoliday { get; set; }
        public int pStatus { get; set; }
    }
}
