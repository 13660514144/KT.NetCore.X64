using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class AccountMap
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public int? UserPriority { get; set; }
        public int? AcctId { get; set; }
        public int? TableId { get; set; }
        public int? ItemId { get; set; }
        public int? SpareW1 { get; set; }
        public int? SpareW2 { get; set; }
        public int? SpareW3 { get; set; }
        public int? SpareW4 { get; set; }
        public int? SpareDw1 { get; set; }
        public int? SpareDw2 { get; set; }
        public int? SpareDw3 { get; set; }
        public int? SpareDw4 { get; set; }
        public string SpareStr1 { get; set; }
        public string SpareStr2 { get; set; }
    }
}
