using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    /// <summary>
    /// 通行权限数据存储服务
    /// </summary>
    public interface IPassRightService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 根据Id获取通行权限信息
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>通行权限信息详情</returns>
        Task<PassRightModel> GetByIdAsync(string id);
 
        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<PassRightModel>> GetAllAsync();

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <param name="model">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRightModel> AddOrEditAsync(PassRightModel model);

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 根据边缘处理器获取权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        Task<List<UnitPassRightEntity>> GetUnitByPageAsync(int page, int size);

        /// <summary>
        /// 根据卡号或人脸id删除通行权限
        /// </summary>
        /// <param name="sign">卡号或人脸id</param>
        Task DeleteBySignAsync(string sign);
        Task<PassRightModel> GetBySignAsync(string sign);
    }
}
