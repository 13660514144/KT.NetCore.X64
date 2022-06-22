using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public interface IKoneRcgifReponseHandler
    {
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task DisconnectAsync(Requests.KoneRcgifHeaderResponse headResponse, byte[] buffer);

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task HeartBeatAsync(Requests.KoneRcgifHeaderResponse headResponse, byte[] buffer);

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task PaddleAsync(Requests.KoneRcgifHeaderResponse headResponse, Requests.KoneRcgifSubHeader1Response subHeadResponse);
    }
}
