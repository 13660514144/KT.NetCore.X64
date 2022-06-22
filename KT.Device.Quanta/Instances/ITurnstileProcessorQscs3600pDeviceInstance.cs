using KT.Quanta.Service.Entities;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Instances
{
    public interface ITurnstileProcessorQscs3600pDeviceInstance
    {
        Task InitializeAsync();
        Task StartAsync();
    }
}