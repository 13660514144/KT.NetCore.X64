using KT.Common.WebApi.HttpModel;
using KT.Prowatch.Service.Models;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.IServices
{
    public interface ILoginUserService
    {
        Task<LoginUserModel> AddAsync(LoginUserModel model);
        Task<LoginUserModel> CheckedOrAddAsync(LoginUserModel connect);
        Task<LoginUserModel> GetLastAsync();
        /// <summary>
        /// 登录最新一条记录
        /// </summary>
        /// <returns></returns>
        Task LoginLastAsync();
        Task<TokenResponse> LoginAsync(LoginUserModel loginUser);
    }
}