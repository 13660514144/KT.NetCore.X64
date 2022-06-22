using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaLeaveMaster
    {
        public TaLeaveMaster()
        {
            TaLeaveTypeMasters = new HashSet<TaLeaveTypeMaster>();
        }

        public int LeaveId { get; set; }
        public string LeaveName { get; set; }
        public string LeaveCode { get; set; }

        public virtual ICollection<TaLeaveTypeMaster> TaLeaveTypeMasters { get; set; }
    }
}
