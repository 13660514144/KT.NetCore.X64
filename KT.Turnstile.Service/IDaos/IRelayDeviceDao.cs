using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 继电器设备信息数据持久化
    /// </summary>
    public interface IRelayDeviceDao : IBaseDataDao<RelayDeviceEntity>
    {
        /// <summary>
        /// 根据Id获取继电器设备信息
        /// </summary>
        /// <param name="id">继电器设备Id</param>
        /// <returns>继电器设备信息详情</returns>
        Task<RelayDeviceEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有继电器设备信息
        /// </summary>
        /// <returns>继电器设备信息列表</returns>
        Task<List<RelayDeviceEntity>> GetAllAsync();

        /// <summary>
        /// 新增继电器设备
        /// </summary>
        /// <param name="entity">继电器设备详情</param>
        /// <returns>是否成功</returns>
        Task<RelayDeviceEntity> AddAsync(RelayDeviceEntity entity);

        /// <summary>
        /// 修改继电器设备
        /// </summary>
        /// <param name="entity">继电器设备详情</param>
        /// <returns>是否成功</returns>
        Task<RelayDeviceEntity> EditAsync(RelayDeviceEntity entity);

        /// <summary>
        /// 物理删除继电器设备
        /// </summary>
        /// <param name="id">继电器设备Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);
    }
}
