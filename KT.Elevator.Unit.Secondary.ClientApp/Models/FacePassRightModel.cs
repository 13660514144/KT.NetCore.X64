using System;

namespace KT.Elevator.Unit.Secondary.ClientApp.Models
{
    /// <summary>
    /// 人脸数据，存于内存用于快速匹配
    /// </summary>
    public class FacePassRightModel
    {
        /// <summary>
        /// id主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 人脸特性
        /// </summary>
        public IntPtr Feature { get; set; }

        /// <summary>
        /// 起始时间，UTC毫秒
        /// </summary>
        public long TimeStart { get; set; }

        /// <summary>
        /// 过期时间，UTC毫秒
        /// </summary>
        public long TimeEnd { get; set; }

    }
}
