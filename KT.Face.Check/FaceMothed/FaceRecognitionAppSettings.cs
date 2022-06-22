

namespace ArcSoftFace.Entity
{
    public class FaceRecognitionAppSettings
    {
        public string FaceRecognitionType { get; set; } = FaceRecognitionTypeEnum.ArcFree22.Value;
        public string FaceAuxiliaryType { get; set; } = FaceRecognitionTypeEnum.ArcFree22.Value;

        public ArcFaceSDK.Models.ArcFaceSettings ArcProFaceSettings { get; set; }
       

    }
}
