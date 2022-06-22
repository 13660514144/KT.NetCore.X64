using KT.Elevator.Unit.Entity.Entities;
using System;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Device.IDevices
{
    /// <summary>
    /// 继电器设备
    /// </summary>
    public interface ISocketDevice : IDisposable
    {
        /// <summary>
        /// 继电器设备
        /// </summary>
        UnitCardDeviceEntity SocketDevice { get; }

        /// <summary>
        /// 初始化继电器设备
        /// </summary>
        /// <param name="device">继电器设备信息</param>
        Task InitAsync(UnitCardDeviceEntity device);

        /// <summary>
        /// 对接收到的数据进行操作
        /// </summary>
        Task ReceiveExecAnsyc(object data);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">发送字符串</param>
        /// <returns></returns>
        Task SendExceAnsyc(string message);
    }
}
