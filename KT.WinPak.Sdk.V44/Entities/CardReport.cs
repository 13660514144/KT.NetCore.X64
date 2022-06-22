using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class CardReport
    {
        public int CardRecId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public string CardNumber { get; set; }
        public int? CardHolderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AccessLevelId { get; set; }
        public string AlplusName { get; set; }
        public short? CardStatus { get; set; }
        public DateTime? CardActivationDate { get; set; }
        public DateTime? CardExpirationDate { get; set; }
        public string Pin1 { get; set; }
        public string Pin2 { get; set; }
        public string BadgeHeaderName { get; set; }
        public string BadgeHeader1Name { get; set; }
        public int? AlplusRecId { get; set; }
        public DateTime? AlplusActivationDate { get; set; }
        public DateTime? AlplusExpirationDate { get; set; }
        public byte? PrintStatus { get; set; }
        public string ActionGroup { get; set; }
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
