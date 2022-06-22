using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 边缘处理器数据持久化
    /// </summary>
    public interface IProcessorDao : IBaseDataDao<ProcessorEntity>
    {
        /// <summary>
        /// 根据Id获取边缘处理器信息
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns>边缘处理器信息详情</returns>
        Task<ProcessorEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有边缘处理器信息
        /// </summary>
        /// <returns>边缘处理器信息列表</returns>
        Task<List<ProcessorEntity>> GetAllAsync();

        /// <summary>
        /// 新增边缘处理器
        /// </summary>
        /// <param name="entity">边缘处理器详情</param>
        /// <returns>边缘处理器详情</returns>
        Task<ProcessorEntity> AddAsync(ProcessorEntity entity);

        /// <summary>
        /// 修改边缘处理器
        /// </summary>
        /// <param name="entity">边缘处理器详情</param>
        /// <returns>边缘处理器详情</returns>
        Task<ProcessorEntity> EditAsync(ProcessorEntity entity);

        /// <summary>
        /// 物理删除边缘处理器
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);
    }
}
