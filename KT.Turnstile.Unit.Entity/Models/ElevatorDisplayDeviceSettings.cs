
using KT.Device.Unit.CardReaders.Models;

namespace KT.Turnstile.Unit.Entity.Models
{
    public class ElevatorDisplayDeviceSettings
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

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

    public class SerialDeviceSettings : ISerialDeviceModel
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
