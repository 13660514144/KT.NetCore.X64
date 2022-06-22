using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients
{
    public abstract class MitsubishiElipClientHandlerBase : ChannelHandlerAdapter, IMitsubishiElipClientHandlerBase
    {
        private ILogger<MitsubishiElipClientHandlerBase> _logger;
        private IMitsubishiElipHandleElevatorSequenceList _mitsubishiElipHandleElevatorSequenceList;

        public MitsubishiElipClientHandlerBase(ILogger<MitsubishiElipClientHandlerBase> logger,
            IMitsubishiElipHandleElevatorSequenceList mitsubishiElipHandleElevatorSequenceList)
        {
            _logger = logger;
            _mitsubishiElipHandleElevatorSequenceList = mitsubishiElipHandleElevatorSequenceList;
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _logger.LogInformation($"EchoClientHandler ChannelActive!");
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var response = message as MitsubishiElipCommunicationHeader;
            if (response == null)
            {
                _logger.LogWarning($"派梯接收派梯结果数据为空！");
                return;
            }

            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} " +
                $"datas:{response.Assistant.Datas.ToCommaPrintString()} ");

            if (MitsubishiElipCommandEnum.Heartbeat.Code.Equals(response.Assistant.Command))
            {
                var data = new MitsubishiElipHeartbeatModel();
                data.ReadFromBytes(response.Assistant.Datas.ToArray());

                _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

                //处理数据 
                HeartbeatHandlerAsync(context, response, data);
            }
            else if (MitsubishiElipCommandEnum.HandleResult.Code.Equals(response.Assistant.Command))
            {
                var data = new MitsubishiElipVerificationAcceptanceModel();
                data.ReadFromBytes(response.Assistant.Datas.ToArray());

                _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

                //获取数据来源
                var sequence = _mitsubishiElipHandleElevatorSequenceList.Get(data.SequenceNumber);
                if (sequence == null)
                {
                    _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                    return;
                }

                //处理数据
                AcceptanceHandlerAsync(context, response, data, sequence);
            }
            else
            {
                _logger.LogError($"派梯接收派梯结果数据：找不到Command类型！");
                return;
            }
        }

        public abstract void AcceptanceHandlerAsync(IChannelHandlerContext context, MitsubishiElipCommunicationHeader response, MitsubishiElipVerificationAcceptanceModel data, MitsubishiElipHandleElevatorSequenceModel sequence);

        public abstract void HeartbeatHandlerAsync(IChannelHandlerContext context, MitsubishiElipCommunicationHeader response, MitsubishiElipHeartbeatModel data);

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

        protected MitsubishiElipCommunicationHeader Decode(DatagramPacket input)
        {
            //数据过长丢弃，防攻击
            if (MitsubishiElipHelper.DiscardMin < input.Content.ReadableBytes)
            {
                _logger.LogError($"MitsubishiElipFrameDecoder丢弃数据：{input.Content.ReadableBytes} ");
                input.Content.SkipBytes(input.Content.ReadableBytes);
                return null;
            }

            //判断数据是否完整,不完整等待数据接收
            var dataLength = input.Content.GetByte(0);
            var length = dataLength + 4;
            if (length > input.Content.ReadableBytes)
            {
                _logger.LogWarning($"MitsubishiElipFrameDecoder数据不完整：need:{length} readable:{input.Content.ReadableBytes} ");
                input.Content.SkipBytes(input.Content.ReadableBytes);
                return null;
            }

            //获取数据
            var bytes = new byte[length];
            input.Content.ReadBytes(bytes);

            _logger.LogInformation($"MitsubishiElipFrameDecoder接收数据：bytes:{bytes.ToCommaPrintString()} ");

            //转换成对象
            var result = new MitsubishiElipCommunicationHeader();
            result.ReadFromBytes(bytes);

            //输出
            return result;
        }
    }

}
