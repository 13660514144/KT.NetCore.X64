using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaOutDutyTypeMaster
    {
        public TaOutDutyTypeMaster()
        {
            TaEmployeeOutDuties = new HashSet<TaEmployeeOutDuty>();
        }

        public int OutDutyTypeId { get; set; }
        public string OutDutyDesc { get; set; }
        public string OutDutyCode { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TaEmployeeOutDuty> TaEmployeeOutDuties { get; set; }
    }
}
