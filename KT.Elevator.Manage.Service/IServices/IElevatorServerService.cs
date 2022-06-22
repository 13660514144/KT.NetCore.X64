using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IServices
{
    /// <summary>
    /// 电梯服务信息
    /// </summary>
    public interface IElevatorServerService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有电梯服务
        /// </summary>
        /// <returns>电梯服务列表</returns>
        Task<List<ElevatorServerModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取电梯服务信息
        /// </summary>
        /// <param name="id">电梯服务Id</param>
        /// <returns>电梯服务信息详情</returns>
        Task<ElevatorServerModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改电梯服务
        /// </summary>
        /// <param name="model">电梯服务详情</param>
        /// <returns>电梯服务详情</returns>
        Task<ElevatorServerModel> AddOrEditAsync(ElevatorServerModel model);

        /// <summary>
        /// 物理删除电梯服务
        /// </summary>
        /// <param name="id">电梯服务Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);
    }
}
