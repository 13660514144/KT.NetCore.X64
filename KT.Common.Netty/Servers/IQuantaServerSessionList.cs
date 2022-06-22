using DotNetty.Transport.Channels;
using KT.Common.Netty.Servers.Models;
using System.Threading.Tasks;

namespace KT.Common.Netty.Servers
{
    public interface IQuantaServerSessionList
    {
        Task AddAsync(string key, QuantaServerSession data);
        Task<QuantaServerSession> GetAsync(string key);
        Task<QuantaServerSession> RemoveAsync(string key);
    }
}