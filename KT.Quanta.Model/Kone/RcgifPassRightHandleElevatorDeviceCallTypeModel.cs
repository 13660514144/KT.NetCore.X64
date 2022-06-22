using KT.Common.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Model.Kone
{
    public class RcgifPassRightHandleElevatorDeviceCallTypeModel
    {
        /// <summary>
        /// Id主键
        /// </summary>
        public string Id { get; set; }

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
        public int CallType { get; set; } = AcsBypassCallTypeEnum.NormalCall.Code;
    }
}
