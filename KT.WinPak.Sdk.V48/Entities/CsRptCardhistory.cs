using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class CsRptCardhistory
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public DateTime? Gentime { get; set; }
        public string Cardnumber { get; set; }
        public string Type { get; set; }
        public short? TransactionType { get; set; }
        public short? CardEventType { get; set; }
        public string Status { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public int? ReaderId { get; set; }
        public string ReaderName { get; set; }
    }
}
