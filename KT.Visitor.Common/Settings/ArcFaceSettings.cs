using System.Windows;

namespace KT.Visitor.Common.Settings
{
    /// <summary>
    /// 人脸设置
    /// </summary>
    public class ArcFaceSettings
    {
        /// <summary>
        /// SDK APPID    请到官网https://ai.arcsoft.com.cn/index.html中申请 -->
        /// </summary>
        public string AppId { get; set; } = "Cj3U46MibTVkH9a1iR9yFCT5Rerkh4RDDceNckobXr2e";

        /// <summary>
        /// 与win64 SDK对应    请到官网https://ai.arcsoft.com.cn/index.html中申请
        /// </summary>
        public string SdkKey64 { get; set; } = "7ym8XiRbxr1bgqzP7Q4M8poJyTLp36JANTESg53wNrTh";

        /// <summary>
        /// 与win32 SDK对应    请到官网https://ai.arcsoft.com.cn/index.html中申请 
        /// </summary>
        public string SdkKey32 { get; set; } = "7ym8XiRbxr1bgqzP7Q4M8poJvD7YdquG9FL4iPSsuyRy";

        /// <summary>
        /// 阈值
        /// </summary>
        public float Threshold { get; set; } = 0.8F;

        /// <summary>
        /// 头像裁剪扩展
        /// </summary>
        public Thickness CutExtension { get; set; } = new Thickness(0.1, 0.3, 0.1, 0.3);
    }
}
