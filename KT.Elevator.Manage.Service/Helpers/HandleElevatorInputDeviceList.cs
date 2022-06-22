using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    /// <summary>
    /// 二次派梯一体机设备列表
    /// </summary>
    public class HandleElevatorInputDeviceList
    {
        private readonly ILogger<HandleElevatorInputDeviceList> _logger;

        public HandleElevatorInputDeviceList(ILogger<HandleElevatorInputDeviceList> logger)
        {
            _logger = logger;
            _queue = new ConcurrentDictionary<string, HandleElevatorInputDeviceModel>();
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
                _queue.TryAdd(item.Id, item);
            }
        }

        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <returns></returns>
        public List<DeviceStateModel> GetStates()
        {
            var results = _queue.Select(x =>
               {
                   return new DeviceStateModel()
                   {
                       Id = x.Value.Id,
                       IsOnline = x.Value.IsOnline
                   };
               }).ToList();

            return results;
        }

        /// <summary>
        /// 轮询所有设备执行
        /// </summary>
        /// <param name="action">执行的委托定义</param>
        public async Task<List<HandleElevatorInputDeviceModel>> GetAllAsync(Func<HandleElevatorInputDeviceModel, HandleElevatorInputDeviceModel> action)
        {
            return await Task.Run(() =>
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
                return results;
            });
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
