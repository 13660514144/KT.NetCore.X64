using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Entities
{
    [Table("HANDLE_ELEVATOR_DEVICE_AUXILIARY")]
    public class HandleElevatorDeviceAuxiliaryEntity : BaseEntity
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
