using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaShiftMaster
    {
        public TaShiftMaster()
        {
            TaEmployeeShifts = new HashSet<TaEmployeeShift>();
            TaShiftDetails = new HashSet<TaShiftDetail>();
            TaShiftRotationFromShiftNavigations = new HashSet<TaShiftRotation>();
            TaShiftRotationToShiftNavigations = new HashSet<TaShiftRotation>();
        }

        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int LocationId { get; set; }
        public string ShiftStart { get; set; }
        public string ShiftEnd { get; set; }
        public string WorkingHrs { get; set; }
        public string ShiftInGrace { get; set; }
        public string LateUpto { get; set; }
        public string Half1End { get; set; }
        public string Half2Start { get; set; }
        public string EarlyGo { get; set; }
        public string ShiftEarly { get; set; }
        public string MinHrsFullDay { get; set; }
        public string MinHrsHalfDay { get; set; }
        public bool? IsFlexi { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public virtual TaLocationMaster Location { get; set; }
        public virtual ICollection<TaEmployeeShift> TaEmployeeShifts { get; set; }
        public virtual ICollection<TaShiftDetail> TaShiftDetails { get; set; }
        public virtual ICollection<TaShiftRotation> TaShiftRotationFromShiftNavigations { get; set; }
        public virtual ICollection<TaShiftRotation> TaShiftRotationToShiftNavigations { get; set; }
    }
}
