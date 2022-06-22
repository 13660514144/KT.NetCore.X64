using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class FloorPlanItem
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? FloorPlanId { get; set; }
        public byte[] Olecontrol { get; set; }
        public int? Type { get; set; }
        public int? Hid { get; set; }
        public int? Xpoint1 { get; set; }
        public int? Ypoint1 { get; set; }
        public int? Xpoint2 { get; set; }
        public int? Ypoint2 { get; set; }
        public int? Xpoint3 { get; set; }
        public int? Ypoint3 { get; set; }
        public int? Xpoint4 { get; set; }
        public int? Ypoint4 { get; set; }
        public byte? ShowToolTip { get; set; }
        public byte? ShowName { get; set; }
        public int? RotateAngle { get; set; }
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
