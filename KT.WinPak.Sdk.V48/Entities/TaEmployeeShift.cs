using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaEmployeeShift
    {
        public int EmployeeShiftId { get; set; }
        public int EmployeeId { get; set; }
        public int ShiftId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsShiftRotate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TaEmployee Employee { get; set; }
        public virtual TaShiftMaster Shift { get; set; }
    }
}
