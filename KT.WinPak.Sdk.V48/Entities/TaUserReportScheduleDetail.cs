using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaUserReportScheduleDetail
    {
        public int ScheduleDetailsId { get; set; }
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? NextScheduleDate { get; set; }
        public int? FetchStatus { get; set; }

        public virtual TaEmployee Employee { get; set; }
        public virtual TaUserReportSchedule Schedule { get; set; }
    }
}
