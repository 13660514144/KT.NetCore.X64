using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace KT.Common.Netty.Common
{
    /// <summary>
    /// 设备列表
    /// </summary>
    public class QuantaNettyActionManager : IQuantaNettyActionManager
    {
        private ConcurrentDictionary<string, Func<IChannel, QuantaNettyHeader, Task>> _actions;

        public QuantaNettyActionManager()
        {
            _actions = new ConcurrentDictionary<string, Func<IChannel, QuantaNettyHeader, Task>>();
        }

        public Task AddAsync(int module, int command, Func<IChannel, QuantaNettyHeader, Task> data)
        {
            _actions.AddOrUpdate($"{module}:{command}", data, UpdateValueFactory(data));

            return Task.CompletedTask;
        }

        private Func<string, Func<IChannel, QuantaNettyHeader, Task>, Func<IChannel, QuantaNettyHeader, Task>> UpdateValueFactory(Func<IChannel, QuantaNettyHeader, Task> data)
        {
            return (key, oldData) => data;
        }

        public Task<Func<IChannel, QuantaNettyHeader, Task>> GetAsync(int module, int command)
        {
            bool isGet = _actions.TryGetValue($"{module}:{command}", out Func<IChannel, QuantaNettyHeader, Task> @value);
            if (isGet)
            {
                return Task.FromResult(@value);
            }

            return Task.FromResult<Func<IChannel, QuantaNettyHeader, Task>>(default);
        }
    }
}
