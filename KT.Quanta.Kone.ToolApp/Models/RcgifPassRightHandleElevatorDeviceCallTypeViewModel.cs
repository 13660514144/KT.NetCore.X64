using KT.Common.Data.Models;
using KT.Common.WpfApp.ViewModels;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Model.Kone;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KT.Quanta.Kone.ToolApp.Models
{
    public class RcgifPassRightHandleElevatorDeviceCallTypeViewModel : BindableBase
    {
        private string id;
        private string passRightSign;
        private string handleElevatorDeviceId;
        private AcsBypassCallTypeEnum callType = AcsBypassCallTypeEnum.NormalCall;

        /// <summary>
        /// Id主键
        /// </summary>
        public string Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }

        /// <summary>
        /// 权限Id
        /// </summary>
        public string PassRightSign
        {
            get => passRightSign;
            set
            {
                SetProperty(ref passRightSign, value);
            }
        }

        /// <summary>
        /// 派梯设备id
        /// </summary>
        public string HandleElevatorDeviceId
        {
            get => handleElevatorDeviceId;
            set
            {
                SetProperty(ref handleElevatorDeviceId, value);
            }
        }

        /// <summary>
        /// 呼叫类型
        /// </summary>
        public AcsBypassCallTypeEnum CallType
        {
            get => callType;
            set
            {
                SetProperty(ref callType, value);
            }
        }
    }
}
