using KT.WinPak.SDK.V48.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.IServices
{
    public interface IAccessLevelSdkService
    {
        /// <summary>
        /// 获取访问级别
        /// </summary>
        /// <returns></returns>
        List<AccessLevelModel> GetAll();

        /// <summary>
        /// 添加门禁级别
        /// </summary>
        /// <param name="model">The new access level details. </param>
        /// <returns></returns>
        AccessLevelModel Add(AccessLevelModel model);

        /// <summary>
        /// 修改门禁级别
        /// </summary>
        /// <param name="bstrAccl">The name of the access level to be edited.</param>
        /// <param name="model">The new access level details. </param>
        /// <returns></returns>
        bool Edit(string bstrAccl, AccessLevelModel model);

        /// <summary>
        /// 删除门禁级别
        /// </summary>
        /// <param name="bstrAcclName">The name of the access level to be deleted. </param>
        /// <returns></returns>
        bool Delete(string bstrAcclName, string bstrAcctName);

        /// <summary>
        /// 根据名称获取门禁级别
        /// </summary>
        /// <param name="bstrAcclName">门禁级别名称</param>
        /// <returns>门禁级别详细信息</returns>
        AccessLevelModel GetByName(string bstrAcclName,string bstrAcctName);

        /// <summary>
        /// 配置门禁与读卡器关系映射
        /// </summary>
        /// <param name="model">门禁与读卡器映射关系</param>
        /// <returns>是否操作成功</returns>
        bool Configure(ConfigureAccessLevelModel model);

        /// <summary>
        /// 配置门禁与读卡器关系映射,包含读卡器组
        /// </summary>
        /// <param name="model">门禁与读卡器映射关系</param>
        /// <returns>是否操作成功</returns>
        bool ConfigureEntranceAccess(ConfigureEntranceModel model);
    }
}
