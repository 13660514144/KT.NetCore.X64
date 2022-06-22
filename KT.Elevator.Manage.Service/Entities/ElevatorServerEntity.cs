﻿using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Elevator.Manage.Service.Entities
{
    /// <summary>
    /// 电梯服务
    /// </summary>
    [Table("ELEVATOR_SERVER")]
    public class ElevatorServerEntity : BaseEntity
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务器账号
        /// </summary>
        public string PCAccount { get; set; }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 数据库账号
        /// </summary>
        public string DBAccount { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 是否为主服务器
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public ElevatorGroupEntity ElevatorGroup { get; set; }
    }
}