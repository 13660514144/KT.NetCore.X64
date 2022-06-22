using KT.Common.Netty.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Unit.Model.Requests
{
    public class HandleElevatorDisplayRequest : QuantaSerializer
    {
        /// <summary>
        /// 派梯楼层名称
        /// </summary>
        public string DestinationFloorName { get; set; }

        /// <summary>
        /// 派梯电梯名称
        /// </summary>
        public string ElevatorName { get; set; }

        protected override void Read()
        {
            DestinationFloorName = ReadString();
            ElevatorName = ReadString();
        }

        protected override void Write()
        {
            WriteString(DestinationFloorName);
            WriteString(ElevatorName);
        }
    }
}
