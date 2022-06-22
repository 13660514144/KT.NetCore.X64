using KT.Turnstile.Model.Models;
using KT.Turnstile.Unit.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Services
{
    /// <summary>
    /// 通行权限数据存储服务
    /// </summary>
    public interface ICardDeviceRightGroupService
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
        Task<CardDeviceRightGroupModel> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<CardDeviceRightGroupModel>> GetAllAsync();

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 新增或修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CardDeviceRightGroupModel> AddOrEditAsync(CardDeviceRightGroupModel model);


        /// <summary>
        /// 获取边缘处理器权限组信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<TurnstileUnitRightGroupEntity>> GetUnitAllAsync(int page, int size);
    }
}
