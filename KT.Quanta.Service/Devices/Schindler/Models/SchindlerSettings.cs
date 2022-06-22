using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    /// <summary>
    /// 配置
    /// </summary>
    public class SchindlerSettings
    {
        /// <summary>
        /// 用户模板名称
        /// </summary>
        public string PersonProfileTemplate { get; set; }

        /// <summary>
        /// 所有楼层用户模板
        /// </summary>
        public string AllFloorProfileName { get; set; }

        /// <summary>
        /// 响应时间（秒）
        /// </summary>
        public decimal ResponseSecondTime { get; set; } = 1.2M;
    }
}
