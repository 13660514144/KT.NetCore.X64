using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 分发数据错误记录信息数据持久化
    /// </summary>
    public interface IDistributeErrorDao : IBaseDataDao<DistributeErrorEntity>
    {
        /// <summary>
        /// 根据Id获取分发数据错误记录信息
        /// </summary>
        /// <param name="id">分发数据错误记录Id</param>
        /// <returns>分发数据错误记录信息详情</returns>
        Task<DistributeErrorEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有分发数据错误记录信息
        /// </summary>
        /// <returns>分发数据错误记录信息列表</returns>
        Task<List<DistributeErrorEntity>> GetAllAsync();

        /// <summary>
        /// 新增分发数据错误记录
        /// </summary>
        /// <param name="entity">分发数据错误记录详情</param>
        /// <returns>是否成功</returns>
        Task<DistributeErrorEntity> AddAsync(DistributeErrorEntity entity);

        /// <summary>
        /// 修改分发数据错误记录
        /// </summary>
        /// <param name="entity">分发数据错误记录详情</param>
        /// <returns>是否成功</returns>
        Task<DistributeErrorEntity> EditAsync(DistributeErrorEntity entity);

        /// <summary>
        /// 物理删除分发数据错误记录
        /// </summary>
        /// <param name="id">分发数据错误记录Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 物理删除分发数据错误记录
        /// </summary>
        /// <param name="id">分发数据错误记录Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(List<DistributeErrorEntity> entities);

        /// <summary>
        /// 根据边缘处理器Id获取数据
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns></returns>
        Task<List<DistributeErrorEntity>> GetByProcessorIdAsync(string id);

    }
}
