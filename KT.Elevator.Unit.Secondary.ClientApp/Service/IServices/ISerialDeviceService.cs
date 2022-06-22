using KT.Elevator.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.IServices
{
    /// <summary>
    /// 硬件读卡器设备操作
    /// </summary>
    public interface ISerialDeviceService
    {
        /// <summary>
        /// 初始化所有设备
        /// 数据库中查询的设备信息，开机启动时启用
        /// </summary>
        Task InitAllCardDeviceAsync();
         
        /// <summary>
        /// 初始化二次派梯一体机二维码阅读器
        /// </summary>
        /// <returns></returns>
        Task InitQrCodeDeviceAsync();
    }
}
