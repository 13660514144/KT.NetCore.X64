using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 串口配置信息数据数据持久化
    /// </summary>
    public interface ISerialConfigDao : IBaseDataDao<SerialConfigEntity>
    {
        /// <summary>
        /// 根据Id获取串口配置信息
        /// </summary>
        /// <param name="id">串口配置Id</param>
        /// <returns>串口配置信息详情</returns>
        Task<SerialConfigEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有串口配置信息
        /// </summary>
        /// <returns>串口配置信息列表</returns>
        Task<List<SerialConfigEntity>> GetAllAsync();

        /// <summary>
        /// 新增串口配置
        /// </summary>
        /// <param name="entity">串口配置详情</param>
        /// <returns>是否成功</returns>
        Task<SerialConfigEntity> AddAsync(SerialConfigEntity entity);

        /// <summary>
        /// 修改串口配置
        /// </summary>
        /// <param name="entity">串口配置详情</param>
        /// <returns>是否成功</returns>
        Task<SerialConfigEntity> EditAsync(SerialConfigEntity entity);

        /// <summary>
        /// 物理串口配置
        /// </summary>
        /// <param name="id">串口配置Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);
    }
}
