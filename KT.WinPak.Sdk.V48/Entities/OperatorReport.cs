using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class OperatorReport
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public short? OperatorType { get; set; }
        public string OperatorName { get; set; }
        public string Description { get; set; }
        public string OperatorLevelName { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string CmdFileName { get; set; }
        public string CmdFile1Name { get; set; }
        public DateTime? LastLogOn { get; set; }
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
