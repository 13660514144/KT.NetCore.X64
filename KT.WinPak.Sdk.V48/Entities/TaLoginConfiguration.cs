using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class TaLoginConfiguration
    {
        public int LoginConfigId { get; set; }
        public bool IsDomain { get; set; }
        public int? DblockTime { get; set; }
        public int? NoofAttempt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
