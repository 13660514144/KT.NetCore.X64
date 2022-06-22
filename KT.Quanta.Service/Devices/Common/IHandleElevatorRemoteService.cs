using KT.Quanta.Service.Devices.DeviceDistributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 派梯服务
    /// </summary>
    public interface IHandleElevatorRemoteService
    {
        /// <summary>
        /// 直接派梯
        /// </summary>
        /// <param name="elevatorGroupRemoteDevice">电梯组</param>
        /// <param name="distributeHandle">派梯参数</param> 
        Task DirectCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle);

        /// <summary>
        /// 多楼层派梯
        /// </summary>
        /// <param name="elevatorGroupRemoteDevice">电梯组</param>
        /// <param name="distributeHandle">派梯参数</param> 
        Task MultiFloorCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeMultiFloorHandleElevatorModel distributeHandle);

        /// <summary>
        /// 权限派梯
        /// </summary>
        /// <param name="elevatorGroupRemoteDevice">电梯组</param>
        /// <param name="distributeHandle">派梯参数</param> 
        Task RightCallAsync(IRemoteDevice elevatorGroupRemoteDevice, DistributeHandleElevatorModel distributeHandle);
    }
}
