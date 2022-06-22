using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaUserReportSchedule
    {
        public TaUserReportSchedule()
        {
            TaUserReportScheduleDetails = new HashSet<TaUserReportScheduleDetail>();
        }

        public int ScheduleId { get; set; }
        public int ReportId { get; set; }
        public int FrequencyId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public string ScheduleName { get; set; }
        public int? FormatId { get; set; }

        public virtual TaReportFormat Format { get; set; }
        public virtual ICollection<TaUserReportScheduleDetail> TaUserReportScheduleDetails { get; set; }
    }
}
