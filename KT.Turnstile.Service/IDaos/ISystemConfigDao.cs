using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 系统配置数据持久化
    /// </summary>
    public interface ISystemConfigDao : IBaseDataDao<SystemConfigEntity>
    {
        /// <summary>
        /// 根据Id获取系统配置信息
        /// </summary>
        /// <param name="id">系统配置Id</param>
        /// <returns>系统配置信息详情</returns>
        Task<SystemConfigEntity> GetByIdAsync(string id);

        /// <summary>
        /// 根据Id获取系统配置信息
        /// </summary>
        /// <param name="key">系统配置key</param>
        /// <returns>系统配置信息详情</returns>
        Task<SystemConfigEntity> GetByKeyAsync(string key);

        /// <summary>
        /// 查询所有系统配置信息
        /// </summary>
        /// <returns>系统配置信息列表</returns>
        Task<List<SystemConfigEntity>> GetAllAsync();

        /// <summary>
        /// 新增系统配置
        /// </summary>
        /// <param name="entity">系统配置详情</param>
        /// <returns>是否成功</returns>
        Task<SystemConfigEntity> AddAsync(SystemConfigEntity entity);

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="entity">系统配置详情</param>
        /// <returns>是否成功</returns>
        Task<SystemConfigEntity> EditAsync(SystemConfigEntity entity);

        /// <summary>
        /// 物理删除系统配置
        /// </summary>
        /// <param name="id">系统配置Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);
    }
}
