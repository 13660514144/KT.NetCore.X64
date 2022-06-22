using KT.Common.Core.Utils;
using KT.Common.Event;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Schindler.Clients;
using KT.Quanta.Service.Devices.Schindler.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Quanta.Service.Helpers;

namespace KT.Quanta.Service.Devices.Schindler.Models
{
    public class SchindlerDatabaseQueue
    {
        //是否正在运行
        private bool _isRuning;
        //统计队列条数
        private int _totalNum;
        //最后一条发送时间
        private long _sendLastTime;
        //时间定时器，超时未返回发送下一条数据
        private Timer _timer;
        //队列锁
        private object _locker = new object();
        private readonly ConcurrentQueue<SchindlerDatabaseQueueModel> _data;
        private readonly IEventAggregator _eventAggregator;
        private readonly SchindlerSettings _schindlerSettings;

        private readonly ILogger<SchindlerDatabaseQueue> _logger;
        private DistirbQueueSchindler _DistirbQueueSchindler;
        public SchindlerDatabaseQueue(ILogger<SchindlerDatabaseQueue> logger,
            IEventAggregator eventAggregator,
            IOptions<SchindlerSettings> schindlerSettings
            , DistirbQueueSchindler _distirbQueueSchindler)
        {
            _data = new ConcurrentQueue<SchindlerDatabaseQueueModel>();

            _logger = logger;
            _eventAggregator = eventAggregator;
            _schindlerSettings = schindlerSettings.Value;

            _eventAggregator.GetEvent<DatabaseResponseEvent>().Subscribe(Excute);

            _timer = new Timer(ReponseTimeOutTimerCallback, null, -1, 500);
            _DistirbQueueSchindler = _distirbQueueSchindler;
        }

        private void Excute()
        {
            //直接运行不能异步
            Task.Run(async () =>
            {
                try
                {
                    await ExcuteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "异步执行Schindler权限下发对列失败！");
                }
            });
        }

        private void ReponseTimeOutTimerCallback(object state)
        {
            if (_isRuning)
            {
                //设置4秒钟超时
                if ((DateTimeUtil.UtcNowMillis() - _sendLastTime) >= _schindlerSettings.ResponseSecondTime)
                {
                    //直接运行不能异步
                    Task.Run(async () =>
                    {
                        try
                        {
                            await ExcuteAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "异步执行Schindler权限下发对列失败！");
                        }
                    });
                }
            }
            else
            {
                _logger.LogInformation($"迅达权限下发超时定时器暂停！");
                _timer.Change(-1, 500);
            }
        }

        public void Add(SchindlerDatabaseQueueModel model)
        {
            _data.Enqueue(model);
            _totalNum++;
            _logger.LogInformation($"迅达权限下发队列：count:{_totalNum} queue:{_data.Count} type:{model.DistributeType} ");

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

            _timer.Change(100, 100);

            //直接运行不能异步
            Task.Run(async () =>
            {
                try
                {
                    await ExcuteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "异步执行Schindler权限下发对列失败！");
                }
            });
        }

        private SchindlerDatabaseQueueModel Dequeue()
        {
            if (_data.IsEmpty)
            {
                return null;
            }
            var isSuccess = _data.TryDequeue(out SchindlerDatabaseQueueModel result);
            if (isSuccess)
            {
                return result;
            }
            else
            {
                _logger.LogWarning("获取分发数据失败！");
                return Dequeue();
            }
        }

        public async Task ExcuteAsync()
        {
         
            try
            {
                var model = Dequeue();
                if (model == null)
                {
                    _isRuning = false;
                    _timer.Change(-1, 500);
                    _logger.LogInformation($"迅达权限下发结束：count:{_totalNum} queue:{_data.Count} ");
                    _totalNum = 0;
                    return;
                }
                //发送完成更新最后发送时间
                _sendLastTime = DateTimeUtil.UtcNowMillis();

                if (model.DistributeType == SchindlerDatabaseDistributeTypeEnum.ChangeOrInsertPerson.Value)
                {
                    foreach (var item in model.RemoteDevice.CommunicateDevices)
                    {
                        //分流服务 2021-10-19
                        _DistirbQueueSchindler._SchindlerQueue.CommunicateDevices.Add(item);
                        _DistirbQueueSchindler._SchindlerQueue.Datas.Add(model.Datas);
                        //分流服务 2021-10-19
                        //var client = item.GetLoginUserClient<ISchindlerDatabaseClientHost>();
                        //await client.SendAsync(model.Datas);
                    }

                    _logger.LogInformation($"迅达结束下发卡信息：{model.Datas.ToCommaPrintString()} ");
                }
                else if (model.DistributeType == SchindlerDatabaseDistributeTypeEnum.ChangePersonZone.Value)
                {
                    foreach (var item in model.RemoteDevice.CommunicateDevices)
                    {
                        //分流服务 2021-10-19
                        _DistirbQueueSchindler._SchindlerQueue.CommunicateDevices.Add(item);
                        _DistirbQueueSchindler._SchindlerQueue.Datas.Add(model.Datas);
                        //分流服务 2021-10-19
                        //var client = item.GetLoginUserClient<ISchindlerDatabaseClientHost>();
                        //await client.SendAsync(model.Datas);
                    }

                    _logger.LogInformation($"迅达结束下发卡权限：{model.Datas.ToCommaPrintString()} ");
                }
                else if (model.DistributeType == SchindlerDatabaseDistributeTypeEnum.DeletePerson.Value)
                {
                    foreach (var item in model.RemoteDevice.CommunicateDevices)
                    {
                        //分流服务 2021-10-19
                        _DistirbQueueSchindler._SchindlerQueue.CommunicateDevices.Add(item);
                        _DistirbQueueSchindler._SchindlerQueue.Datas.Add(model.Datas);
                        //分流服务 2021-10-19
                        //var client = item.GetLoginUserClient<ISchindlerDatabaseClientHost>();
                        //await client.SendAsync(model.Datas);
                    }

                    _logger.LogInformation($"迅达结束下发删除权限信息：{model.Datas.ToCommaPrintString()} ");
                }
                else
                {
                    _logger.LogError($"迅达找不到下发权限类型：type:{model.DistributeType}");
                    //失败后发送下一条
                    Task.Run(async () =>
                    {
                        try
                        {
                            await ExcuteAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "异步执行Schindler权限下发对列失败！");
                        }
                    });
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"迅达执行数据分发失败：ex:{ex} ");
                //失败后发送下一条
                /*
                Task.Run(async () =>
                {
                    try
                    {
                        await ExcuteAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "异步执行Schindler权限下发对列失败！");
                    }
                });
                */
            }
        }
    }
}

