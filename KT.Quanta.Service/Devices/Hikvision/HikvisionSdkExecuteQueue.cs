using KT.Common.Core.Utils;
using KT.Common.Event;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Devices.Schindler.Clients;
using KT.Quanta.Service.Devices.Schindler.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    /// <summary>
    /// 海康单个设备执行数据，不能异步
    /// </summary>
    public class HikvisionSdkExecuteQueue
    {
        //是否正在运行
        private bool _isRuning;
        //统计队列条数
        private int _totalNum;
        //队列锁
        private object _locker = new object();
        private readonly ConcurrentQueue<HikvisionSdkExecuteQueueModel> _data;
        private readonly IEventAggregator _eventAggregator;

        private readonly ILogger<HikvisionSdkExecuteQueue> _logger;

        public HikvisionSdkExecuteQueue(ILogger<HikvisionSdkExecuteQueue> logger,
            IEventAggregator eventAggregator)
        {
            _data = new ConcurrentQueue<HikvisionSdkExecuteQueueModel>();

            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        public void Add(HikvisionSdkExecuteQueueModel model)
        {
            _data.Enqueue(model);
            _totalNum++;
            _logger.LogInformation($"{DeviceKey} 海康权限下发队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");

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
                    _logger.LogError(ex, "异步执行海康SDK权限下发对列失败！");
                }
            });
        }
        private HikvisionSdkExecuteQueueModel Dequeue()
        {
            if (_data.IsEmpty)
            {
                return null;
            }
            var isSuccess = _data.TryDequeue(out HikvisionSdkExecuteQueueModel result);
            if (isSuccess)
            {
                return result;
            }
            else
            {
                _logger.LogWarning($"{DeviceKey} 海康获取分发数据失败！");
                return Dequeue();
            }
        }

        public Func<HikvisionSdkExecuteQueueModel, Task> ExecuteActionAsync;
        public string DeviceKey;

        public async Task ExcuteAsync()
        {
            try
            {
                var model = Dequeue();
                if (model == null)
                {
                    _isRuning = false;
                    _logger.LogInformation($"{DeviceKey} 海康权限下发结束：count:{_totalNum} queue:{_data.Count} ");
                    _totalNum = 0;
                    return;
                }
                await ExecuteActionAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DeviceKey} 海康执行数据分发失败：ex:{ex} ");
            }

            //发送下一条
            await ExcuteAsync();
        }
    }
}

