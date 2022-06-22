using KT.Common.Core.Utils;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Quanta.Helpers;
using KT.Quanta.Service.Devices.Quanta.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hitachi.Handlers
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public class HitachiCallReponseHandler : IHitachiCallReponseHandler
    {
        private ILogger<HitachiCallReponseHandler> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private IHitachiHandleElevatorSequenceList _hitachiHandleElevatorSequenceList;

        public HitachiCallReponseHandler(ILogger<HitachiCallReponseHandler> logger,
            IServiceScopeFactory serviceScopeFactory,
            IHitachiHandleElevatorSequenceList hitachiHandleElevatorSequenceList)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _hitachiHandleElevatorSequenceList = hitachiHandleElevatorSequenceList;
        }

        public async Task PaddleAsync(UnitDispatchReceiveHandleElevatorModel response)
        {
            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(response, JsonUtil.JsonPrintSettings)} ");

            //获取数据来源
            var sequence = _hitachiHandleElevatorSequenceList.Get(response.MessageId);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                return;
            }

            var elevatorDisplay = new ElevatorDisplayModel();
            elevatorDisplay.DestinationFloor = response.DistinationFloorId;
            elevatorDisplay.ElevatorId = response.ElevatorName;
            elevatorDisplay.MessageId = sequence.MessageId;

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                await quantaDisplayDistributeDataService.HandleElevatorSuccess(elevatorDisplay);
            }
        }
    }
}
