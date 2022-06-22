using KT.Common.Core.Enums;

namespace ArcSoftFace.Entity
{
    public class FaceRecognitionTypeEnum : BaseEnum
    {
        public FaceRecognitionTypeEnum()
        {
        }

        public FaceRecognitionTypeEnum(int code, string value, string text) : base(code, value, text)
        {
        }

        public static FaceRecognitionTypeEnum None => new FaceRecognitionTypeEnum(1, "None", "不启用人脸识别");
        public static FaceRecognitionTypeEnum ArcFree22 => new FaceRecognitionTypeEnum(2, "ARC_FREE_22", "虹软免费2.2");
        public static FaceRecognitionTypeEnum ArcPro40 => new FaceRecognitionTypeEnum(3, "ARC_PRO_4", "虹软收费4.0");
        public static FaceRecognitionTypeEnum Koala => new FaceRecognitionTypeEnum(4, "KOALA", "考拉1比1");

    }
}
