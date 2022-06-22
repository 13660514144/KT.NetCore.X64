using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.Services
{
    public class GetSigSizeQuery
    {
        public uint dwCardHolderID { get; set; }
        public int SigNo { get; set; }

        public ref uint pdwFileSize => throw new NotImplementedException();
    }
}
