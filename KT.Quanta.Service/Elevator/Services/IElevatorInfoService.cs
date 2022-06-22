using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IServices
{
    /// <summary>
    /// 电梯信息
    /// </summary>
    public interface IElevatorInfoService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有电梯
        /// </summary>
        /// <returns>电梯列表</returns>
        Task<List<ElevatorInfoModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取电梯信息
        /// </summary>
        /// <param name="id">电梯Id</param>
        /// <returns>电梯信息详情</returns>
        Task<ElevatorInfoModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改电梯
        /// </summary>
        /// <param name="model">电梯详情</param>
        /// <returns>电梯详情</returns>
        Task<ElevatorInfoModel> AddOrEditAsync(ElevatorInfoModel model);

        /// <summary>
        /// 物理删除电梯
        /// </summary>
        /// <param name="id">电梯Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 根据真实id查找电梯信息
        /// </summary>
        /// <param name="realId"></param>
        /// <returns></returns>
        Task<ElevatorInfoModel> GetByElevatorGroupIdAndRealIdAsync(string elevatorGroupId, string realId);

        /// <summary>
        /// 根据电梯组创建电梯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ElevatorGroupModel> AddOrEditByElevatorGroupId(ElevatorGroupModel model);
        Task DeleteByElevatorGroupId(string elevatorGroupId);
    }
}
