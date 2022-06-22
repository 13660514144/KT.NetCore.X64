using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IServices
{
    /// <summary>
    /// 大厦信息
    /// </summary>
    public interface IEdificeService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦列表</returns>
        Task<List<EdificeModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取大厦信息
        /// </summary>
        /// <param name="id">大厦Id</param>
        /// <returns>大厦信息详情</returns>
        Task<EdificeModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改大厦
        /// </summary>
        /// <param name="model">大厦详情</param>
        /// <returns>大厦详情</returns>
        Task<EdificeModel> AddOrEditAsync(EdificeModel model);

        /// <summary>
        /// 物理删除大厦
        /// </summary>
        /// <param name="id">大厦Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 获取所有大厦，包含楼层数据
        /// </summary>
        /// <returns>大厦列表</returns>
        Task<List<EdificeModel>> GetAllWithFloorAsync();

        /// <summary>
        /// 根据Id获取大厦信息，包含楼层数据
        /// </summary>
        /// <param name="id">大厦Id</param>
        /// <returns>大厦信息详情</returns>
        Task<EdificeModel> GetWithFloorByIdAsync(string id);

        /// <summary>
        /// 修改大厦，包含楼层数据
        /// </summary>
        /// <param name="model">大厦详情</param>
        /// <returns>大厦详情</returns>
        Task<EdificeModel> AddOrEditWithFloorAsync(EdificeModel model);

        /// <summary>
        /// 物理删除大厦，包含楼层数据
        /// </summary>
        /// <param name="id">大厦Id</param>
        /// <returns>是否成功</returns>
        Task DeleteWithFloorAsync(string id);
    }
}
