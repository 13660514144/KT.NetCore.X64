using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetSigQuery
    {
        public uint dwCardHolderID { get; set; }
        public int SigNo { get; set; }

        public ref int pFileData => throw new NotImplementedException();

        public ref uint pdwFileSize => throw new NotImplementedException();
    }
}
