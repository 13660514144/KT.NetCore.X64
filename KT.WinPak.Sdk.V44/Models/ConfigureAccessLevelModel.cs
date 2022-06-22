using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Models
{
    /// <summary>
    /// 配置读卡器
    /// </summary>
    public class ConfigureAccessLevelModel
    {
        /// <summary>
        /// 门禁级别名称
        /// </summary>
        public string AccessLevelName { get; set; }

        /// <summary>
        /// 读卡器名称
        /// </summary>
        public List<string> DeviceName { get; set; }

        /// <summary>
        /// 时区名称
        /// </summary>
        public string TimeZoneName { get; set; }
    }
}
