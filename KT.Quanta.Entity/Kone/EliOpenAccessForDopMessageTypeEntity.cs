using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Entity.Kone
{
    [Table("ELI_OPEN_ACCESS_FOR_DOP_MESSAGE_TYPE")]
    public class EliOpenAccessForDopMessageTypeEntity : BaseEntity
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
        public int MessageType { get; set; }
    }
}
