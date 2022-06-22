using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class ScheduleReport
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public string ScheduleName { get; set; }
        public short? Type { get; set; }
        public short? Frequency { get; set; }
        public DateTime? NextDateAndTime { get; set; }
        public string Advname { get; set; }
        public string CmdFileName { get; set; }
        public int? Flags { get; set; }
        public short? SpareW1 { get; set; }
        public short? SpareW2 { get; set; }
        public short? SpareW3 { get; set; }
        public short? SpareW4 { get; set; }
        public int? SpareDw1 { get; set; }
        public int? SpareDw2 { get; set; }
        public int? SpareDw3 { get; set; }
        public int? SpareDw4 { get; set; }
        public string SpareStr1 { get; set; }
        public string SpareStr2 { get; set; }
    }
}
