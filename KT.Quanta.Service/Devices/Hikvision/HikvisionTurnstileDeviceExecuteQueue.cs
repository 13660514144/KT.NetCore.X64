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
    /// 海康远程服务，异步执行
    /// </summary>
    public class HikvisionTurnstileDeviceExecuteQueue
    {
        //是否正在运行
        private bool _isRuning;
        //统计队列条数
        private int _totalNum;
        //队列锁
        private object _locker = new object();
        private readonly ConcurrentQueue<HikvisionDeviceExecuteQueueModel> _data;
        private readonly IEventAggregator _eventAggregator;

        private readonly ILogger<HikvisionTurnstileDeviceExecuteQueue> _logger;

        public HikvisionTurnstileDeviceExecuteQueue(ILogger<HikvisionTurnstileDeviceExecuteQueue> logger,
            IEventAggregator eventAggregator)
        {
            _data = new ConcurrentQueue<HikvisionDeviceExecuteQueueModel>();

            _logger = logger;
            _eventAggregator = eventAggregator;
        }

        public void Add(HikvisionDeviceExecuteQueueModel model)
        {
            _data.Enqueue(model);
            _totalNum++;
            _logger.LogInformation($"{DeviceKey} 海康闸机权限下发队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");

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
                    _logger.LogError(ex, "异步执行海康闸机设备下发对列失败！");
                }
            });
        }

        private HikvisionDeviceExecuteQueueModel Dequeue()
        {
            if (_data.IsEmpty)
            {
                return null;
            }
            var isSuccess = _data.TryDequeue(out HikvisionDeviceExecuteQueueModel result);
            if (isSuccess)
            {
                return result;
            }
            else
            {
                _logger.LogWarning($"{DeviceKey} 海康闸机获取分发数据失败！");
                return Dequeue();
            }
        }

        public Func<HikvisionDeviceExecuteQueueModel, Task> ExecuteActionAsync { private get; set; }
        public string DeviceKey;

        public async Task ExcuteAsync()
        {
            try
            {
                var model = Dequeue();
                if (model == null)
                {
                    _isRuning = false;
                    _logger.LogInformation($"{DeviceKey} 海康闸机权限下发结束：count:{_totalNum} queue:{_data.Count} ");
                    _totalNum = 0;
                    return;
                }
                _logger.LogInformation($"{DeviceKey} 海康闸机权限执行队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");
                //执行数据
                //Thread.Sleep(500);
                await ExecuteAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DeviceKey} 海康闸机执行数据分发失败：ex:{ex} ");
            }
            //Thread.Sleep(500);
            //发送下一条
            await ExcuteAsync();
        }

        //执行数据操作
        private async Task ExecuteAsync(HikvisionDeviceExecuteQueueModel model)
        {
            try
            {
                await ExecuteActionAsync(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DeviceKey} 海康闸机执行数据分发失败：ex:{ex} ");
            }
        }
    }
}

