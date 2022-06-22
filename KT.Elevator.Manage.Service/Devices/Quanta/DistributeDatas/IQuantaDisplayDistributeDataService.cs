using KT.Elevator.Manage.Service.Devices.Kone.Models;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas
{
    public interface IQuantaDisplayDistributeDataService
    {
        Task HandleElevatorSuccess(MsgPaddle paddle);
    }
}