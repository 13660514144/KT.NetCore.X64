using KT.Elevator.Unit.Dispatch.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hitachi.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHitachiCallReponseHandler
    { 
        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        Task PaddleAsync(UnitDispatchReceiveHandleElevatorModel subHeadResponse);
    }
}
