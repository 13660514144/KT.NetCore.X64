namespace KT.Device.Unit.CardReaders.Models
{
    public interface ISerialDeviceModel
    {
        /// <summary>
        /// 串口名
        /// </summary>
        string PortName { get; set; }

        /// <summary>
        /// 串口波特率
        /// </summary>
        int Baudrate { get; set; }

        /// <summary>
        /// 串口数据位
        /// </summary>
        int Databits { get; set; }

        /// <summary>
        /// 串口停止位
        /// </summary>
        int Stopbits { get; set; }

        /// <summary>
        /// 串口校验位
        /// </summary>
        int Parity { get; set; }

        /// <summary>
        /// 串口数据读取超时
        /// </summary>
        int ReadTimeout { get; set; }

        /// <summary>
        /// 串口编码方式
        /// </summary>
        string Encoding { get; set; }

    }
}
