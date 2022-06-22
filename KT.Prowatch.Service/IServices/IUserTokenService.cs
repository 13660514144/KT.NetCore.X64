using KT.Prowatch.Service.Models;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.IServices
{
    public interface IUserTokenService
    {
        Task<UserTokenModel> GetByTokenAsync(string token);
        Task<UserTokenModel> InsertAsync(UserTokenModel tokenData);
    }
}