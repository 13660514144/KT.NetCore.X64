using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Netty.Servers
{
    public class QuantaServerActionList : IQuantaServerActionList
    {
        private ConcurrentDictionary<string, IChannel> _responseActions;

        public QuantaServerActionList()
        {
            _responseActions = new ConcurrentDictionary<string, IChannel>();
        }

        public void Add(string key, IChannel data)
        {
            _responseActions.AddOrUpdate(key, data, UpdateValueFactory(data));
        }

        private Func<string, IChannel, IChannel> UpdateValueFactory(IChannel data)
        {
            return (key, oldData) => data;
        }

        public IChannel Get(string key)
        {
            bool isGet = _responseActions.TryGetValue(key, out IChannel @value);
            if (isGet)
            {
                return @value;
            }
            return default;
        }
    }
}
