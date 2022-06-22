using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    /// <summary>
    /// 系统配置数据存储服务
    /// </summary>
    public interface ISystemConfigService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 根据Id获取系统配置信息
        /// </summary>
        /// <param name="id">系统配置Id</param>
        /// <returns>系统配置信息详情</returns>
        Task<SystemConfigModel> GetByIdAsync(string id);

        /// <summary>
        /// 根据Key获取系统配置信息
        /// </summary>
        /// <param name="key">系统配置Key</param>
        /// <returns>系统配置信息详情</returns>
        Task<SystemConfigModel> GetByKeyAsync(string key);

        /// <summary>
        /// 根据Key获取系统配置信息,如果不存在，则创建
        /// </summary>
        /// <param name="key">系统配置Key</param>
        /// <returns>系统配置信息详情</returns>
        Task<SystemConfigModel> GetByKeyWithNewAsync(string key, string val);

        /// <summary>
        /// 查询所有系统配置信息
        /// </summary>
        /// <returns>系统配置信息列表</returns>
        Task<List<SystemConfigModel>> GetAllAsync();

        /// <summary>
        /// 新增系统配置
        /// </summary>
        /// <param name="model">系统配置详情</param>
        /// <returns>是否成功</returns>
        Task<SystemConfigModel> AddAsync(SystemConfigModel model);

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="model">系统配置详情</param>
        /// <returns>是否成功</returns>
        Task<SystemConfigModel> EditAsync(SystemConfigModel model);

        /// <summary>
        /// 物理删除系统配置
        /// </summary>
        /// <param name="id">系统配置Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 根据Key新增或修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<SystemConfigModel> AddOrEditByKeyAsync(string key, string value);

        /// <summary>
        /// 根据Key新增或修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<string> AddOrEditByKeyAsync(SystemConfigEnum key, string value);
    }
}
