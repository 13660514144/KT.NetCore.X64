using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IServices
{
    /// <summary>
    /// 电梯组信息
    /// </summary>
    public interface IElevatorGroupService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有电梯组
        /// </summary>
        /// <returns>电梯组列表</returns>
        Task<List<ElevatorGroupModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取电梯组信息
        /// </summary>
        /// <param name="id">电梯组Id</param>
        /// <returns>电梯组信息详情</returns>
        Task<ElevatorGroupModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改电梯组
        /// </summary>
        /// <param name="model">电梯组详情</param>
        /// <returns>电梯组详情</returns>
        Task<ElevatorGroupModel> AddOrEditAsync(ElevatorGroupModel model);

        /// <summary>
        /// 物理删除电梯组
        /// </summary>
        /// <param name="id">电梯组Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);
    }
}
