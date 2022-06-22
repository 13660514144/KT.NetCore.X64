using KT.TestTool.TestApp.Entities;
using System.Collections.Generic;

namespace KT.TestTool.TestApp.IDaos
{
    /// <summary>
    /// 系统配置数据持久化
    /// </summary>
    public interface ISystemConfigDataDao
    {
        /// <summary>
        /// 根据Id获取系统配置信息
        /// </summary>
        /// <param name="id">系统配置Id</param>
        /// <returns>系统配置信息详情</returns>
        SystemConfigEntity GetById(string id);

        /// <summary>
        /// 根据Id获取系统配置信息
        /// </summary>
        /// <param name="key">系统配置key</param>
        /// <returns>系统配置信息详情</returns>
        SystemConfigEntity GetByKey(string key);

        /// <summary>
        /// 查询所有系统配置信息
        /// </summary>
        /// <returns>系统配置信息列表</returns>
        List<SystemConfigEntity> GetAll();

        /// <summary>
        /// 新增系统配置
        /// </summary>
        /// <param name="entity">系统配置详情</param>
        /// <returns>是否成功</returns>
        SystemConfigEntity Add(SystemConfigEntity entity);

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="entity">系统配置详情</param>
        /// <returns>是否成功</returns>
        SystemConfigEntity Edit(SystemConfigEntity entity);

        /// <summary>
        /// 物理删除系统配置
        /// </summary>
        /// <param name="id">系统配置Id</param>
        /// <returns>是否成功</returns>
        bool Delete(string id);
    }
}
