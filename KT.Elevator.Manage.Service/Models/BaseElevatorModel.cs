using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Models
{
    public class BaseElevatorModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 边缘中数据更新时间
        /// </summary>
        public long EditedTime { get; set; }
    }
}
