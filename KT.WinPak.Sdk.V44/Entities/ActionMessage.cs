using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class ActionMessage
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? ActionGroupId { get; set; }
        public short? ActionNumber { get; set; }
        public short? Priority { get; set; }
        public int? TimeZoneId { get; set; }
        public int? RecvCmdfileId { get; set; }
        public int? AckCmdfileId { get; set; }
        public int? ClearCmdfileId { get; set; }
        public int? CameraId { get; set; }
        public int? MonitorId { get; set; }
        public string AnimationFile { get; set; }
        public string Message { get; set; }
        public short? HistoryFlag { get; set; }
        public short? PrintFlag { get; set; }
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
