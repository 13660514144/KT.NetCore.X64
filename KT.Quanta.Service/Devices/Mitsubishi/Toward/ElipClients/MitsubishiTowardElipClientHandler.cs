using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Handlers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace KT.Quanta.Service.Devices.Mitsubishi.Toward.ElipClients
{
    public class MitsubishiTowardElipClientHandler : MitsubishiElipClientHandlerBase, IMitsubishiTowardElipClientHandler
    {
        private ILogger<MitsubishiElipClientHandler> _logger;
        private IMitsubishiTowardElipResponseHandler _mitsubishiTowardElipResponseHandler;

        public MitsubishiTowardElipClientHandler(ILogger<MitsubishiElipClientHandler> logger,
            IMitsubishiElipHandleElevatorSequenceList mitsubishiElipHandleElevatorSequenceList,
           IMitsubishiTowardElipResponseHandler mitsubishiTowardElipResponseHandler)
            : base(logger, mitsubishiElipHandleElevatorSequenceList)
        {
            _logger = logger;
            _mitsubishiTowardElipResponseHandler = mitsubishiTowardElipResponseHandler;
        }

        public override void AcceptanceHandlerAsync(IChannelHandlerContext context, MitsubishiElipCommunicationHeader response,
            MitsubishiElipVerificationAcceptanceModel data,
            MitsubishiElipHandleElevatorSequenceModel sequence)
        {
            _mitsubishiTowardElipResponseHandler.AcceptanceAsync(context, data, sequence);
        }

        public override void HeartbeatHandlerAsync(IChannelHandlerContext context,
            MitsubishiElipCommunicationHeader response,
            MitsubishiElipHeartbeatModel data)
        {
            _mitsubishiTowardElipResponseHandler.HeartbeatAsync(response);
        }
    }
}