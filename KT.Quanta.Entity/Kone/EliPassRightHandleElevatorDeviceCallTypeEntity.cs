using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Entities
{
    [Table("ELI_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE")]
    public class EliPassRightHandleElevatorDeviceCallTypeEntity : BaseEntity
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public string PassRightSign { get; set; }

        /// <summary>
        /// 派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 呼叫类型
        /// </summary>
        public int CallType { get; set; }
    }
}
