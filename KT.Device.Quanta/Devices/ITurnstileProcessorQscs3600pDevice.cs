using KT.Quanta.Service.Entities;
using System;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Devices
{
    public interface ITurnstileProcessorQscs3600pDevice
    {
        Task CreateAsync(ProcessorEntity entity);
        Task UpdateAsync(ProcessorEntity entity);
    }
}