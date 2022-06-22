using KT.Quanta.Service.Turnstile.Dtos;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.Services
{
    /// <summary>
    /// 通行权限数据存储服务
    /// </summary>
    public interface ITurnstilePassRightService
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
        Task<TurnstilePassRightModel> GetBySignAndAccessTypeAsync(string sign, string accessType);

        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<TurnstilePassRightModel>> GetAllAsync();

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <param name="model">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<TurnstilePassRightModel> AddOrEditAsync(TurnstilePassRightModel model);

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(TurnstilePassRightModel model);

        /// <summary>
        /// 根据边缘处理器获取权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        Task<List<TurnstileUnitPassRightEntity>> GetUnitPageAsync(int page, int size);

        /// <summary>
        /// 根据Sign删除权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task DeleteBySignAsync(TurnstilePassRightModel model);
    }
}
