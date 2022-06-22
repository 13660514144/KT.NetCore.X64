using System;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Communicators
{
    public interface IQscs3600pCommunicator : IAsyncDisposable
    {
        Task CareateAsync(string remoteIp, int remotePort);
        Task UpdateAsync(string remoteIp, int remotePort);
    }
}