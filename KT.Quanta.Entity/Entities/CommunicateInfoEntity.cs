using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KT.Quanta.Service.Entities
{
    public class CommunicateInfoEntity : BaseEntity
    { 
        /// <summary>
        /// Ip地址
        /// </summary>
        [Required]
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        [Required]
        public int Port { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
