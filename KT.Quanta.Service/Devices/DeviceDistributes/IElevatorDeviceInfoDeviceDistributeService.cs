using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    public interface IElevatorDeviceInfoDeviceDistributeService
    {
        Task<List<ProcessorModel>> GetProcessorFloorsAsync();
        Task<List<ProcessorFloorModel>> GetInitOutputByProcessorIdAsync(string processorId);
    }
}
