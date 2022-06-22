using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace KT.Device
{
    /// <summary>
    /// 设备列表，单例
    /// </summary>
    public class DeviceList : IDeviceList
    {
        private ConcurrentDictionary<string, object> _devices;

        public DeviceList()
        {
            _devices = new ConcurrentDictionary<string, object>();
        }

        public Task AddAsync(string key, object data)
        {
            _devices.AddOrUpdate(key, data, UpdateValueFactory(data));

            return Task.CompletedTask;
        }

        private Func<string, object, object> UpdateValueFactory(object data)
        {
            return (key, oldData) => data;
        }

        public Task<object> GetAsync(string key)
        {
            bool isGet = _devices.TryGetValue(key, out object @value);
            if (isGet)
            {
                return Task.FromResult(@value);
            }
            return Task.FromResult<object>(default);
        }

        public Task<object> RemoveAsync(string key)
        {
            bool isRemove = _devices.TryRemove(key, out object @value);
            if (isRemove)
            {
                return Task.FromResult(@value);
            }
            return Task.FromResult<object>(default);
        }
    }
}
