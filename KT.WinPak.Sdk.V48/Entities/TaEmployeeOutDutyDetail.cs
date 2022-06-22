using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeOutDutyDetail
    {
        public int EmpOutDutyDetailsId { get; set; }
        public int EmpOutDutyId { get; set; }
        public DateTime? OutDutyDate { get; set; }
        public int StatusId { get; set; }
        public byte? IsHalfDay { get; set; }
        public string Comments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TaEmployeeOutDuty EmpOutDuty { get; set; }
        public virtual TaStatusMaster Status { get; set; }
    }
}
