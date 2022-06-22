using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Services
{
    public class ImportPhotoQuery
    {
        public uint dwCardHolderID { get; set; }
        public int PhotoNo { get; set; }

        public ref int pFileData => throw new NotImplementedException();

        public uint dwFileSize { get; set; }
    }
}
