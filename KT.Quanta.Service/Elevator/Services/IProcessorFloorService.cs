using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.IServices
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    public interface IProcessorFloorService
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
        Task<List<ProcessorFloorModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取读卡器设备信息
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>读卡器设备信息详情</returns>
        Task<ProcessorFloorModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <param name="model">读卡器设备详情</param>
        /// <returns>读卡器设备详情</returns>
        Task<ProcessorFloorModel> AddOrEditAsync(ProcessorFloorModel model);

        /// <summary>
        /// 物理删除读卡器设备
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ProcessorModel> AddOrEditByProcessorIdAsync(ProcessorModel model);

        /// <summary>
        /// 根据边缘处理器删除楼层映射
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByProcessorIdAsync(string id);

        /// <summary>
        /// 根据边缘处理器获取楼层输出映射信息
        /// </summary>
        /// <returns></returns>
        Task<ProcessorModel> GetByProcessorIdAsync(string processorId);

        /// <summary>
        /// 从设备中获取
        /// </summary>
        /// <param name="processorId">边缘处理器id</param>
        /// <returns></returns>
        Task<List<ProcessorFloorModel>> GetInitByProcessorIdAsync(string processorId);

        /// <summary>
        /// 从设备中获取
        /// </summary>
        /// <returns></returns>
        Task<List<ProcessorModel>> GetInitAsync();

    }
}
