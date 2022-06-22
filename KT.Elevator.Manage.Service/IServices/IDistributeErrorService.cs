using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.IServices
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
        /// 修改分发数据错误记录
        /// </summary>
        /// <param name="model">分发数据错误记录详情</param>
        /// <returns>分发数据错误记录详情</returns>
        Task<DistributeErrorModel> EditAsync(DistributeErrorModel model);

        /// <summary>
        /// 物理删除分发数据错误记录
        /// </summary>
        /// <param name="id">分发数据错误记录Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 增加一次错误次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> AddErrorTimesAsync(string id);

        /// <summary>
        /// 是否存在推送错误内容
        /// </summary>
        /// <param name="deviceKey">设备Key</param>
        /// <returns>是否存在内容</returns>
        Task<bool> IsExistsByDeviceKeyAsync(string deviceKey);
    }
}
