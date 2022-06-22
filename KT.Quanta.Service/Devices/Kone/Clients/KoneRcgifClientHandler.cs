using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Kone.Requests;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public class KoneRcgifClientHandler : ChannelHandlerAdapter, IKoneRcgifClientHandler
    {
        private readonly ILogger<KoneRcgifClientHandler> _logger;
        private IKoneRcgifReponseHandler _koneReponseHandler;

        public Action SetReceiveHeartbeatTimeHandler { get; set; }

        public KoneRcgifClientHandler(ILogger<KoneRcgifClientHandler> logger,
            IKoneRcgifReponseHandler koneReponseHandler)
        {
            _logger = logger;
            _koneReponseHandler = koneReponseHandler;
        }

        /// <summary>
        /// 连接服务器成功后执行
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelActive(IChannelHandlerContext context)
        {
            _logger.LogInformation($"KoneEchoClientHandler ChannelActive!");
        }

        /// <summary>
        /// 管道读取数据
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var headResponse = message as KoneRcgifHeaderResponse;
            if (headResponse != null)
            {
                HandleAsync(headResponse);
            }

            _logger.LogInformation($"KoneEchoClientHandler ChannelRead Message:{message} ");
        }

        private async void HandleAsync(KoneRcgifHeaderResponse headResponse)
        {
            try
            {
                await HandleResultAsync(headResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"通力数据接收处理异常：{ex} ");
            }
        }

        private async Task HandleResultAsync(KoneRcgifHeaderResponse headResponse)
        {
            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} datas:{headResponse.Datas.ToCommaPrintString()} ");

            var isLittleEndianess = headResponse.IsLittleEndianess();
            if (headResponse.MessageType == KoneRcgifMessageTypeEnum.MSG_HEARTBEAT.Code)
            {
                SetReceiveHeartbeatTimeHandler?.Invoke();
                await _koneReponseHandler.HeartBeatAsync(headResponse, headResponse.Datas);
            }
            else if (headResponse.MessageType == KoneRcgifMessageTypeEnum.MSG_DESTINATION_CALL.Code)
            {
                var subHeadResponse = new KoneRcgifSubHeader1Response();
                subHeadResponse.ReadFromBytes(headResponse.Datas, isLittleEndianess);

                if (subHeadResponse.ApplicationMsgType == KoneRcgifApplicationMsgTypeEnum.DISCONNECT.Code)
                {
                    await _koneReponseHandler.DisconnectAsync(headResponse, subHeadResponse.Datas);
                }
                else if (subHeadResponse.ApplicationMsgType == KoneRcgifApplicationMsgTypeEnum.YOUR_TRANSPORTATION.Code)
                {
                    await _koneReponseHandler.PaddleAsync(headResponse, subHeadResponse);
                }
                else
                {
                    _logger.LogError($"找不到接收的参数类型：ApplicationMsgType：{subHeadResponse.ApplicationMsgType} ");
                }
            }
            else
            {
                _logger.LogError($"找不到接收的参数类型：MessageType：{headResponse.MessageType} ");
            }
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            _logger.LogInformation($"KoneEchoClientHandler ChannelReadComplete!");
            context.Flush();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _logger.LogError($"KoneEchoClientHandler ExceptionCaught Exception:{exception} ");
            context.CloseAsync();
        }
    }
}
