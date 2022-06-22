using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Visitor.Common.Settings
{
    public class AppSettings
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 相机
        /// </summary>
        public int CameraIndex { get; set; } = 0;

        /// <summary>
        /// 二维码打印模板
        /// </summary>
        public string QrCodePrintTemplate { get; set; } = "VisitQrCodeDocument.xaml";

        /// <summary>
        /// 相机旋转角度
        /// </summary>
        public double CameraTransformAngle { get; set; }

        /// <summary>
        /// 手机号校验规则
        /// </summary>
        public string PhoneRegular { set; get; } = "^(1[3-9])[0-9]{9}$";

        /// <summary>
        /// 操作过期时间
        /// </summary>
        public int OperationTimeOutSecond { set; get; } = 30;

        /// <summary>
        /// 是否全屏
        /// </summary>
        public bool IsFullScreen { get; set; } = true;

        /// <summary>
        /// 照片格式
        /// </summary>
        public string PhotoExtenstion { get; set; } = ".jpg";

        /// <summary>
        /// Lgo
        /// </summary>
        public string LogoPath { get; set; } = "Resources/Images/logo.png";

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; } = "康塔智慧通行平台";

        /// <summary>
        /// 系统副名称
        /// </summary>
        public string SystemSecondName { get; set; } = "欢迎您";

        /// <summary>
        /// 版权声明
        /// </summary>
        public string SystemCopyright { get; set; } = " QuantaData.All Rights Reserved. 广州康塔科技有限公司  ";
        public string Uritree { get; set; } = "";
        public string Urimsg { get; set; } = "";
        public string BaseUrl { get; set; } = "";
        public JsHeader Js { get; set; } = new JsHeader();
        public class JsHeader
        {
            public string Token { get; set; } = "";
            public string Secret { get; set; } = "";
            public string LoginName { get; set; } = "";
            public string Account { get; set; } = "";            
        }
    }
}
