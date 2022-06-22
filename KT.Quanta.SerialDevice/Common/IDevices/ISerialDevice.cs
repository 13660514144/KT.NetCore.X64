using System.Threading.Tasks;
using System;
using System.IO.Ports;
using System.Text;
using KT.Quanta.SerialDevice.Common.Handlers;

namespace KT.Quanta.SerialDevice.Common.IDevices
{
    /// <summary>
    /// 串口设备
    /// </summary>
    public interface ISerialDevice : IDisposable
    {
        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encoding { get; }

        /// <summary>
        /// 串口数据
        /// </summary>
        SerialPort SerialPort { get; }

        /// <summary>
        /// 初始化读卡器设备
        /// </summary>
        /// <param name="device">读卡器设备信息</param>
        Task InitAsync(ISerialDataAnalyze dataAnalyze, ISerialHandler serialHandler);

        /// <summary>
        /// 对接收到的数据进行操作
        /// </summary>
        Task ReceiveAnsyc<T>(T data);

        /// <summary>
        /// 开启连接
        /// </summary>
        /// <returns></returns>
        Task OpenAsync();

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        Task CloseAsync();
    }
}
