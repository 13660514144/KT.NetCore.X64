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
    public interface IPassRightDao : IBaseDataDao<PassRightEntity>
    {
        /// <summary>
        /// 根据Id获取通行权限信息
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>通行权限信息详情</returns>
        Task<PassRightEntity> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<PassRightEntity>> GetAllAsync();

        /// <summary>
        /// 新增通行权限
        /// </summary>
        /// <param name="entity">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRightEntity> AddAsync(PassRightEntity entity);

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <param name="entity">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRightEntity> EditAsync(PassRightEntity entity);

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// 获取所有有权限的通行权限
        /// </summary>
        /// <param name="cardNumber">卡号</param>
        /// <param name="cardType">卡类型</param>
        /// <param name="passAisleId">通道</param> 
        /// <param name="passRightType">通行类型 <see cref="PassRightTypeEnum"/></param>
        /// <param name="passTime">通行时间</param> 
        /// <returns></returns>
        Task<List<PassRightEntity>> GetHasRightListAsync(string cardNumber, long passTime);

        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        Task<PassRightEntity> GetLastAsync(string cardNumber);

        /// <summary>
        /// 权限组查找权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<PassRightEntity>> GetByRightGroupId(string id);

        /// <summary>
        /// 根据边缘处理器获取权限
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        Task<List<PassRightEntity>> GetByProcessorIdAsync(string processorId);

        Task<List<PassRightEntity>> GetWithCardDeviceWidthProcessorByRightGroupId(string id);
        Task<List<PassRightEntity>> GetPageAsync(int page, int size);
    }
}
