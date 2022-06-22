using KT.Unit.Face.Arc.Free.SDKModels;
using System;

namespace KT.Unit.Face.Arc.Free.Entity
{
    /// <summary>
    /// 视频检测缓存实体类
    /// </summary>
    public class FaceTrackUnit
    {
        public MRECT Rect { get; set; }
        public IntPtr Feature { get; set; }
        public string message = string.Empty;
    }
}
