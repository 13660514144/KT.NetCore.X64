﻿using System.Net;

namespace KT.Quanta.Service.Devices.Quanta.Models
{
    public class QuantaHandleElevatorSequenceModel
    {
        /// <summary>
        /// 消息id（序列号）
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 楼层名称（目的楼层）
        /// </summary>
        public string FloorName { get; set; }
    }
}