using KT.Quanta.Common.Enums;
using System.Threading.Tasks;

namespace KT.Device.Unit.Instances
{
    public interface IQscsBx5nCardDeviceInstance
    {
        BrandModelEnum BrandModel { get; }

        Task StartAsync();
    }
}