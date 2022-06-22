
using DotNetty.Transport.Channels;
using System.Threading.Tasks;

namespace KT.Common.Netty.Servers
{
    public interface IQuantaServerHostBase
    {
        IChannelHandler GetChannelHandler();
        QuantaServerFrameDecoder GetFrameDecoder();
        QuantaServerFrameEncoder GetFrameEncoder();
        void RunAsync(int port);
        Task Shutdown();
    }
}
