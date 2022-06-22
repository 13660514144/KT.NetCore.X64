using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaLocationMaster
    {
        public TaLocationMaster()
        {
            TaEmployees = new HashSet<TaEmployee>();
            TaHolidayMasters = new HashSet<TaHolidayMaster>();
            TaLeaveTypeMasters = new HashSet<TaLeaveTypeMaster>();
            TaShiftMasters = new HashSet<TaShiftMaster>();
            TaShiftRotations = new HashSet<TaShiftRotation>();
        }

        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<TaEmployee> TaEmployees { get; set; }
        public virtual ICollection<TaHolidayMaster> TaHolidayMasters { get; set; }
        public virtual ICollection<TaLeaveTypeMaster> TaLeaveTypeMasters { get; set; }
        public virtual ICollection<TaShiftMaster> TaShiftMasters { get; set; }
        public virtual ICollection<TaShiftRotation> TaShiftRotations { get; set; }
    }
}
