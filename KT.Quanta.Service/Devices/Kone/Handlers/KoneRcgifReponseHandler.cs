using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Requests;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Quanta.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public class KoneRcgifReponseHandler : IKoneRcgifReponseHandler
    {
        private ILogger<KoneRcgifReponseHandler> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private readonly IKoneRcgifHandleElevatorSequenceList _koneRcgifHandleElevatorSequenceList;

        public KoneRcgifReponseHandler(ILogger<KoneRcgifReponseHandler> logger,
            IServiceScopeFactory serviceScopeFactory,
            IKoneRcgifHandleElevatorSequenceList koneRcgifHandleElevatorSequenceList)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _koneRcgifHandleElevatorSequenceList = koneRcgifHandleElevatorSequenceList;
        }

        /// <summary>
        /// 心跳 8000000011
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Task HeartBeatAsync(Requests.KoneRcgifHeaderResponse headResponse, byte[] buffer)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneRcgifHeartbeatMessageResponse();
            data.ReadFromBytes(buffer, isLittleEndianess);

            _logger.LogInformation($"派梯接收心跳数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 断开连接 800000001F
        /// </summary>
        /// <returns></returns>
        public Task DisconnectAsync(Requests.KoneRcgifHeaderResponse headResponse, byte[] buffer)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneRcgifDisconnectMessageResponse();
            data.ReadFromBytes(buffer, isLittleEndianess);

            _logger.LogInformation($"派梯接收断开数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            return Task.CompletedTask;
        }

        public async Task PaddleAsync(Requests.KoneRcgifHeaderResponse headResponse, Requests.KoneRcgifSubHeader1Response subHeadResponse)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneRcgifYourTransportationMessageResponse();
            data.ReadFromBytes(subHeadResponse.Datas, isLittleEndianess);

            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            //获取数据来源
            var sequence = _koneRcgifHandleElevatorSequenceList.Get(subHeadResponse.ResponseId);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                return;
            }

            var elevatorDisplay = new ElevatorDisplayModel();
            elevatorDisplay.TerminalId = data.TerminalId;
            elevatorDisplay.DestinationFloor = data.DestinationFloor.ToString();
            elevatorDisplay.ElevatorId = data.ServingElevatorId.ToString();
            elevatorDisplay.MessageId = sequence.MessageId;
            elevatorDisplay.HandleElevatorDeviceId = sequence.HandleElevatorDeviceId;
            elevatorDisplay.CardDeviceId = sequence.CardDeviceId;

            // 返回错误信息
            if (data.ResponseCode == KoneRcgifResponseCodeEnum.BackupGroup.Code)
            {
                return;
            }
            else if (data.ResponseCode != KoneRcgifResponseCodeEnum.NoError.Code)
            {
                var error = BaseEnum.FromCode<KoneRcgifResponseCodeEnum>(data.ResponseCode);
                if (error != null)
                {
                    _logger.LogError($"操作错误：code:{data.ResponseCode} message:{error.Text} ");
                }
                else
                {
                    _logger.LogError($"操作错误：code:{error.Text} ");
                }

                //显示派梯结果
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                    await quantaDisplayDistributeDataService.HandleElevatorError(elevatorDisplay, $"派梯失败：{error.Text} ");
                }

                return;
            }

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                await quantaDisplayDistributeDataService.HandleElevatorSuccess(elevatorDisplay);
            }
        }
    }
}
