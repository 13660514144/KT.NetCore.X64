using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.SerialDevice.Common.Handlers
{
    /// <summary>
    /// 串口事件处理
    /// </summary>
    public interface ISerialHandler
    {
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <typeparam name="T">接收数据类型</typeparam>
        /// <param name="data">接收的数据</param>
        /// <returns></returns>
        Task ReceiveAsync<T>(T data);
    }
}
