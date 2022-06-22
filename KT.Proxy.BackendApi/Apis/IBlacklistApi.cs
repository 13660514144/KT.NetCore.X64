using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface IBlacklistApi
    {
        Task AddAsync(BlacklistModel black);
        Task DeleteAsync(long id);
        Task EditAsync(BlacklistModel model);
        Task<PageData<BlacklistModel>> GetBlacksAsync(BlacklistSearchModel blacklistSearch);
        Task<BlacklistModel> GetByIdAsync(long id);
        Task<bool> IsBlackAsync(CheckBlacklistModel model);
    }
}
