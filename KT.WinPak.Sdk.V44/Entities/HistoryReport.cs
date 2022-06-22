﻿using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class HistoryReport
    {
        public int RecordId { get; set; }
        public int? HistoryAccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public DateTime? GenTime { get; set; }
        public int? SeqId { get; set; }
        public short? Type1 { get; set; }
        public short? Param1 { get; set; }
        public short? Param2 { get; set; }
        public string Param3 { get; set; }
        public int? Link1 { get; set; }
        public int? Link2 { get; set; }
        public int? Link3 { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CardHolderAccountId { get; set; }
        public string OperatorName { get; set; }
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
        public byte[] Note { get; set; }
    }
}
