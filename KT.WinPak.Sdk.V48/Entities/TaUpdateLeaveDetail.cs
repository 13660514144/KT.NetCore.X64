using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaUpdateLeaveDetail
    {
        public int UpdateLeaveId { get; set; }
        public int EmployeeId { get; set; }
        public double DaysAvailable { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual TaEmployee Employee { get; set; }
    }
}
