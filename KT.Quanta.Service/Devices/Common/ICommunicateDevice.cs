using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 远程设备
    /// </summary>
    public interface ICommunicateDevice
    {
        /// <summary>
        /// 远程设备数据
        /// </summary>
        CommunicateDeviceInfoModel CommunicateDeviceInfo { get; }

        /// <summary>
        /// 登录客户端对象
        /// </summary>
        T GetLoginUserClient<T>();

        /// <summary>
        /// 关闭连接
        /// </summary>
        Task CloseAsync();

        /// <summary>
        /// 心跳
        /// </summary>
        Task HeartbeatAsync();

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="remoteDevice"></param>
        Task InitAsync(CommunicateDeviceInfoModel communicateDevice, RemoteDeviceModel remoteDevice);

        /// <summary>
        /// 查检是否连接，如果未连接则连接
        /// </summary>
        /// <returns>最络连接状态</returns>
        Task<bool> CheckAndLinkAsync();
    }
}
