using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Secondary.ClientApp
{
    /// <summary>
    /// 输入设备配置
    /// </summary>
    public class InputDeviceSettings
    {
        /// <summary>
        /// 相机设置
        /// </summary>
        public CameraSettings CameraSettings { get; set; }

        /// <summary>
        /// IC卡阅读器设置
        /// </summary>
        public IcCardDeviceSettings IcCardDeviceSettings { get; set; }

        /// <summary>
        /// 二维码卡阅读器设置
        /// </summary>
        public QrCodeDeviceSettings QrCodeDeviceSettings { get; set; }
    }

    /// <summary>
    /// 相机设置
    /// </summary>
    public class CameraSettings
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// IC卡阅读器设置
    /// </summary>
    public class IcCardDeviceSettings
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 串口设置
        /// </summary>
        public SerialDeviceSettings SerialSettings { get; set; }
    }

    /// <summary>
    /// 二维码卡阅读器设置
    /// </summary>
    public class QrCodeDeviceSettings
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 串口设置
        /// </summary>
        public SerialDeviceSettings SerialSettings { get; set; }
    }

    public class SerialDeviceSettings
    {
        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baudrate { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int Databits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public int Stopbits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public int Parity { get; set; }

        /// <summary>
        /// 数据读取超时
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// 编码方式
        /// </summary>
        public string Encoding { get; set; }
    }
}
