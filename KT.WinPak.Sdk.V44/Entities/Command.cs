using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class Command
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? CommandFileId { get; set; }
        public short? StepNo { get; set; }
        public int? Hid { get; set; }
        public short? Command1 { get; set; }
        public int? Param1 { get; set; }
        public int? Param2 { get; set; }
        public int? Param3 { get; set; }
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
        public byte[] Blob { get; set; }
    }
}
