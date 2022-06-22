using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class Finuser
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? CardHolderId { get; set; }
        public short? UserLevel { get; set; }
        public short? CardType { get; set; }
        public string CardId { get; set; }
        public string CustomId { get; set; }
        public int? AccessLevelId { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Password { get; set; }
        public string Password1 { get; set; }
        public short? NoOfUsesLeft { get; set; }
        public int? LastReaderHid { get; set; }
        public DateTime? LastReaderDate { get; set; }
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
