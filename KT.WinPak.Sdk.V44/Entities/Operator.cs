using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class Operator
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public short? OperatorType { get; set; }
        public string OperatorName { get; set; }
        public string Description { get; set; }
        public byte[] Password { get; set; }
        public int? OperatorLevel { get; set; }
        public int? CardHolderId { get; set; }
        public int? TimeZoneId { get; set; }
        public int? AccountIdassigned { get; set; }
        public int? LogOnCmd { get; set; }
        public int? LogOffCmd { get; set; }
        public short? LonOnattempts { get; set; }
        public DateTime? LastLogOn { get; set; }
        public string Language { get; set; }
        public string UserSid { get; set; }
        public string UserDomain { get; set; }
        public short? SpareW1 { get; set; }
        public short? SpareW2 { get; set; }
        public short? SpareW3 { get; set; }
        public int? SpareW4 { get; set; }
        public int? SpareDw1 { get; set; }
        public int? SpareDw2 { get; set; }
        public int? SpareDw3 { get; set; }
        public int? SpareDw4 { get; set; }
        public string SpareStr1 { get; set; }
        public string SpareStr2 { get; set; }
    }
}
