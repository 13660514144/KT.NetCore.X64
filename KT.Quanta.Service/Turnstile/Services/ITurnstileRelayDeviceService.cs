using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.IServices
{
    /// <summary>
    /// 继电器设备信息
    /// </summary>
    public interface ITurnstileRelayDeviceService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有继电器设备
        /// </summary>
        /// <returns>继电器设备列表</returns>
        Task<List<TurnstileRelayDeviceModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取继电器设备信息
        /// </summary>
        /// <param name="id">继电器设备Id</param>
        /// <returns>继电器设备信息详情</returns>
        Task<TurnstileRelayDeviceModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改继电器设备
        /// </summary>
        /// <param name="model">继电器设备详情</param>
        /// <returns>继电器设备详情</returns>
        Task<TurnstileRelayDeviceModel> AddOrEditAsync(TurnstileRelayDeviceModel model);

        /// <summary>
        /// 物理删除继电器设备
        /// </summary>
        /// <param name="id">继电器设备Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);
    }
}
