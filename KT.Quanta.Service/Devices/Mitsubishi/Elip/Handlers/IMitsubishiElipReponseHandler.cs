using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Handlers
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public interface IMitsubishiElipReponseHandler
    {
        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task HeartbeatAsync(MitsubishiElipCommunicationHeader header);

        /// <summary>
        /// 自动派梯
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task AcceptanceAsync(MitsubishiElipVerificationAcceptanceModel data, MitsubishiElipHandleElevatorSequenceModel sequence);
    }
}
