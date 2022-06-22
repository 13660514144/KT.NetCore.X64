using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Model.Elevator.Dtos
{
    public class HandleElevatorDeviceAuxiliaryModel : BaseQuantaModel
    {
        public string HandleElevatorDeviceId { get; set; }

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
