﻿using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class SubAccount
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public string SubAcctName { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactAddress { get; set; }
        public string MiscField1 { get; set; }
        public string MiscField2 { get; set; }
        public string MiscField3 { get; set; }
        public string MiscField4 { get; set; }
        public string MiscField5 { get; set; }
        public string MiscField6 { get; set; }
        public string MiscField7 { get; set; }
        public string MiscField8 { get; set; }
        public string MiscField9 { get; set; }
        public string MiscField10 { get; set; }
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
