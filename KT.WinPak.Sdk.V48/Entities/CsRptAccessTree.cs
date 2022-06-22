using System;
using System.Collections.Generic;

#nullable disable

namespace KT.WinPak.SDK.V48.Entities
{
    public partial class CsRptAccessTree
    {
        public int? AccountId { get; set; }
        public int RecordId { get; set; }
        public int? ParentRecordId { get; set; }
        public string BranchName { get; set; }
        public int? EntranceId { get; set; }
        public string ReaderName { get; set; }
        public byte? Deleted { get; set; }
    }
}
