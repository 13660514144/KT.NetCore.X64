using KT.Quanta.Service.Devices.DeviceDistributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class ElevatorPassRightDistributeQueue
    {
        private bool _isRuning;
        private int _totalNum;
        private object _locker = new object();
        private readonly ConcurrentQueue<ElevatorPassRightDistributeQueueModel> _data;

        private readonly ILogger<ElevatorPassRightDistributeQueue> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ElevatorPassRightDistributeQueue(ILogger<ElevatorPassRightDistributeQueue> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _data = new ConcurrentQueue<ElevatorPassRightDistributeQueueModel>();

            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Add(ElevatorPassRightDistributeQueueModel model)
        {
            _data.Enqueue(model);
            _totalNum++;
            _logger.LogInformation($"梯控权限下发队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");

            if (!_isRuning)
            {
                lock (_locker)
                {
                    if (!_isRuning)
                    {
                        _isRuning = true;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
            //直接运行不能异步
            Task.Run(async () =>
            {
                try
                {
                    await ExcuteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "执行权限分发对列失败！");
                }
            });
        }

        private ElevatorPassRightDistributeQueueModel Dequeue()
        {
            if (_data.IsEmpty)
            {
                return null;
            }
            var isSuccess = _data.TryDequeue(out ElevatorPassRightDistributeQueueModel result);
            if (isSuccess)
            {
                return result;
            }
            else
            {
                _logger.LogInformation("梯控获取分发数据失败！");
                return Dequeue();
            }
        }
        /// <summary>
        /// 梯控权限下发入口
        /// </summary>
        /// <returns></returns>
        private async Task ExcuteAsync()
        {
            try
            {
                var model = Dequeue();
                if (model == null)
                {
                    _isRuning = false;
                    _logger.LogInformation($"梯控权限下发结束：count:{_totalNum} queue:{_data.Count} ");
                    _totalNum = 0;
                    return;
                }
                _logger.LogInformation($"梯控权限执行队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");
                //开始创建下发队列
                if (model.DistributeType == PassRightDistributeTypeEnum.AddOrEdit.Value)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var elevatorPassRightDeviceDistributeService = scope.ServiceProvider.GetRequiredService<IElevatorPassRightDeviceDistributeService>();
                    await elevatorPassRightDeviceDistributeService.AddOrUpdateAsync(model.PassRight, model.Face, model.OldPassRight);
                }
                else if (model.DistributeType == PassRightDistributeTypeEnum.Delete.Value)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var elevatorPassRightDeviceDistributeService = scope.ServiceProvider.GetRequiredService<IElevatorPassRightDeviceDistributeService>();
                    await elevatorPassRightDeviceDistributeService.DeleteAsync(model.PassRight);
                }
                else
                {
                    _logger.LogError($"找不到下发权限类型：type:{model.DistributeType }");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"执行数据分发失败：ex:{ex} ");
            }

            //执行下一条数据
            await ExcuteAsync();
        }
    }
}
