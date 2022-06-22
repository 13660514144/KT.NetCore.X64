using KT.Elevator.Unit.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperTools
{
    public class HitichSendComm
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 目地楼层
        /// </summary>
        public string Floor { get; set; }
        public string CommBox { get; set; }
    }
    /// <summary>
    /// 派梯发送队列
    /// </summary>
    public class QueueElevaor
    {
        public byte[] Instructions { get; set; }
        public int ScommFlg { get; set; }
        public long UtcTime { get; set; }
        public UnitHandleElevatorModel Handler { get; set; } = new UnitHandleElevatorModel();
        public string MessageKey { get; set; }
    }
    public class WaitOverQue
    {
        public string FloorName { get; set; }
        public long UtcTime { get; set; }
    }
    public class ListWaitKey
    { 
        public string Key { get; set; }
        public long UtcTime { get; set; }
        public string PortName { get; set; }
    }
}
