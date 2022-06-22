using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer
{
    public interface INettyServerHost
    {
        Task CloseAsync();
        Task RunAsync(int port);
    }
}