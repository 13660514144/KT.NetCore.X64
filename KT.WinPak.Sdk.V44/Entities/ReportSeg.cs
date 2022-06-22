using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class ReportSeg
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public string ReportId { get; set; }
        public int? LineNo { get; set; }
        public int? Xpos { get; set; }
        public string Text { get; set; }
        public int? Alignment { get; set; }
        public short? Italics { get; set; }
        public short? Underline { get; set; }
        public string FaceName { get; set; }
        public int? FontFace { get; set; }
        public int? FontHeight { get; set; }
        public int? FontWeight { get; set; }
        public string TableName { get; set; }
        public string ColName { get; set; }
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
