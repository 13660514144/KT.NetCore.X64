using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Helpers
{
    /// <summary>
    /// 二次派梯一体机设备列表
    /// </summary>
    public class HandleElevatorInputDeviceList
    {
        private readonly ILogger<HandleElevatorInputDeviceList> _logger;
        private readonly AppSettings _appSettings;

        public HandleElevatorInputDeviceList(ILogger<HandleElevatorInputDeviceList> logger,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _queue = new ConcurrentDictionary<string, HandleElevatorInputDeviceModel>();
            _appSettings = appSettings.Value;
        }

        //所有设备信息 
        private ConcurrentDictionary<string, HandleElevatorInputDeviceModel> _queue;

        public void Init(List<HandleElevatorInputDeviceModel> devices)
        {
            _queue.Clear();

            if (devices == null)
            {
                return;
            }
            foreach (var item in devices)
            {
                if (_appSettings.IsAsyncInitRemoteDevice)
                {
                    Task.Run(async () =>
                    {
                        try
                        {
                            _queue.TryAdd(item.Id, item);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "异步派梯设备读卡器失败！");
                        }
                    });
                }
                else
                {
                    _queue.TryAdd(item.Id, item);
                }
            }
        }

        /// <summary>
        /// 轮询所有设备执行
        /// </summary>
        /// <param name="action">执行的委托定义</param>
        public Task<List<HandleElevatorInputDeviceModel>> GetAllAsync(Func<HandleElevatorInputDeviceModel, HandleElevatorInputDeviceModel> action)
        {
            var results = new List<HandleElevatorInputDeviceModel>();
            foreach (var item in _queue)
            {
                var model = action.Invoke(item.Value);
                if (model != null)
                {
                    results.Add(model);
                }
            }

            return Task.FromResult(results);
        }

        /// <summary>
        /// 向队列中添加或修改数据
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public void AddOrUpdate(HandleElevatorInputDeviceModel data)
        {
            _queue.AddOrUpdate(data.Id, data, UpdateValueFactory(data));
        }

        private static Func<string, HandleElevatorInputDeviceModel, HandleElevatorInputDeviceModel> UpdateValueFactory(HandleElevatorInputDeviceModel data)
        {
            return (key, oldData) => data;
        }

        /// <summary>
        /// 获取设备
        /// </summary>
        /// <param name="key">key id</param>
        /// <returns>获取的设备结果</returns>
        public HandleElevatorInputDeviceModel GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.Key == key);
            return val.Value;
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="key">key id</param>
        /// <returns>删除的设备</returns>
        public HandleElevatorInputDeviceModel RemoveByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (_queue.ContainsKey(key))
            {
                _queue.Remove(key, out HandleElevatorInputDeviceModel result);
                return result;
            }
            return null;
        }

    }
}
