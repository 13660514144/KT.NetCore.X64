using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.Common.Core.Enums
{
    public class PathEnum : BaseEnum
    {
        private static string _recordImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "records", "images");

        public PathEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static PathEnum RECORD_IMAGE_PATH = new PathEnum(1, _recordImagePath, "通行记录图片地址");
    }
}
