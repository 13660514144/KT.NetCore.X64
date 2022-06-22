using System;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.FaceHelpers.ArcFaceFree
{
    /// <summary>
    /// 人脸检测引擎句柄
    /// </summary>
    public class FaceEngine
    {
        /// <summary>
        /// 图片引擎句柄
        /// </summary>
        public IntPtr ImageEngine { get; set; } = IntPtr.Zero;

        /// <summary>
        /// 视频引擎句柄
        /// </summary>
        public IntPtr VideoEngine { get; set; } = IntPtr.Zero;

        /// <summary>
        /// RGB引擎句柄
        /// </summary>
        public IntPtr VideoRGBImageEngine { get; set; } = IntPtr.Zero;

        /// <summary>
        /// IR引擎句柄
        /// </summary>
        public IntPtr VideoIRImageEngine { get; set; } = IntPtr.Zero;
    }
}
