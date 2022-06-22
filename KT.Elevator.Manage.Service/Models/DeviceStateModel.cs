﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public class DeviceStateModel
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }
    }
}