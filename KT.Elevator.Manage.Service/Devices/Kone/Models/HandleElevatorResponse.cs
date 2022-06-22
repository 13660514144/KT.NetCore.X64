using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone.Models
{
    /// <summary>
    /// 派梯结果
    /// </summary>
    public class HandleElevatorResponse
    {
        /// <summary>
        /// 终端Id
        /// </summary>
        public string TerminalId { get; set; }

        /// <summary>
        /// 电梯Id
        /// </summary>
        public string ElevatorId { get; set; }

    }
}
