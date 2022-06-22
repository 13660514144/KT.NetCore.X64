using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Common.Netty.Clients;
using KT.Common.Netty.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Clients
{
    /// <summary>
    /// socket客户端
    /// </summary>
    public class QuantaClientHost : QuantaClientHostBase, IQuantaClientHost
    {
        private IServiceProvider _serviceProvider;

        public QuantaClientHost(ILogger<QuantaClientHostBase> logger,
            IServiceProvider serviceProvider)
            : base(logger, serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task<List<IChannelHandler>> GetFrameEncodersAsync()
        {
            var result = new List<IChannelHandler>()
            {
                _serviceProvider.GetRequiredService<QuantaClientFrameEncoder>()
            };

            return Task.FromResult(result);
        }

        public override Task<List<IChannelHandler>> GetFrameDecodersAsync()
        {
            var result = new List<IChannelHandler>()
            {
                _serviceProvider.GetRequiredService<QuantaClientFrameDecoder>()
            };

            return Task.FromResult(result);
        }

        public override Task<List<IChannelHandler>> GetClientHandlersAsync()
        {
            var result = new List<IChannelHandler>()
            {
                 _serviceProvider.GetRequiredService<IQuantaClientHandler>()
            };

            return Task.FromResult(result);
        }
    }
}
