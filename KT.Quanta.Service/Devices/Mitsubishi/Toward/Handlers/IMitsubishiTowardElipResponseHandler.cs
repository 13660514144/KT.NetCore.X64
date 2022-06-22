using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers
{
    public interface IMitsubishiTowardElipResponseHandler
    {
        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task HeartbeatAsync(MitsubishiElipCommunicationHeader header);

        /// <summary>
        /// 派梯结果
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task AcceptanceAsync(IChannelHandlerContext context, MitsubishiElipVerificationAcceptanceModel data, MitsubishiElipHandleElevatorSequenceModel sequence);
    }
}