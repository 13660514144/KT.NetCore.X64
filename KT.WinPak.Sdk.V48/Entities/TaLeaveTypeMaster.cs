using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaLeaveTypeMaster
    {
        public TaLeaveTypeMaster()
        {
            TaEmpLeaveAvailabilities = new HashSet<TaEmpLeaveAvailability>();
        }

        public int LeaveTypeId { get; set; }
        public int LeaveId { get; set; }
        public int LocationId { get; set; }
        public string LeaveApplicable { get; set; }
        public bool IsBalanceCarryFwd { get; set; }
        public bool IsHolidayOff { get; set; }
        public bool IsNegativeBalance { get; set; }
        public bool IsWeeklyOff { get; set; }
        public int? MaxApplnPerYear { get; set; }
        public int? MaxDaysCarryFwd { get; set; }
        public int? MaxDaysPerAppln { get; set; }
        public int? MaxDaysPerYear { get; set; }
        public int? MinServiceInDays { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LeaveApplicableId { get; set; }

        public virtual TaLeaveMaster Leave { get; set; }
        public virtual TaLocationMaster Location { get; set; }
        public virtual ICollection<TaEmpLeaveAvailability> TaEmpLeaveAvailabilities { get; set; }
    }
}
