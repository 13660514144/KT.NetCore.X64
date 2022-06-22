using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class GetPhotoSizeQuery
    {
        public uint dwCardHolderID { get; set; }
        public int PhotoNo { get; set; }

        public ref uint pdwFileSize => throw new NotImplementedException();
    }
}
