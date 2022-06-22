using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Kone.Requests;
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
    public interface IKoneEliReponseHandler
    {
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task DisconnectAsync(KoneEliHeaderResponse headResponse, byte[] buffer);

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task HeartBeatAsync(KoneEliHeaderResponse headResponse, byte[] buffer);

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task PaddleAsync(Requests.KoneEliHeaderResponse headResponse, Requests.KoneEliSubHeader1Response subHeadResponse);

        /// <summary>
        /// 状态返回
        /// </summary>
        /// <param name="headResponse"></param>
        /// <param name="subHeadResponse"></param>
        /// <returns></returns>
        Task StatusAsync(KoneEliHeaderResponse headResponse, KoneEliSubHeader1Response subHeadResponse, IChannelHandlerContext context);

        /// <summary>
        /// dop关闭
        /// </summary>
        /// <param name="headResponse"></param>
        /// <param name="subHeadResponse"></param>
        /// <returns></returns>
        Task DopClosedAsync(KoneEliHeaderResponse headResponse, KoneEliSubHeader1Response subHeadResponse);
    }
}
