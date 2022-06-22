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
    public interface IPassRecordService
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
        Task<PassRecordModel> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有通行权限信息
        /// </summary>
        /// <returns>通行权限信息列表</returns>
        Task<List<PassRecordModel>> GetAllAsync();

        /// <summary>
        /// 新增通行权限
        /// </summary>
        /// <param name="model">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRecordModel> AddAsync(PassRecordModel model);

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <param name="model">通行权限详情</param>
        /// <returns>是否成功</returns>
        Task<PassRecordModel> EditAsync(PassRecordModel model);

        /// <summary>
        /// 获取最新一条记录
        /// </summary>
        /// <returns></returns>
        Task<PassRecordModel> GetLastAsync();

        /// <summary>
        /// 物理删除通行权限
        /// </summary>
        /// <param name="id">通行权限Id</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(string id);
         
        /// <summary>
        /// 推送通行记录
        /// </summary>
        /// <param name="unitPassRecord"></param>
        /// <returns></returns>
        Task PushPassRecord(TurnstileUnitPassRecordEntity unitPassRecord);

        /// <summary>
        /// 如果不存在则新增
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        Task AddIfNoExistsAsync(PassRecordModel passRecord);
    }
}
