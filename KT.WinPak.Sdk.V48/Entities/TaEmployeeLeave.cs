using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeLeave
    {
        public TaEmployeeLeave()
        {
            TaEmployeeLeaveDetails = new HashSet<TaEmployeeLeaveDetail>();
        }

        public int EmpLeaveId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string AuthorizedBy { get; set; }
        public int StatusId { get; set; }
        public string Address { get; set; }
        public string Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string Comments { get; set; }

        public virtual ICollection<TaEmployeeLeaveDetail> TaEmployeeLeaveDetails { get; set; }
    }
}
