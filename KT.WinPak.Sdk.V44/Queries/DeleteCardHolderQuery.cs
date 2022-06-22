using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Queries
{
    public class DeleteCardHolderQuery
    {
        public int lCHId { get; set; }
        public int bDelCard { get; set; }
        public int bDelImage { get; set; }
        public int pStatus { get; set; }
    }
}
