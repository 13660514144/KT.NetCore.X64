using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaHolidayMaster
    {
        public int HolidayId { get; set; }
        public DateTime? HolidayDate { get; set; }
        public string HolidayDesc { get; set; }
        public int? LocationId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? Createdby { get; set; }

        public virtual TaLocationMaster Location { get; set; }
    }
}
