namespace ArcFaceSDK.Models
{
    /// <summary>
    /// 人脸设置
    /// </summary>
    public class ArcFaceSettings
    {
        /// <summary>
        /// 是否在线激活，true(在线激活) or false(离线激活)
        /// </summary>
        public bool IsOnlineActive { get; set; } = true;

        /// <summary>
        /// SDK APPID    请到官网https://ai.arcsoft.com.cn/index.html中申请 -->
        /// </summary>
        public string AppId { get; set; } = "Cj3U46MibTVkH9a1iR9yFCSxGFbYyJPhiqsdkNdvaeK3";

        /// <summary>
        /// 与win64 SDK对应    请到官网https://ai.arcsoft.com.cn/index.html中申请
        /// </summary>
        public string SdkKey64 { get; set; } = "6HB6vQP27rzNkTSEwyPF58xey1nYo9xSJ24HpYPuSbQt";

        /// <summary>
        /// 与win32 SDK对应    请到官网https://ai.arcsoft.com.cn/index.html中申请 
        /// </summary>
        public string SdkKey32 { get; set; } = "6HB6vQP27rzNkTSEwyPF58xeumZLMZKs5PB5gkWgpgxS";

        /// <summary>
        /// 付费激活码，与win64 SDK对应    请到官网https://ai.arcsoft.com.cn/index.html中申请
        /// </summary>
        public string ActiveKey64 { get; set; } = "086A-112B-G167-AB2E";

        /// <summary>
        /// 付费激活码，与win32 SDK对应    请到官网https://ai.arcsoft.com.cn/index.html中申请
        /// </summary>
        public string ActiveKey32 { get; set; } = "";

        /// <summary>
        /// 离线激活文件路径
        /// </summary>
        public string OfflineActiveFilePath { get; set; } = "";

        /// <summary>
        /// RGB摄像头索引
        /// 摄像头索引,索引从0开始，如果仅有一个摄像头请两个参数的值都设为0
        /// </summary>        
        public int RgbCameraIndex { get; set; } = 0;

        /// <summary>
        /// IR摄像头索引
        /// 摄像头索引,索引从0开始，如果仅有一个摄像头请两个参数的值都设为0
        /// </summary>        
        public int IrCameraIndex { get; set; } = 1;

        /// <summary>
        /// 视频模式：FR失败重试次数
        /// </summary>        
        public int FrMatchTime { get; set; } = 20;

        /// <summary>
        /// 视频模式：活体检测失败重试次数
        /// </summary>        
        public int LivenessMatchTime { get; set; } = 20;

        /// <summary>
        /// 阈值
        /// </summary>
        public float Threshold { get; set; } = 0.81F;

        /// <summary>
        /// 阈值
        /// </summary>
        public float ThresholdMask { get; set; } = 0.78F;

        /// <summary>
        /// 红外（IR）活体阈值
        /// </summary>
        public float ThresholdIr { get; set; } = 0.7f;

        /// <summary>
        /// 可见光（RGB）活体阈值
        /// </summary>
        public float ThresholdRgb { get; set; } = 0.5f;

        /// <summary>
        /// 图像质量注册阈值
        /// </summary>
        public float ThresholdImgRegister { get; set; } = 0.35f;

        /// <summary>
        /// 图像质量识别戴口罩阈值
        /// </summary>
        public float ThresholdImgMask { get; set; } = 0.29f;

        /// <summary>
        /// 图像质量识别未戴口罩阈值
        /// </summary>
        public float ThresholdImgNoMask { get; set; } = 0.32f;

        /// <summary>
        /// 人脸较验失败重复较验次数
        /// </summary>
        public int FaceErrorRecheckTimes { get; set; } = 5;

        /// <summary>
        /// 人脸较验失败延迟较验时间，较验不存在的人脸会重复多次较验，稍微延迟后再较验下一张，避免人脸未摆正
        /// </summary>
        public decimal FaceErrorRetryDelaySecondTime { get; set; } = 0.2M;

        /// <summary>
        /// 人脸扫描线程数量
        /// </summary>
        public int LibraryThread { get; set; } = 5;

        /// <summary>
        /// 初始化人脸页数大小
        /// </summary>
        public int InitPageSize { get; set; } = 50;

        /// <summary>
        /// 人脸最小面积
        /// </summary>
        public int FaceMinArea { get; set; } = 22000;

        /// <summary>
        /// 允许误差范围（双摄像头时人脸位置比较）
        /// </summary>
        public int AllowAbleErrorRange { get; set; } = 40;

        /// <summary>
        /// 活体检测时间范围(双摄像头时活体检测与人脸较验异步进行，存在时间差)
        /// </summary>
        public decimal LiveDetectAbleSecondTime { get; set; } = 0.1M;

        /// <summary>
        /// 是否检测活体
        /// </summary>
        public bool IsCheckLiveness { get; set; } = false;
    }
}
