using KT.Quanta.Common.Enums;
using System.Threading.Tasks;

namespace KT.Device.Unit.Instances
{
    public interface IQiacsQt660rCardDeviceInstance
    {
        BrandModelEnum BrandModel { get; }

        Task StartAsync();
    }
}