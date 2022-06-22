using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Schindler.Helpers;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    public abstract class SchindlerClientHandlerBase : ChannelHandlerAdapter, ISchindlerClientHandlerBase
    {
        private readonly ILogger<SchindlerClientHandlerBase> _logger;

        public CommunicateDeviceInfoModel CommunicateDeviceInfo { get; set; }

        public SchindlerClientHandlerBase(ILogger<SchindlerClientHandlerBase> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 连接服务器成功后执行
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelActive(IChannelHandlerContext context)
        {
            _logger.LogInformation($"SchindlerEchoClientHandler ChannelActive!");
        }

        /// <summary>
        /// 管道读取数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as IByteBuffer;
            if (byteBuffer != null)
            {
                HandleAsync(byteBuffer);
            }

            _logger.LogInformation($"SchindlerEchoClientHandler ChannelRead Message:{message} ");
        }

        private async void HandleAsync(IByteBuffer byteBuffer)
        {
            try
            {
                await HandleResult(byteBuffer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"迅达数据接收处理异常：{ex} ");
            }
        }

        public abstract Task HandleResult(IByteBuffer byteBuffer);

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            _logger.LogInformation($"SchindlerEchoClientHandler ChannelReadComplete!");
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _logger.LogError($"SchindlerEchoClientHandler ExceptionCaught Exception:{exception} ");
            context.CloseAsync();
        }
    }
}