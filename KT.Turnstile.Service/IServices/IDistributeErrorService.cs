﻿using KT.Turnstile.Common.Enums;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IServices
{
    /// <summary>
    /// 分发数据错误记录信息
    /// </summary>
    public interface IDistributeErrorService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有分发数据错误记录
        /// </summary>
        /// <returns>分发数据错误记录列表</returns>
        Task<List<DistributeErrorModel>> GetAllAsync();

        /// <summary>
        /// 获取推送到边缘处理器的数据
        /// </summary>
        /// <param name="processorKey"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        Task<List<UnitErrorModel>> GetUnitPageAndDeleteByProcessorKeyAsync(string processorKey, int page, int size);

        /// <summary>
        /// 根据Id获取分发数据错误记录信息
        /// </summary>
        /// <param name="id">分发数据错误记录Id</param>
        /// <returns>分发数据错误记录信息详情</returns>
        Task<DistributeErrorModel> GetByIdAsync(string id);
 
        /// <summary>
        /// 新增分发数据错误记录
        /// </summary>
        /// <param name="model">分发数据错误记录详情</param>
        /// <returns>分发数据错误记录详情</returns>
        Task AddRetryAsync(DistributeErrorModel model);
 
        /// <summary>
        /// 物理删除分发数据错误记录
        /// </summary>
        /// <param name="id">分发数据错误记录Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 根据边缘处理器Id获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<DistributeErrorModel>> GetByProcessorIdAsync(string id);

        /// <summary>
        /// 增加一次错误次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> AddErrorTimesAsync(string id);
    }
}
