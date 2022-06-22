﻿using KT.Quanta.Common.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.IServices
{
    /// <summary>
    /// 边缘处理器数据存储服务
    /// </summary>
    public interface ITurnstileProcessorService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 根据Id获取边缘处理器信息
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns>边缘处理器信息详情</returns>
        Task<TurnstileProcessorModel> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有边缘处理器信息
        /// </summary>
        /// <returns>边缘处理器信息列表</returns>
        Task<List<TurnstileProcessorModel>> GetFromDeviceTypeAsync();

        /// <summary>
        /// 修改边缘处理器
        /// </summary>
        /// <param name="model">边缘处理器详情</param>
        /// <returns>是否成功</returns>
        Task<TurnstileProcessorModel> AddOrEditAsync(TurnstileProcessorModel model);

        /// <summary>
        /// 物理删除边缘处理器
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);
 
        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="processorKey"></param>
        /// <returns>返回是否包含错误数据</returns>
        Task<bool> LinkReturnHasErrorAsync(SeekSocketModel seekSocket, string connectionId);

        /// <summary>
        /// 初始化加载数据
        /// </summary>
        /// <returns></returns>
        Task InitLoadAsync();

        /// <summary>
        /// 获取边缘处理器
        /// </summary>
        /// <returns></returns>
        Task<List<TurnstileProcessorModel>> GetStaticListAsync();
    }
}
