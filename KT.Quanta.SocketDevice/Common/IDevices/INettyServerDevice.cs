using System;
using System.Threading.Tasks;

namespace KT.Quanta.SocketDevice.IDevices
{
    /// <summary>
    /// Socket Netty服务端设备
    /// </summary>
    public interface INettyServerDevice : IDisposable
    {
        /// <summary>
        /// 对接收到的数据进行操作
        /// </summary>
        Task ReceiveAnsyc(object data);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message">发送字符串</param>
        /// <returns></returns>
        Task SendAnsyc(string message);
    }
}
