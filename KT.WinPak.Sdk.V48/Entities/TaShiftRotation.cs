using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaShiftRotation
    {
        public int ShiftRotationId { get; set; }
        public int LocationId { get; set; }
        public string RotationName { get; set; }
        public int FromShift { get; set; }
        public int ToShift { get; set; }
        public int RotateAfterEvery { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TaShiftMaster FromShiftNavigation { get; set; }
        public virtual TaLocationMaster Location { get; set; }
        public virtual TaShiftMaster ToShiftNavigation { get; set; }
    }
}
