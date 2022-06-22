using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Netty.Servers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Server
{
    /// <summary>
    /// SocketTcp服务
    /// </summary>
    public class QuantaServerHost : QuantaServerHostBase, IQuantaServerHost
    {
        private readonly IContainerProvider _containerProvider;

        public QuantaServerHost(ILogger<QuantaServerHost> logger,
            IContainerProvider containerProvider) : base(logger)
        {
            _containerProvider = containerProvider;
        }

        public override IChannelHandler GetChannelHandler()
        {
            return _containerProvider.Resolve<IQuantaServerHandler>();
        }

        public override QuantaServerFrameEncoder GetFrameEncoder()
        {
            return _containerProvider.Resolve<QuantaServerFrameEncoder>();
        }

        public override QuantaServerFrameDecoder GetFrameDecoder()
        {
            return _containerProvider.Resolve<QuantaServerFrameDecoder>();
        }
    }
}