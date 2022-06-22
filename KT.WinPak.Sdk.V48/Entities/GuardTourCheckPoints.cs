using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class GuardTourCheckPoints
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? GuardTourId { get; set; }
        public int? SequenceNum { get; set; }
        public int? Hidid { get; set; }
        public byte? Sequenced { get; set; }
        public byte? PassBy { get; set; }
        public short? Time { get; set; }
        public short? TolerancePlus { get; set; }
        public short? ToleranceMinus { get; set; }
        public int? ActionGroupId { get; set; }
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
