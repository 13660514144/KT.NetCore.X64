using KT.Turnstile.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using KT.Turnstile.Common.Enums;
using System.Threading.Tasks;
using KT.Common.Data.Daos;

namespace KT.Turnstile.Manage.Service.IDaos
{
    /// <summary>
    /// 通行权限数据持久化
    /// </summary>
    public interface ICardDeviceRightGroupDao : IBaseDataDao<CardDeviceRightGroupEntity>
    {
        /// <summary>
        /// 根据Id获取通行权限信息
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>通行权限信息详情</returns>
        Task<CardDeviceRightGroupEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<CardDeviceRightGroupEntity>> GetAllAsync();

        /// <summary>
        /// 新增通行权限
        /// </summary>
        /// <param name="entity">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<CardDeviceRightGroupEntity> AddAsync(CardDeviceRightGroupEntity entity);

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <param name="entity">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<CardDeviceRightGroupEntity> EditAsync(CardDeviceRightGroupEntity entity);

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// 根据Id获取包含读卡器数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CardDeviceRightGroupEntity> GetWidthCardDeviceByIdAsync(string id);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<CardDeviceRightGroupEntity>> GetPageAsync(int page, int size);
    }
}
