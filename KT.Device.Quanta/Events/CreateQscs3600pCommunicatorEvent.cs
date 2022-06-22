using KT.Common.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Device.Quanta.Events
{
    public class CreateQscs3600pCommunicatorEvent : PubSubEvent<CareateQscs3600pCommunicatorModel>
    {
    }

    public class CareateQscs3600pCommunicatorModel
    {
        /// <summary>
        /// 远程ip
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort { get; set; }
    }
}
