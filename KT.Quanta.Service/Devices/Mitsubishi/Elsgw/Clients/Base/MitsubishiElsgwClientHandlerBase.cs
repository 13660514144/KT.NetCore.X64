using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Handlers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Clients
{
    public abstract class MitsubishiElsgwClientHandlerBase : ChannelHandlerAdapter, IMitsubishiElsgwClientHandlerBase
    {
        private ILogger<MitsubishiElsgwClientHandlerBase> _logger;
        private IMitsubishiElsgwReponseHandler _mitsubishiReponseHandler;
        private IMitsubishiElsgwFrameDecoder _mitsubishiElsgwFrameDecoder;

        public MitsubishiElsgwClientHandlerBase(ILogger<MitsubishiElsgwClientHandlerBase> logger,
            IMitsubishiElsgwReponseHandler mitsubishiReponseHandler,
             IMitsubishiElsgwFrameDecoder mitsubishiElsgwFrameDecoder)
        {
            _logger = logger;
            _mitsubishiReponseHandler = mitsubishiReponseHandler;
            _mitsubishiElsgwFrameDecoder = mitsubishiElsgwFrameDecoder;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _logger.LogInformation($"EchoClientHandler ChannelActive!");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var byteBuffer = message as DatagramPacket;
            if (byteBuffer == null)
            {
                _logger.LogWarning($"派梯接收派梯结果数据为空！");
                return;
            }

            var response = _mitsubishiElsgwFrameDecoder.Decode(byteBuffer);
            if (response == null)
            {
                _logger.LogWarning($"派梯接收派梯结果数据转换为空！");
                return;
            }

            //处理数据
            DataHandlerAsync(response);
        }

        public abstract void DataHandlerAsync(MitsubishiElsgwTransmissionHeader response);

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