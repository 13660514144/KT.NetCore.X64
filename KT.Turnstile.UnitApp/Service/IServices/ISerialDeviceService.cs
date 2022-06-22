using KT.Turnstile.Unit.Entity.Entities;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.IServices
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
        void InitAllCardDeviceAsync();
    }
}
