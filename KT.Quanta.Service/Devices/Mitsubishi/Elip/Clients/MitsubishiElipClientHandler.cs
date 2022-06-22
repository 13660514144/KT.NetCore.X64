using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Handlers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients
{
    public class MitsubishiElipClientHandler : MitsubishiElipClientHandlerBase, IMitsubishiElipClientHandler
    {
        private ILogger<MitsubishiElipClientHandler> _logger;
        private IMitsubishiElipReponseHandler _mitsubishiReponseHandler;
        private IMitsubishiTowardElipResponseHandler _mitsubishiTowardElipResponseHandler;

        public MitsubishiElipClientHandler(ILogger<MitsubishiElipClientHandler> logger,
            IMitsubishiElipReponseHandler mitsubishiReponseHandler,
            IMitsubishiElipHandleElevatorSequenceList mitsubishiElipHandleElevatorSequenceList,
            IMitsubishiTowardElipResponseHandler mitsubishiTowardElipResponseHandler)
            : base(logger, mitsubishiElipHandleElevatorSequenceList)
        {
            _logger = logger;
            _mitsubishiReponseHandler = mitsubishiReponseHandler;
            _mitsubishiTowardElipResponseHandler = mitsubishiTowardElipResponseHandler;
        }
        public override void AcceptanceHandlerAsync(IChannelHandlerContext context,
            MitsubishiElipCommunicationHeader response,
            MitsubishiElipVerificationAcceptanceModel data,
            MitsubishiElipHandleElevatorSequenceModel sequence)
        {
            if (sequence.HandleElevatorType == HandleElevatorTypeEnum.MitsubishiElip.Value)
            {
                _mitsubishiReponseHandler.AcceptanceAsync(data, sequence);
            }
            else if (sequence.HandleElevatorType == HandleElevatorTypeEnum.MitsubishiToward.Value)
            {
                _mitsubishiTowardElipResponseHandler.AcceptanceAsync(context, data, sequence);
            }
            else
            {
                _logger.LogError($"派梯接收派梯结果数据：找不到派梯类型类型！");
                return;
            }
        }

        public override void HeartbeatHandlerAsync(IChannelHandlerContext context,
            MitsubishiElipCommunicationHeader response,
            MitsubishiElipHeartbeatModel data)
        {
            _mitsubishiReponseHandler.HeartbeatAsync(response);
        }
    }
}