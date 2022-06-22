using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaShiftDetail
    {
        public int ShiftDetailsId { get; set; }
        public int ShiftId { get; set; }
        public string ShiftDay { get; set; }
        public bool? IsWeeklyOff { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual TaShiftMaster Shift { get; set; }
    }
}
