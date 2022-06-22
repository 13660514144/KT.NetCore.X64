using System.IO.Ports;

namespace KT.Device.Unit.CardReaders.Models
{
    /// <summary>
    /// 串口数据原型转换
    /// </summary>
    public class SerialDeviceModelConvert
    {
        /// <summary>
        /// 设备原型转换成串口信息
        /// </summary>
        /// <param name="device">设备信息</param>
        /// <returns>串口信息</returns>
        public static SerialPort ToSerialPort(ISerialDeviceModel device)
        {
            SerialPort sp = new SerialPort();
            //串口名
            sp.PortName = device.PortName;
            //波特率
            sp.BaudRate = device.Baudrate;
            //数据位   
            sp.DataBits = device.Databits;
            //停止位
            sp.StopBits = (StopBits)device.Stopbits;
            //校验位
            sp.Parity = (Parity)device.Parity;
            //设置数据读取超时为1秒
            sp.ReadTimeout = device.ReadTimeout;
          
           
            ////GB2312即信息交换用汉字编码字符集
            //sp.Encoding = Encoding.GetEncoding(device.Encoding);

            return sp;
        }
    }
}
