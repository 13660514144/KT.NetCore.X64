using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class PanelLogEx
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public DateTime? RecvTime { get; set; }
        public DateTime? GenTime { get; set; }
        public int EventType { get; set; }
        public int? DeviceId { get; set; }
        public string Event { get; set; }
        public short? ZoneNumber { get; set; }
        public short? Code { get; set; }
        public string ModuleName { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public int? SeqId { get; set; }
        public short? Type1 { get; set; }
        public short? Type2 { get; set; }
        public short? Param1 { get; set; }
        public string Param3 { get; set; }
        public int? Link1 { get; set; }
        public int? Link2 { get; set; }
        public int? Link4 { get; set; }
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
