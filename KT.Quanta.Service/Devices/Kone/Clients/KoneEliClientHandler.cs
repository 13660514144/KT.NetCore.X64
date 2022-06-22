using DotNetty.Transport.Channels;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Requests;
using KT.Quanta.Service.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone.Clients
{
    public class KoneEliClientHandler : ChannelHandlerAdapter, IKoneEliClientHandler
    {
        private readonly ILogger<KoneEliClientHandler> _logger;
        private IKoneEliReponseHandler _koneReponseHandler;
        private IKoneEliHandleElevatorSequenceList _koneHandleElevatorSequenceList;
        private FloorHandleElevatorResponseList _floorHandleElevatorResponseList;

        public Action SetReceiveHeartbeatTimeHandler { get; set; }

        public KoneEliClientHandler(ILogger<KoneEliClientHandler> logger,
            IKoneEliReponseHandler koneReponseHandler,
            IKoneEliHandleElevatorSequenceList koneHandleElevatorSequenceList,
            FloorHandleElevatorResponseList floorHandleElevatorResponseList)
        {
            _logger = logger;
            _koneReponseHandler = koneReponseHandler;
            _koneHandleElevatorSequenceList = koneHandleElevatorSequenceList;
            _floorHandleElevatorResponseList = floorHandleElevatorResponseList;
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
            var headResponse = message as KoneEliHeaderResponse;
            if (headResponse != null)
            {
                HandleAsync(headResponse, context);
            }

            _logger.LogInformation($"KoneEchoClientHandler ChannelRead Message:{message} ");
        }

        private async void HandleAsync(KoneEliHeaderResponse headResponse, IChannelHandlerContext context)
        {
            try
            {
                await HandleResultAsync(headResponse, context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"通力数据接收处理异常：{ex} ");
            }
        }

        private async Task HandleResultAsync(KoneEliHeaderResponse headResponse, IChannelHandlerContext context)
        {
            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(headResponse, JsonUtil.JsonPrintSettings)} datas:{headResponse.Datas.ToCommaPrintString()} ");

            var isLittleEndianess = headResponse.IsLittleEndianess();
            if (headResponse.MessageType == KoneEliMessageTypeEnum.MSG_HEARTBEAT.Code)
            {
                SetReceiveHeartbeatTimeHandler?.Invoke();
                await _koneReponseHandler.HeartBeatAsync(headResponse, headResponse.Datas);
            }
            else if (headResponse.MessageType == KoneEliMessageTypeEnum.MSG_ACCESS_CONTROL.Code)
            {
                var subHeadResponse = new KoneEliSubHeader1Response();
                subHeadResponse.ReadFromBytes(headResponse.Datas, isLittleEndianess);

                if (subHeadResponse.ApplicationMsgType == KoneEliApplicationMsgTypeEnum.DISCONNECT.Code)
                {
                    await _koneReponseHandler.DisconnectAsync(headResponse, subHeadResponse.Datas);
                }
                else if (subHeadResponse.ApplicationMsgType == KoneEliApplicationMsgTypeEnum.STATUS_RESPONSE.Code)
                {
                    await _koneReponseHandler.StatusAsync(headResponse, subHeadResponse, context);
                }
                else if (subHeadResponse.ApplicationMsgType == KoneEliApplicationMsgTypeEnum.DOP_MESSAGE.Code)
                {
                    await _koneReponseHandler.PaddleAsync(headResponse, subHeadResponse);
                }
                else if (subHeadResponse.ApplicationMsgType == KoneEliApplicationMsgTypeEnum.DOP_CLOSED.Code)
                {
                    await _koneReponseHandler.DopClosedAsync(headResponse, subHeadResponse);
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
