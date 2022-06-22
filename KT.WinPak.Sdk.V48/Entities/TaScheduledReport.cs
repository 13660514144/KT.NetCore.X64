using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaScheduledReport
    {
        public TaScheduledReport()
        {
            TaEmpReportSchedules = new HashSet<TaEmpReportSchedule>();
        }

        public long ReportId { get; set; }
        public string ReportName { get; set; }

        public virtual ICollection<TaEmpReportSchedule> TaEmpReportSchedules { get; set; }
    }
}
