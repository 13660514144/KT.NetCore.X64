using System;
using System.Collections.Generic;

namespace KT.WinPak.SDK.Entities
{
    public partial class NoteFieldTemplate
    {
        public int RecordId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? UserId { get; set; }
        public int? NodeId { get; set; }
        public byte? Deleted { get; set; }
        public short? UserPriority { get; set; }
        public int? RefField { get; set; }
        public string Nfname1 { get; set; }
        public string Nfname2 { get; set; }
        public string Nfname3 { get; set; }
        public string Nfname4 { get; set; }
        public string Nfname5 { get; set; }
        public string FieldDefinition { get; set; }
        public short? Param1 { get; set; }
        public short? Param2 { get; set; }
        public short? Param3 { get; set; }
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
