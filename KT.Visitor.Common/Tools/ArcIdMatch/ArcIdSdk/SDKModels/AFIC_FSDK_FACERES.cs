namespace KT.Visitor.Common.Tools.ArcIdMatch.ArcIdSdk.SDKModels
{
    /// <summary>
    /// 图片检测结果结构体
    /// </summary>
    public struct AFIC_FSDK_FACERES
    {
        /// <summary>
        /// 检测到的人脸数
        /// </summary>
        public int nFace;

        /// <summary>
        /// 人脸框位置
        /// </summary>
        public RECT rcFace;
    }
}
