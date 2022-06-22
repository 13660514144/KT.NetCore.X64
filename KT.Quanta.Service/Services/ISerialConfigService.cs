using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    /// <summary>
    /// 串口配置数据存储服务
    /// </summary>
    public interface ISerialConfigService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 根据Id获取串口配置信息
        /// </summary>
        /// <param name="id">串口配置Id</param>
        /// <returns>串口配置信息详情</returns>
        Task<SerialConfigModel> GetByIdAsync(string id);

        /// <summary>
        /// 查询所有串口配置信息
        /// </summary>
        /// <returns>串口配置信息列表</returns>
        Task<List<SerialConfigModel>> GetAllAsync();

        /// <summary>
        /// 修改串口配置
        /// </summary>
        /// <param name="model">串口配置详情</param>
        /// <returns>是否成功</returns>
        Task<SerialConfigModel> AddOrEditAsync(SerialConfigModel model);

        /// <summary>
        /// 物理删除串口配置
        /// </summary>
        /// <param name="id">串口配置Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);
    }
}
