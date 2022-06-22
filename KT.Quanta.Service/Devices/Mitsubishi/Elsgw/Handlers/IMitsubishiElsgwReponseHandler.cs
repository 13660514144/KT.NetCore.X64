using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Handlers
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public interface IMitsubishiElsgwReponseHandler
    {
        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task HeartbeatAsync(MitsubishiElsgwTransmissionHeader header);

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task SingleFloorCallAsync(MitsubishiElsgwTransmissionHeader header);

        /// <summary>
        /// 多楼层派梯
        /// </summary>
        /// <param name="response"></param>
        Task MultiFloorCallAsync(MitsubishiElsgwTransmissionHeader response);
    }
}
