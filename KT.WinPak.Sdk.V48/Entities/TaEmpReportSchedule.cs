using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmpReportSchedule
    {
        public TaEmpReportSchedule()
        {
            TaEmpReportScheduleDetails = new HashSet<TaEmpReportScheduleDetail>();
        }

        public int ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public long ReportId { get; set; }
        public int FrequencyId { get; set; }
        public DateTime? ScheduleTime { get; set; }

        public virtual TaScheduledReport Report { get; set; }
        public virtual ICollection<TaEmpReportScheduleDetail> TaEmpReportScheduleDetails { get; set; }
    }
}
