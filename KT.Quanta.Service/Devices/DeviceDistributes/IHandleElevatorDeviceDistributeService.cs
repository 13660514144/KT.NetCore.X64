using KT.Quanta.Service.Devices.Common;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public interface IHandleElevatorDeviceDistributeService
    {
        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="elevatorGroupId">电梯组id</param>
        /// <param name="sourceFloor">来源楼层</param>
        /// <param name="destinationFloor">目标楼层</param> 
        Task HandleAsync(string elevatorGroupId, DistributeHandleElevatorModel distributeHandle);

        /// <summary>
        /// 多楼层派梯
        /// </summary>
        /// <param name="elevatorGroupId">电梯组id</param>
        /// <param name="sourceFloor">来源楼层</param>
        /// <param name="destinationFloor">目标楼层</param> 
        Task MultiFloorHandleAsync(string elevatorGroupId, DistributeMultiFloorHandleElevatorModel distributeHandle);
    }
}