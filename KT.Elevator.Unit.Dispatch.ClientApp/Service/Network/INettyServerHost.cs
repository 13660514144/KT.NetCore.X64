using System.Threading.Tasks;

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Network
{
    public interface INettyServerHost
    {
        Task CloseAsync();
        Task RunAsync(int port);
    }
}