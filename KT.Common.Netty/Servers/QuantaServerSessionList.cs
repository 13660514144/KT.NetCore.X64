using KT.Common.Netty.Servers.Models;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace KT.Common.Netty.Servers
{
    public class QuantaServerSessionList : IQuantaServerSessionList
    {
        private ConcurrentDictionary<string, QuantaServerSession> _responseActions;

        public QuantaServerSessionList()
        {
            _responseActions = new ConcurrentDictionary<string, QuantaServerSession>();
        }

        public Task AddAsync(string key, QuantaServerSession data)
        {
            _responseActions.AddOrUpdate(key, data, UpdateValueFactory(data));

            return Task.CompletedTask;
        }

        private Func<string, QuantaServerSession, QuantaServerSession> UpdateValueFactory(QuantaServerSession data)
        {
            return (key, oldData) => data;
        }

        public Task<QuantaServerSession> GetAsync(string key)
        {
            bool isGet = _responseActions.TryGetValue(key, out QuantaServerSession @value);
            if (isGet)
            {
                return Task.FromResult(@value);
            }
            return Task.FromResult<QuantaServerSession>(default);
        }

        public async Task<QuantaServerSession> RemoveAsync(string key)
        {
            bool isRemove = _responseActions.TryRemove(key, out QuantaServerSession @value);
            if (isRemove)
            {
                await @value.CloseAsync();
                return @value;
            }
            return default;
        }
    }
}
