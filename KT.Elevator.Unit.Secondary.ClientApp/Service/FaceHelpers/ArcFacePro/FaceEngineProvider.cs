using KT.Unit.Face.Arc.Pro;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFacePro
{
    /// <summary>
    /// 人脸检测引擎句柄
    /// </summary>
    public class FaceEngineProvider
    {
        /// <summary>
        /// 图片引擎句柄
        /// </summary>
        public FaceEngine ImageEngine { get; set; } = new FaceEngine();

        /// <summary>
        /// 视频引擎句柄
        /// </summary>
        public FaceEngine VideoEngine { get; set; } = new FaceEngine();

        /// <summary>
        /// RGB引擎句柄
        /// </summary>
        public FaceEngine VideoRGBImageEngine { get; set; } = new FaceEngine();

        /// <summary>
        /// IR引擎句柄
        /// </summary>
        public FaceEngine VideoIRImageEngine { get; set; } = new FaceEngine();

    }
}
