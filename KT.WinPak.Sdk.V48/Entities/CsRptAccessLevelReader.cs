using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class CsRptAccessLevelReader
    {
        public int? AccountId { get; set; }
        public int SubAccountId { get; set; }
        public string SubAccountName { get; set; }
        public int AccessLevelId { get; set; }
        public string AccessLevelName { get; set; }
        public int? ReaderId { get; set; }
        public string ReaderName { get; set; }
        public int? TimezoneId { get; set; }
        public string TimezoneName { get; set; }
    }
}
