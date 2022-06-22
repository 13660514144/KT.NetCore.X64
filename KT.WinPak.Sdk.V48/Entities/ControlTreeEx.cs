using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class ControlTreeEx
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? ParentRecordId { get; set; }
        public short? NodeLevel { get; set; }
        public string Name { get; set; }
        public short? NodeClass { get; set; }
        public int? DeviceId { get; set; }
        public short? DeviceType { get; set; }
        public short? SubTypeLevel { get; set; }
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
