using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Models
{
    public class ConfigureEntranceModel
    {
        /// <summary>
        /// 门禁级别名称
        /// </summary>
        public string AccessLevelName { get; set; }

        /// <summary>
        /// 读卡器名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 时区名称
        /// </summary>
        public string TimeZoneName { get; set; }

        /// <summary>
        /// 读卡器组
        /// </summary>
        public string GroupName { get; set; }
    }
}
