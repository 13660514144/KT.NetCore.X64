using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaReportQueue
    {
        public long Id { get; set; }
        public DateTime RequestDate { get; set; }
        public long ScheduleDetailsId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime ScheduleTime { get; set; }
        public long ReportId { get; set; }
        public string ReportName { get; set; }
        public long FrequencyId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmailId { get; set; }
        public string ReportPath { get; set; }
        public DateTime? ReportLastAttemptOn { get; set; }
        public int ReportAttemptCount { get; set; }
        public int ReportStatus { get; set; }
        public DateTime? EmailLastAttemptOn { get; set; }
        public int EmailAttemptCount { get; set; }
        public int EmailStatus { get; set; }

        public virtual TaEmployee Employee { get; set; }
    }
}
