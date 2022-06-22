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
    public class MitsubishiElsgwClientHandler : MitsubishiElsgwClientHandlerBase, IMitsubishiElsgwClientHandler
    {
        private ILogger<MitsubishiElsgwClientHandler> _logger;
        private IMitsubishiElsgwReponseHandler _mitsubishiReponseHandler;

        public MitsubishiElsgwClientHandler(ILogger<MitsubishiElsgwClientHandler> logger,
            IMitsubishiElsgwReponseHandler mitsubishiReponseHandler,
            IMitsubishiElsgwFrameDecoder mitsubishiElsgwFrameDecoder)
            : base(logger, mitsubishiReponseHandler, mitsubishiElsgwFrameDecoder)
        {
            _logger = logger;
            _mitsubishiReponseHandler = mitsubishiReponseHandler;
        }

        public override void DataHandlerAsync(MitsubishiElsgwTransmissionHeader response)
        {
            if (MitsubishiElsgwCommandEnum.Heartbeat.Code.Equals(response.Assistant.CommandNumber))
            {
                _mitsubishiReponseHandler.HeartbeatAsync(response);
            }
            else if (MitsubishiElsgwCommandEnum.HandleResult.Code.Equals(response.Assistant.CommandNumber))
            {
                _mitsubishiReponseHandler.SingleFloorCallAsync(response);
            }
            else
            {
                _logger.LogError($"派梯接收派梯结果数据：找不到Command类型！");
            }
        }
    }

}