﻿using KT.Quanta.Service.Turnstile.Dtos;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.IServices
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    public interface ITurnstileCardDeviceService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备列表</returns>
        Task<List<TurnstileCardDeviceModel>> GetFromDeviceTypeAsync();

        /// <summary>
        /// 根据Id获取读卡器设备信息
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>读卡器设备信息详情</returns>
        Task<TurnstileCardDeviceModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <param name="model">读卡器设备详情</param>
        /// <returns>读卡器设备详情</returns>
        Task<TurnstileCardDeviceModel> AddOrEditAsync(TurnstileCardDeviceModel model);

        /// <summary>
        /// 物理删除读卡器设备
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 获取读卡器到边缘处理器信息
        /// </summary>
        /// <param name="processorId"></param>
        /// <returns></returns>
        Task<List<TurnstileUnitCardDeviceEntity>> GetUnitByProcessorId(string processorId);
    }
}
