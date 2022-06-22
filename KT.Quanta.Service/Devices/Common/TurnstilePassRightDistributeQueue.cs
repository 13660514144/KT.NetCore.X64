using KT.Quanta.Service.Devices.DeviceDistributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class TurnstilePassRightDistributeQueue
    {
        private bool _isRuning;
        private int _totalNum;
        private object _locker = new object();
        private readonly ConcurrentQueue<TurnstilePassRightDistributeQueueModel> _data;

        private readonly ILogger<TurnstilePassRightDistributeQueue> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TurnstilePassRightDistributeQueue(ILogger<TurnstilePassRightDistributeQueue> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _data = new ConcurrentQueue<TurnstilePassRightDistributeQueueModel>();

            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }


        public void Add(TurnstilePassRightDistributeQueueModel model)
        {
            _data.Enqueue(model);
            _totalNum++;
            _logger.LogInformation($"闸机权限下发队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");

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
                    _logger.LogError(ex, "异步执行闸机权限下发对列失败！");
                }
            });
        }
        /// <summary>
        /// 闸机下发
        /// </summary>
        /// <returns></returns>
        private TurnstilePassRightDistributeQueueModel Dequeue()
        {
           
            lock (this)
            {
                if (_data.IsEmpty)
                {
                    return null;
                }
                var isSuccess = _data.TryDequeue(out TurnstilePassRightDistributeQueueModel result);
                if (isSuccess)
                {
                    return result;
                }
                else
                {
                    _logger.LogWarning("闸机获取分发数据失败！");
                    return Dequeue();
                }
            }
        }
        /// <summary>
        /// 分地推送队列
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
                    _logger.LogInformation($"闸机权限下发结束：count:{_totalNum} queue:{_data.Count} ");
                    _totalNum = 0;
                    return;
                }
                _logger.LogInformation($"闸机权限执行队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");

                if (model.DistributeType == PassRightDistributeTypeEnum.AddOrEdit.Value)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var turnstilePassRightDeviceDistributeService = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightDeviceDistributeService>();
                    await turnstilePassRightDeviceDistributeService.AddOrUpdateAsync(model.PassRight, model.Face);
                }
                else if (model.DistributeType == PassRightDistributeTypeEnum.Delete.Value)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var turnstilePassRightDeviceDistributeService = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightDeviceDistributeService>();
                    await turnstilePassRightDeviceDistributeService.DeleteAsync(model.PassRight);
                }
                else if (model.DistributeType == PassRightDistributeTypeEnum.AddOrEditByIds.Value)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var turnstilePassRightDeviceDistributeService = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightDeviceDistributeService>();
                    await turnstilePassRightDeviceDistributeService.AddOrUpdateAsync(model.Ids, model.PassRight, model.Face);
                }
                else if (model.DistributeType == PassRightDistributeTypeEnum.DeleteByIds.Value)
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var turnstilePassRightDeviceDistributeService = scope.ServiceProvider.GetRequiredService<ITurnstilePassRightDeviceDistributeService>();
                    await turnstilePassRightDeviceDistributeService.DeleteAsync(model.Ids, model.PassRight);
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
