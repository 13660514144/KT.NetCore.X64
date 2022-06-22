using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer
{
    /// <summary>
    /// NettyHttp服务器
    /// </summary>
    public interface INettyHttpHost
    {
        /// <summary>
        /// 初始化并运行
        /// </summary>
        /// <param name="port">端口</param> 
        Task RunAsync(int port);

        /// <summary>
        /// 关闭
        /// </summary>
        Task CloseAsync();
    }
}