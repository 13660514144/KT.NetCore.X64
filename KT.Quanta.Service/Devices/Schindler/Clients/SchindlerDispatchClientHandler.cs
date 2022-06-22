using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Quanta.Service.Devices.Schindler.Helpers;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    public class SchindlerDispatchClientHandler : SchindlerClientHandlerBase, ISchindlerDispatchClientHandler
    {
        private ILogger<SchindlerDispatchClientHandler> _logger;
        private ISchindlerReponseHandler _schindlerReponseHandler;

        public SchindlerDispatchClientHandler(ILogger<SchindlerDispatchClientHandler> logger,
            ISchindlerReponseHandler schindlerReponseHandler)
            : base(logger)
        {
            _schindlerReponseHandler = schindlerReponseHandler;
            _logger = logger;
        }

        public override async Task HandleResult(IByteBuffer byteBuffer)
        {
            var headResponse = new SchindlerTelegramHeaderResponse();
            headResponse.ReadFromBuffer(byteBuffer);

            if (headResponse.FunctionType == SchindlerDispatchMessageTypeEnum.HEARTBEAT_RESPONSE.Code)
            {
                await _schindlerReponseHandler.HeartbeatAsync(headResponse.Datas);
            }
            else if (headResponse.FunctionType == SchindlerDispatchMessageTypeEnum.DIRECT_CALL_RESPONSE.Code)
            {
                await _schindlerReponseHandler.PaddleAsync(headResponse);
            }
            else
            {
                _logger.LogError($"迅达电梯派梯回调找不到类型：{headResponse.FunctionType} ");
            }
        }
    }
}