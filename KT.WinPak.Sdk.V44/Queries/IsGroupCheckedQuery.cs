using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class IsGroupCheckedQuery
    {
        public int AccountID { get; set; }
        public int iPanelNo { get; set; }

        public ref int pGroupCheck => throw new NotImplementedException();
    }
}
