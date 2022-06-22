using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Common.Models
{
    public class SystemConfigSettings
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// 版权声明
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Logo图标路径
        /// </summary>
        public string LogoUrl { get; set; }
    }
}
