using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class CsRptCardholder
    {
        public int? AccountId { get; set; }
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public string Cardnumber { get; set; }
        public int? AccessLevelId { get; set; }
        public string Name { get; set; }
    }
}
