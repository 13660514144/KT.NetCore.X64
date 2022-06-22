
using KT.Unit.Face.Common.Models;

namespace KT.Unit.FaceRecognition.Models
{
    public class FaceRecognitionAppSettings
    {
        public string FaceRecognitionType { get; set; } = FaceRecognitionTypeEnum.ArcFree22.Value;
        public string FaceAuxiliaryType { get; set; } = FaceRecognitionTypeEnum.ArcFree22.Value;

        public KT.Unit.Face.Arc.Free.Models.ArcFaceSettings ArcFreeFaceSettings { get; set; }
        public KT.Unit.Face.Arc.Pro.Models.ArcFaceSettings ArcProFaceSettings { get; set; }
        public KoalaFaceSettings KoalaFaceSettings { get; set; }        

    }
}
