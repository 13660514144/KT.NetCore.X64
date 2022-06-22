using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Netty.Servers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Servers
{
    /// <summary>
    /// SocketTcp服务
    /// </summary>
    public class QuantaServerHost : QuantaServerHostBase, IQuantaServerHost
    {
        private readonly IServiceProvider _serviceProvider;

        public QuantaServerHost(ILogger<QuantaServerHost> logger,
            IServiceProvider serviceProvider) : base(logger)
        {
            _serviceProvider = serviceProvider;
        }

        public override IChannelHandler GetChannelHandler()
        {
            return _serviceProvider.GetRequiredService<IQuantaServerHandler>();
        }

        public override QuantaServerFrameEncoder GetFrameEncoder()
        {
            return _serviceProvider.GetRequiredService<QuantaServerFrameEncoder>();
        }

        public override QuantaServerFrameDecoder GetFrameDecoder()
        {
            return _serviceProvider.GetRequiredService<QuantaServerFrameDecoder>();
        }
    }
}
