using System.Threading.Tasks;

namespace KT.Common.Netty.Clients
{
    public interface IQuantaClientHostList
    {
        Task AddAsync(string key, IQuantaClientHostBase data);
        Task<IQuantaClientHostBase> GetAsync(string key);
        Task<IQuantaClientHostBase> RemoveAsync(string key);
    }
}