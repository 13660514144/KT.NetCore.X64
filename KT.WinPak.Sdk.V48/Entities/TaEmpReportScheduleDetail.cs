using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmpReportScheduleDetail
    {
        public int ScheduleDetailsId { get; set; }
        public int ScheduleId { get; set; }
        public int EmployeeId { get; set; }

        public virtual TaEmployee Employee { get; set; }
        public virtual TaEmpReportSchedule Schedule { get; set; }
    }
}
