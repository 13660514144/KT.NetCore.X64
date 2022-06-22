using DotNetty.Transport.Channels;
using System;
using System.Threading.Tasks;

namespace KT.Common.Netty.Common
{
    public interface IQuantaNettyActionManager
    {
        Task AddAsync(int module, int command, Func<IChannel, QuantaNettyHeader,Task> data);
        Task<Func<IChannel, QuantaNettyHeader,Task>> GetAsync(int module, int command);
    }
}