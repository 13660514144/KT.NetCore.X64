using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers
{
    public class MitsubishiTowardElsgwServerHandler : ChannelHandlerAdapter, IMitsubishiTowardElsgwServerHandler
    {
        private ILogger<MitsubishiTowardElsgwServerHandler> _logger;
        private IMitsubishiElsgwFrameDecoder _mitsubishiElsgwFrameDecoder;

        public MitsubishiTowardElsgwServerHandler(ILogger<MitsubishiTowardElsgwServerHandler> logger,
            IMitsubishiElsgwFrameDecoder mitsubishiElsgwFrameDecoder)
        {
            _logger = logger;
            _mitsubishiElsgwFrameDecoder = mitsubishiElsgwFrameDecoder;
        }

        public IMitsubishiTowardElsgwRequestHandler MitsubishiTowardElsgwRequestHandler { get; set; }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _logger.LogInformation($"EchoClientHandler ChannelActive!");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var datagramPacket = message as DatagramPacket;
            if (datagramPacket == null)
            {
                _logger.LogWarning($"派梯接收派梯结果数据为空！");
                return;
            }
            var senderAddress = (IPEndPoint)datagramPacket.Sender;
            if (senderAddress.Port != 52000)
            {
                _logger.LogWarning($"数据来源端口异常：port:{senderAddress.Port} address:{datagramPacket.Sender.AddressFamily} ");
                return;
            }

            var response = _mitsubishiElsgwFrameDecoder.Decode(datagramPacket);
            if (response == null)
            {
                _logger.LogWarning($"派梯接收派梯结果数据转换为空！");
                return;
            }

            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} " +
                $"datas:{response.Assistant.Datas.ToCommaPrintString()} ");

            if (MitsubishiElsgwCommandEnum.Heartbeat.Code.Equals(response.Assistant.CommandNumber))
            {
                MitsubishiTowardElsgwRequestHandler.HeartbeatAsync(context, response, datagramPacket.Sender);
            }
            else if (MitsubishiElsgwCommandEnum.SingleFloorCall.Code.Equals(response.Assistant.CommandNumber))
            {
                MitsubishiTowardElsgwRequestHandler.SingleFloorCallAsync(response, datagramPacket.Sender);
            }
            else if (MitsubishiElsgwCommandEnum.MultiFloorCall.Code.Equals(response.Assistant.CommandNumber))
            {
                MitsubishiTowardElsgwRequestHandler.MultiFloorCallAsync(response, datagramPacket.Sender);
            }
            else
            {
                _logger.LogError($"派梯接收派梯结果数据：找不到Command类型！");
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
