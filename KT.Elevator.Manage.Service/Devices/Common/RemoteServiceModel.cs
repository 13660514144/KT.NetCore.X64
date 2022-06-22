using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Common
{
    /// <summary>
    /// 所有远程服务类
    /// </summary>
    public class RemoteServiceModel
    {
        /// <summary>
        /// 电梯服务
        /// </summary>
        public IElevatorRemoteService ElevatorRemoteService { get; set; }

        /// <summary>
        /// 闸机服务
        /// </summary>
        public ITurnstileRemoteService TurnstileRemoteService { get; set; }

        /// <summary>
        /// 派梯
        /// </summary>
        public IHandleElevatorRemoteService HandleElevatorService { get; set; }

        /// <summary>
        /// 展示
        /// </summary>
        public IDisplayRemoteService DisplayRemoteService { get; set; }
    }
}
