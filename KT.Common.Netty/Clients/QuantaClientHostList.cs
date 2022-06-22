using DotNetty.Transport.Channels;
using KT.Common.Netty.Servers.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Common.Netty.Clients
{
    public class QuantaClientHostList : IQuantaClientHostList
    {
        private ConcurrentDictionary<string, IQuantaClientHostBase> _responseActions;

        public QuantaClientHostList()
        {
            _responseActions = new ConcurrentDictionary<string, IQuantaClientHostBase>();
        }

        public Task AddAsync(string key, IQuantaClientHostBase data)
        {
            _responseActions.AddOrUpdate(key, data, UpdateValueFactory(data));

            return Task.CompletedTask;
        }

        private Func<string, IQuantaClientHostBase, IQuantaClientHostBase> UpdateValueFactory(IQuantaClientHostBase data)
        {
            return (key, oldData) => data;
        }

        public Task<IQuantaClientHostBase> GetAsync(string key)
        {
            bool isGet = _responseActions.TryGetValue(key, out IQuantaClientHostBase @value);
            if (isGet)
            {
                return Task.FromResult(@value);
            }
            return Task.FromResult<IQuantaClientHostBase>(default);
        }
        public async Task<IQuantaClientHostBase> RemoveAsync(string key)
        {
            bool isRemove = _responseActions.TryRemove(key, out IQuantaClientHostBase @value);
            if (isRemove)
            {
                await @value.CloseAsync();
                return @value;
            }
            return default;
        }
    }
}
