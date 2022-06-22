using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using KT.Quanta.Service.Devices.Quanta.Models;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaDisplayDistributeDataService
    {
        /// <summary>
        /// 派梯成功
        /// </summary>
        /// <param name="paddle"></param>
        /// <returns></returns>
        Task HandleElevatorSuccess(ElevatorDisplayModel paddle);

        /// <summary>
        /// 派梯成功，无结果
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task HandleElevatorSuccess(string messageId);

        /// <summary>
        /// 派梯失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task HandleElevatorError(ElevatorDisplayModel elevatorDisplay, string message);
        Task HandleElevatorStatus(ElevatorDisplayModel paddle);
    }
}