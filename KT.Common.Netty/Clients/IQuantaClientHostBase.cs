using DotNetty.Transport.Channels;
using KT.Common.Netty.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Common.Netty.Clients
{
    public interface IQuantaClientHostBase
    {
        Task CloseAsync();
        Task ConnectAsync();
        Task InitAsync(string remoteIp, int remotePort);
        Task<List<IChannelHandler>> GetFrameEncodersAsync();
        Task<List<IChannelHandler>> GetFrameDecodersAsync();
        Task<List<IChannelHandler>> GetClientHandlersAsync();
        Task SendAsync(QuantaNettyHeader header);
    }
}