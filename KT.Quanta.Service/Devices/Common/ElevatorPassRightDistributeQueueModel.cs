using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Common
{
    public class ElevatorPassRightDistributeQueueModel
    {
        public string DistributeType { get; set; }
        public PassRightModel PassRight { get; set; }

        /// <summary>
        /// 旧权限，用于迅达、海康删除旧数据
        /// </summary>
        public PassRightModel OldPassRight { get; set; }
        public FaceInfoModel Face { get; set; }
    }
}
