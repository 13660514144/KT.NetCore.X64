using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class CardEx
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public string CardNumber { get; set; }
        public short? Issue { get; set; }
        public int? CardHolderId { get; set; }
        public int? AccessLevelId { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public short? NoOfUsesLeft { get; set; }
        public string Pin1 { get; set; }
        public string Pin2 { get; set; }
        public int? CmdfileId { get; set; }
        public short? CardStatus { get; set; }
        public short? Display { get; set; }
        public int? BackDrop1Id { get; set; }
        public int? BackDrop2Id { get; set; }
        public int? ActionGroupId { get; set; }
        public byte? PrintStatus { get; set; }
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
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public string Name { get; set; }
    }
}
