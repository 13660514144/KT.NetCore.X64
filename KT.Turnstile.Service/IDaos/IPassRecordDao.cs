using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 通行权限数据持久化
    /// </summary>
    public interface IPassRecordDao : IBaseDataDao<PassRecordEntity>
    {
        /// <summary>
        /// 根据Id获取通行权限信息
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>通行权限信息详情</returns>
        Task<PassRecordEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<PassRecordEntity>> GetAllAsync();

        /// <summary>
        /// 新增通行权限
        /// </summary>
        /// <param name="entity">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRecordEntity> AddAsync(PassRecordEntity entity);

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <param name="entity">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRecordEntity> EditAsync(PassRecordEntity entity);

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);
    }
}
