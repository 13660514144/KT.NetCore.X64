using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    /// <summary>
    /// 通行数据接收模型
    /// </summary>
    public class ElevatorPassModel
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 电梯服务名称
        /// </summary>
        public string ServerId { get; set; }
    }
}
