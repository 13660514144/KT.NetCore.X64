using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class CsRptMcard
    {
        public int? AccountId { get; set; }
        public int? SubAccountId { get; set; }
        public string SubAccountName { get; set; }
        public string CardNumber { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public string CardStatus { get; set; }
        public string CardActivationDate { get; set; }
        public string CardExpirationDate { get; set; }
        public string AccesslevelName { get; set; }
        public int? AccessLevelId { get; set; }
        public short? Sparew4 { get; set; }
        public int? Sparedw4 { get; set; }
        public string Sparedw3 { get; set; }
    }
}
