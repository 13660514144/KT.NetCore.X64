using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Common.Netty.Common;
using Microsoft.Extensions.Logging;
using System;

namespace KT.Common.Netty.Clients
{
    public class QuantaClientHandlerBase : ChannelHandlerAdapter, IQuantaClientHandlerBase
    {
        private readonly ILogger _logger;
        public QuantaClientHandlerBase(ILogger<QuantaClientHandlerBase> logger)
        {
            _logger = logger;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _logger.LogInformation($"EchoClientHandler ChannelActive!");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as QuantaNettyHeader;
            if (byteBuffer != null)
            {

            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            _logger.LogInformation($"EchoClientHandler ChannelReadComplete!");
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _logger.LogError($"EchoClientHandler ExceptionCaught Exception:{exception} ");
            context.CloseAsync();
        }
    }
}