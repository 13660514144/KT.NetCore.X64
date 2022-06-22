using System.Threading.Tasks;

namespace KT.Device
{
    public interface IDeviceList
    {
        Task AddAsync(string key, object data);
        Task<object> GetAsync(string key);
        Task<object> RemoveAsync(string key);
    }
}