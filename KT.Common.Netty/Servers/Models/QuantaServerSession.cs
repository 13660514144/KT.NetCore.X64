using DotNetty.Transport.Channels;
using KT.Common.Netty.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Netty.Servers.Models
{
    public class QuantaServerSession
    {
        private IChannel _channel;

        public QuantaServerSession(IChannelHandlerContext context)
        {
            _channel = context.Channel;
        }
        public QuantaServerSession(IChannel channel)
        {
            _channel = channel;
        }

        public async Task SendAsync(QuantaNettyHeader header)
        {
            await _channel.WriteAndFlushAsync(header);
        }

        public async Task CloseAsync()
        {
            await _channel.CloseAsync();
        }

        public Task<bool> IsConnectAsync()
        {
            return Task.FromResult(_channel.Active);
        }
    }
}
