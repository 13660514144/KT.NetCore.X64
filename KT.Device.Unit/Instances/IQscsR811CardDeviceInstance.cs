using KT.Quanta.Common.Enums;
using System.Threading.Tasks;

namespace KT.Device.Unit.Instances
{
    public interface IQscsR811CardDeviceInstance
    {
        BrandModelEnum BrandModel { get; }

        Task StartAsync();
    }
}