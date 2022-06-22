using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Dispatch.Entity.Models
{
    public class UnitDispatchSendHandleElevatorModel
    {
        /// <summary>
        /// 每次发送携带的主键id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardNumber { get; set; }

        /// <summary>
        /// 手动楼层
        /// </summary>
        public string AutoRealFloorId { get; set; }

        /// <summary>
        /// 自动楼层
        /// </summary>
        public List<ElevatorFloorModel> ManualRealFloorIds { get; set; }

        /// <summary>
        /// 手动楼层名称，用于显示
        /// </summary>
        public string DestinationFloorName { get; set; }

    }
}
