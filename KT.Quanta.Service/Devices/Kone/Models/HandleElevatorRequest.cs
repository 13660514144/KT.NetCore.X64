using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Kone.Models
{
    /// <summary>
    /// 派梯结果
    /// </summary>
    public class HandleElevatorRequest
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 派梯设备Id
        /// </summary>
        public string HandleElevatorId { get; set; }

    }
}
