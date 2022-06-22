using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Quanta.Service.Devices.Hikvision.Models
{
    public class HikvisionSdkExecuteQueueModel
    {
        public string DistributeType { get; set; }

        /// <summary>
        /// 后台系统人员id,用于出错数据上传
        /// </summary>
        public string PersonId { get; set; }

        public object Data { get; set; }
    }
}
