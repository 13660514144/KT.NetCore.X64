using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Common.WpfApp.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.NettyServer
{
    public class NettyServerHandler : ChannelHandlerAdapter
    {
        private ILogger _logger;
        public NettyServerHandler()
        {
            _logger = ContainerHelper.Resolve<ILogger>();
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as IByteBuffer;
            if (buffer != null)
            {
                var json = buffer.ToString(Encoding.UTF8);
                _logger.LogInformation($"接收消息：{json} ");
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            _logger.LogInformation("NettyServerHandler ChannelReadComplete!");
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _logger.LogError("NettyServerHandler ExceptionCaught Exception: " + exception);
            context.CloseAsync();
        }
    }
}