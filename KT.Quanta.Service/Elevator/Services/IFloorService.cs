using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IServices
{
    /// <summary>
    /// 楼层信息
    /// </summary>
    public interface IFloorService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层列表</returns> 
        Task<List<FloorModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取楼层信息
        /// </summary>
        /// <param name="id">楼层Id</param>
        /// <returns>楼层信息详情</returns>
        Task<FloorModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改楼层
        /// </summary>
        /// <param name="model">楼层详情</param>
        /// <returns>楼层详情</returns>
        Task<FloorModel> AddOrEditAsync(FloorModel model);

        /// <summary>
        /// 物理删除楼层
        /// </summary>
        /// <param name="id">楼层Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 根据大厦Id获取楼层信息
        /// </summary>
        /// <param name="id">大厦Id</param>
        /// <returns>楼层列表</returns>
        Task<List<FloorModel>> GetByEdificeIdAsync(string id);


        Task<List<FloorModel>> EditDirectionsAsync(List<FloorModel> models);
    }
}
