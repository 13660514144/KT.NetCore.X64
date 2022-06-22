using KT.Elevator.Manage.Service.Devices.Kone.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone
{
    public interface IKoneService
    {
        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="elevatorServerId"></param>
        /// <returns></returns>
        Task<HandleElevatorResponse> HandleElevator(HandleElevatorRequest request);
    }
}
