using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaReportFormat
    {
        public TaReportFormat()
        {
            TaUserReportSchedules = new HashSet<TaUserReportSchedule>();
        }

        public int FormatId { get; set; }
        public string FormatName { get; set; }

        public virtual ICollection<TaUserReportSchedule> TaUserReportSchedules { get; set; }
    }
}
