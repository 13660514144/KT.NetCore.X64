using DotNetty.Buffers;
using KT.Quanta.Service.Devices.Schindler.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler.Clients
{
    public class SchindlerDatabaseClientHandler : SchindlerClientHandlerBase, ISchindlerDatabaseClientHandler
    {
        private ILogger<SchindlerDatabaseClientHandler> _logger;
        private ISchindlerReponseHandler _schindlerReponseHandler;

        public SchindlerDatabaseClientHandler(ILogger<SchindlerDatabaseClientHandler> logger,
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

            if (headResponse.FunctionType == SchindlerDatabaseMessageTypeEnum.HEARTBEAT_RESPONSE.Code)
            {
                await _schindlerReponseHandler.HeartbeatAsync(headResponse.Datas);
            }
            else if (headResponse.FunctionType == SchindlerDatabaseMessageTypeEnum.CHANGE_INSERT_PERSON_RESPONSE.Code)
            {
                await _schindlerReponseHandler.ChangeInsertPersonAsync(headResponse);
            }
            else if (headResponse.FunctionType == SchindlerDatabaseMessageTypeEnum.CHANGE_INSERT_PERSON_RESPONSE_NACK.Code)
            {
                await _schindlerReponseHandler.ChangeInsertPersonNackAsync(headResponse);
            }
            else if (headResponse.FunctionType == SchindlerDatabaseMessageTypeEnum.DELETE_PERSON_RESPONSE.Code)
            {
                await _schindlerReponseHandler.DeletePersonAsync(headResponse);
            }
            else if (headResponse.FunctionType == SchindlerDatabaseMessageTypeEnum.DELETE_PERSON_RESPONSE_NACK.Code)
            {
                await _schindlerReponseHandler.DeletePersonNackAsync(headResponse);
            }
            else if (headResponse.FunctionType == SchindlerDatabaseMessageTypeEnum.SET_ZONE_ACCESS_RESPONSE.Code)
            {
                await _schindlerReponseHandler.SetZoneAccessResponseAsync(headResponse);
            }
            else
            {
                _logger.LogError($"迅达电梯下发数据回调找不到类型：{headResponse.FunctionType} ");
            }
        }
    }
}