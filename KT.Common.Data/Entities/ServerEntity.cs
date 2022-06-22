using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Data.Entities
{
    /// <summary>
    /// 服务基础数据结构
    /// </summary>
    public class ServerEntity : BaseEntity
    {
        /// <summary>
        /// 服务器数据库地址
        /// </summary>
        public string DBAddr { get; set; }

        /// <summary>
        /// 服务器数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 服务器数据库用户名
        /// </summary>
        public string DBUser { get; set; }

        /// <summary>
        /// 服务器数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string PCAddr { get; set; }

        /// <summary>
        /// 服务器系统用户名
        /// </summary>
        public string PCUser { get; set; }

        /// <summary>
        /// 服务器系统密码
        /// </summary>
        public string PCPassword { get; set; }
    }
}
