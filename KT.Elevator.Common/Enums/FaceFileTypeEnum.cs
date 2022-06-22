using KT.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Common.Enums
{
    public class FaceFileTypeEnum : BaseEnum
    {
        public FaceFileTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }
        public static FaceFileTypeEnum FACE { get; } = new FaceFileTypeEnum(1, "FACE", "人脸");
        public static FaceFileTypeEnum CERTIFICATE { get; } = new FaceFileTypeEnum(2, "CERTIFICATE", "证件照");
        public static FaceFileTypeEnum LIFE { get; } = new FaceFileTypeEnum(3, "CERTIFICATE", "生活照");
    }
}
