using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Elevator.Dtos
{
    public class PassRightDestinationFloorModel : BaseQuantaModel
    {
        public string Sign { get; set; }

        public string ElevatorGroupId { get; set; }
         
        public string FloorId { get; set; }

        public FloorModel Floor { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsFront { get; set; }

        /// <summary>
        /// 是否开启前门
        /// </summary>
        public bool IsRear { get; set; }
    }
}
