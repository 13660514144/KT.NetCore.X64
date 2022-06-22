using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaStatusMaster
    {
        public TaStatusMaster()
        {
            TaEmployeeAttnRegularizes = new HashSet<TaEmployeeAttnRegularize>();
            TaEmployeeOutDuties = new HashSet<TaEmployeeOutDuty>();
            TaEmployeeOutDutyDetails = new HashSet<TaEmployeeOutDutyDetail>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TaEmployeeAttnRegularize> TaEmployeeAttnRegularizes { get; set; }
        public virtual ICollection<TaEmployeeOutDuty> TaEmployeeOutDuties { get; set; }
        public virtual ICollection<TaEmployeeOutDutyDetail> TaEmployeeOutDutyDetails { get; set; }
    }
}
