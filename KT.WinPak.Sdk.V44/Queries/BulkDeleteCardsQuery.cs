using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class BulkDeleteCardsQuery
    {
        public object sStartNo { get; set; }
        public object sStopNo { get; set; }
        public int lAccountID { get; set; }
        public int lOperID { get; set; }
        public object sOpName { get; set; }
    }
}
