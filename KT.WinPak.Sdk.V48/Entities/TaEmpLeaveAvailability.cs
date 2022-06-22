using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmpLeaveAvailability
    {
        public int LeaveAvailableId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public double? Balance { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual TaEmployee Employee { get; set; }
        public virtual TaLeaveTypeMaster LeaveType { get; set; }
    }
}
