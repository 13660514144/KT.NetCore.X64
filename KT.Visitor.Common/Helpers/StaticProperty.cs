using KT.Proxy.BackendApi.Helpers;
using KT.Visitor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Helpers
{
    /// <summary>
    /// 静态字段，用于界面绑定
    /// </summary>
    public static class StaticProperty
    {
        public static string SystemName { get; set; }

        public static string LoginUserName { get; set; }
           
        public static void SetValue(SystemConfigModel systemConfig)
        {
            if (string.IsNullOrEmpty(systemConfig.SystemName))
            {
                SystemName = "前台访客登记系统";
            }
            else
            {
                SystemName = systemConfig.SystemName;
            }
            StaticInfo.ServerAddress = systemConfig.ServerAddress;
        }
    }
}
