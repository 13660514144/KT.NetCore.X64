using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class Reports
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short? EnglishUnits { get; set; }
        public int? PageWidth { get; set; }
        public int? PageLength { get; set; }
        public int? TopMargin { get; set; }
        public int? BottomMargin { get; set; }
        public int? LeftMargin { get; set; }
        public int? RightMargin { get; set; }
        public string MasterTable { get; set; }
        public string LinkedTable { get; set; }
        public string LinkedCol { get; set; }
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
