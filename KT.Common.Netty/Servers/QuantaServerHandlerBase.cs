using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Common.Netty.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Servers
{
    public abstract class QuantaServerHandlerBase : ChannelHandlerAdapter, IQuantaServerHandlerBase
    {
        private readonly ILogger<QuantaServerHandlerBase> _logger;
        public QuantaServerHandlerBase(ILogger<QuantaServerHandlerBase> logger)
        {
            _logger = logger;
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var buffer = message as QuantaNettyHeader;
            if (buffer == null)
            {
                _logger.LogInformation($"{context.Channel.RemoteAddress} 接收数据异常！");
                return;
            }

            DataHandleAsync(context, buffer);
        }

        private async void DataHandleAsync(IChannelHandlerContext context, QuantaNettyHeader buffer)
        {
            //根据模块与命令号查找操作对象
            var function = await GetActionManager().GetAsync(buffer.Module, buffer.Command);
            if (function == null)
            {
                _logger.LogError($"{context.Channel.RemoteAddress} 找不到操作对象：module:{buffer.Module} command:{buffer.Command} ");
                return;
            }

            //执行业务操作
            await function.Invoke(context.Channel, buffer);
        }

        public abstract IQuantaNettyActionManager GetActionManager();

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}