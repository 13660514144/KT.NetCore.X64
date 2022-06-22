using KT.Common.Core.Utils;
using KT.Turnstile.Model.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Helpers
{
    /// <summary>
    /// 边缘处理器数据
    /// </summary>
    public class ProcessorDeviceList
    {
        private readonly ILogger<ProcessorDeviceList> _logger;

        public ProcessorDeviceList(ILogger<ProcessorDeviceList> logger)
        {
            _logger = logger;
            _queue = new ConcurrentDictionary<string, ProcessorModel>();
        }

        //所有设备信息 
        private ConcurrentDictionary<string, ProcessorModel> _queue;

        public void Init(List<ProcessorModel> devices)
        {
            _queue.Clear();

            if (devices == null)
            {
                return;
            }
            foreach (var item in devices)
            {
                if (!string.IsNullOrEmpty(item.ProcessorKey))
                {
                    _queue.TryAdd(item.ProcessorKey, item);
                }
                else
                {
                    _queue.TryAdd(item.Id, item);
                }
            }
        }


        /// <summary>
        /// 轮询所有边缘处理器执行
        /// </summary>
        /// <param name="action">执行的委托定义</param>
        public void ExecAll(Action<ProcessorModel> action)
        {
            foreach (var item in _queue)
            {
                action.Invoke(item.Value);
            }
        }


        /// <summary>
        /// 向队列中添加接收到的数据
        /// </summary>
        /// <param name="data">接收到的数据</param>
        public void AddOrUpdate(ProcessorModel data)
        {
            if (!string.IsNullOrEmpty(data.ProcessorKey))
            {
                var oldItem = _queue.GetValueOrDefault(data.ProcessorKey);
                if (oldItem != null)
                {
                    data.IsOnline = oldItem.IsOnline;
                    data.ConnectionId = oldItem.ConnectionId;
                }
            }

            if (!string.IsNullOrEmpty(data.ProcessorKey))
            {
                _queue.AddOrUpdate(data.ProcessorKey, data, UpdateValueFactory(data));
            }
            else
            {
                _queue.AddOrUpdate(data.Id, data, UpdateValueFactory(data));
            }
        }

        private Func<string, ProcessorModel, ProcessorModel> UpdateValueFactory(ProcessorModel data)
        {
            return (key, oldData) => data;
        }

        public ProcessorModel GetByConnectionId(string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.Value.ConnectionId == connectionId);
            return val.Value;
        }



        /// <summary>
        /// 根据串口名称获取设备
        /// </summary>
        /// <param name="key">串口名称</param>
        /// <returns></returns>
        public ProcessorModel GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.Key == key);
            return val.Value;
        }

        /// <summary>
        /// 根据串口名称获取设备
        /// </summary>
        /// <param name="key">串口名称</param>
        /// <returns></returns>
        public ProcessorModel GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.Value.Id == id);
            return val.Value;
        }

        /// <summary>
        /// 根据串口名称获取设备
        /// </summary>
        /// <param name="key">串口名称</param>
        /// <returns></returns>
        public string GetKeyById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var val = _queue.FirstOrDefault(x => x.Value.Id == id);
            return val.Key;
        }
        public ProcessorModel RemoveByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (_queue.ContainsKey(key))
            {
                _queue.Remove(key, out ProcessorModel result);
                return result;
            }
            return null;
        }

        internal KeyValuePair<string, ProcessorModel> GetByIpAndPortId(string ipAddress, int port)
        {
            if (string.IsNullOrEmpty(ipAddress) && port > 0)
            {
                return new KeyValuePair<string, ProcessorModel>();
            }

            var val = _queue.FirstOrDefault(x => x.Value.IpAddress == ipAddress && x.Value.Port == port);
            return val;
        }

        internal ProcessorModel RemoveById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            var key = GetKeyById(id);
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            _queue.Remove(key, out ProcessorModel result);
            return result;
        }
    }
}
