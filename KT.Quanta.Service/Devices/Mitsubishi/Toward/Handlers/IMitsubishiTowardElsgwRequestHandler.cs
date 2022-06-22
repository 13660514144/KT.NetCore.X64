using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElipClients;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers
{
    public interface IMitsubishiTowardElsgwRequestHandler
    {
        /// <summary>
        /// 对应转换的客户端
        /// </summary>
        IMitsubishiElipClientHostBase MitsubishiElipClientHost { get; set; }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        Task HeartbeatAsync(IChannelHandlerContext context, MitsubishiElsgwTransmissionHeader response, EndPoint endPoint);

        /// <summary>
        /// 单楼层派梯
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        Task SingleFloorCallAsync(MitsubishiElsgwTransmissionHeader response, EndPoint endPoint);

        /// <summary>
        /// 多楼层派梯
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        Task MultiFloorCallAsync(MitsubishiElsgwTransmissionHeader response, EndPoint endPoint);
    }
}