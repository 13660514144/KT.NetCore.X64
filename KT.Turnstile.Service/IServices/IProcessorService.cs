using KT.Quanta.Common;
using KT.Quanta.Common.Models;
using KT.Turnstile.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.IServices
{
    /// <summary>
    /// 边缘处理器数据存储服务
    /// </summary>
    public interface IProcessorService
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
        Task<ProcessorModel> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有边缘处理器信息
        /// </summary>
        /// <returns>边缘处理器信息列表</returns>
        Task<List<ProcessorModel>> GetAllAsync();

        /// <summary>
        /// 修改边缘处理器
        /// </summary>
        /// <param name="model">边缘处理器详情</param>
        /// <returns>是否成功</returns>
        Task<ProcessorModel> AddOrEditAsync(ProcessorModel model);

        /// <summary>
        /// 物理删除边缘处理器
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 修改是否包含错误数据 
        /// </summary>
        /// <param name="has"></param>
        Task EditHasDistributeErrorAsync(string id, bool has);

        /// <summary>
        /// 根据ProcessorKey获取详细信息
        /// </summary>
        /// <param name="processorKey"></param>
        /// <returns></returns>
        Task<ProcessorModel> GetByKeyAsync(string processorKey);

        /// <summary>
        /// 获取静态列表数据
        /// </summary>
        /// <returns></returns>
        List<ProcessorModel> GetStaticListAsync();

        /// <summary>
        /// 客户端连接
        /// </summary>
        /// <param name="processorKey"></param>
        /// <returns>返回是否包含错误数据</returns>
        Task<bool> LinkReturnHasErrorAsync(SeekSocketModel seekSocket, string connectionId);

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="processorKey"></param>
        /// <returns></returns>
        void DisLink(string processorKey);

        /// <summary>
        /// 初始化边缘处理器
        /// </summary>
        /// <returns></returns>
        Task InitProcessorAsync();
    }
}
