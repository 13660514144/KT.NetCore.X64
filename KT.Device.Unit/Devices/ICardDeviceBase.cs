using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace KT.Device.Unit.Devices
{
    public interface ICardDeviceBase : IAsyncDisposable
    {
        object CardDeviceInfo { get; }
        SerialPort SerialPort { get; }

        Task StartAsync(object obj);
    }
}