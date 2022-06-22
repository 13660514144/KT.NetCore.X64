using KT.Common.Data.Daos;
using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 读卡器设备信息数据持久化
    /// </summary>
    public interface ICardDeviceDao : IBaseDataDao<CardDeviceEntity>
    {
        /// <summary>
        /// 根据Id获取读卡器设备信息
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>读卡器设备信息详情</returns>
        Task<CardDeviceEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有读卡器设备信息
        /// </summary>
        /// <returns>读卡器设备信息列表</returns>
        Task<List<CardDeviceEntity>> GetAllAsync();

        /// <summary>
        /// 查询所有读卡器设备信息
        /// </summary>
        /// <returns>读卡器设备信息列表</returns>
        Task<List<CardDeviceEntity>> GetByProcessorIdAsync(string id);

        /// <summary>
        /// 新增读卡器设备
        /// </summary>
        /// <param name="entity">读卡器设备详情</param>
        /// <returns>是否成功</returns>
        Task<CardDeviceEntity> AddAsync(CardDeviceEntity entity);

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <param name="entity">读卡器设备详情</param>
        /// <returns>是否成功</returns>
        Task<CardDeviceEntity> EditAsync(CardDeviceEntity entity);

        /// <summary>
        /// 物理删除读卡器设备
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// 物理删除读卡器设备
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>是否成功</returns>
        Task<CardDeviceEntity> DeleteReturnWidthProcessorAsync(string id);
    }
}
