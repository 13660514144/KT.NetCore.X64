using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.WebApi.HttpModel
{
    public class FileInfoModel
    {
        public string FileType { get; set; }
        public byte[] Bytes { get; set; }
    }
}
